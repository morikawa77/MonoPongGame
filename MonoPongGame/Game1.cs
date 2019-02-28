using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace MonoPongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //texturas

        Texture2D gameplay;
        Texture2D gamestart;
        Texture2D gameover;
        Texture2D gamewin;

        //entidade
        Bola bola;
        Bastao jogador1;
        Bastao jogador2;

        // Som
        Song musica;
        SoundEffect pontoSom;

        // Font
        SpriteFont placarFont;

        // Pontuacao
        int[] score = new int[2];

        // condicao de vitoria
        const int totalpontos = 11;

        // estados do jogo
        Jogo jogo;
        private GameTime gameTime;

        // Dificuldade do jogo
        int dificuldade;
        float enemyYacelera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //dimencionar a tela
            //comprimento
            graphics.PreferredBackBufferWidth = 800;
            //altura
            graphics.PreferredBackBufferHeight = 600;

            //titulo do jogo
            Window.Title = "MonoPong Game";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            jogo = Jogo.GameStart;
            MediaPlayer.Play(musica);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //carrega a textura 2d
            gameplay = Content.Load<Texture2D>("gameplay");
            gamestart = Content.Load<Texture2D>("gameplay2");
            gameover = Content.Load<Texture2D>("gameover");
            gamewin = Content.Load<Texture2D>("gamewin");

            //istancia do objeto
            bola = new Bola(this, new Vector2(384.0f, 300.0f));
            jogador1 = new Bastao(this,new Vector2(2, 250.0f));
            jogador2 = new Bastao(this, new Vector2(765.0f, 250.0f));

            // carrega musica e efeito sonoro
            musica = Content.Load<Song>("musica");
            pontoSom = Content.Load<SoundEffect>("risada");
            

            // carrega o spriteFont para o placar
            placarFont = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            GamePadCapabilities c = GamePad.GetCapabilities(PlayerIndex.One);
            if (c.IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (c.HasLeftYThumbStick)
                {
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1.0f)
                    {
                        jogador1.Direcao = new Vector2(0.0f, -2.0f);

                        if (jogador1.Posicao.Y < 0.0f)
                        {
                            jogador1.Direcao = new Vector2(0.0f, 0.0f);
                        }
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -1.0f)
                    {
                        jogador1.Direcao = new Vector2(0.0f, 2.0f);
                        if (jogador1.Posicao.Y + jogador1.Textura.Height > 600.0f)
                        {
                            jogador1.Direcao = new Vector2(0.0f, 0.0f);
                        }
                    }
                    else
                    {
                        jogador1.Direcao = Vector2.Zero;
                    }

                    jogador1.Update(gameTime);
                }

                /*
                if (c.HasRightYThumbStick)
                {
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y == 1.0f)
                    {
                        jogador2.Direcao = new Vector2(0.0f, -2.0f);

                        if (jogador2.Posicao.Y < 0.0f)
                        {
                            jogador2.Direcao = new Vector2(0.0f, 0.0f);
                        }
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y == -1.0f)
                    {
                        jogador2.Direcao = new Vector2(0.0f, 2.0f);
                        if (jogador2.Posicao.Y + jogador1.Textura.Height > 600.0f)
                        {
                            jogador2.Direcao = new Vector2(0.0f, 0.0f);
                        }
                    }
                    else
                    {
                        jogador2.Direcao = Vector2.Zero;
                    }

                    jogador2.Update(gameTime);
                }

                */
                // start no normal
                if (c.HasStartButton)
                {

                    // Se apertar start
                    if (state.IsButtonDown(Buttons.Start))
                    {
                        jogo = Jogo.GamePlay;
                        dificuldade = 2;
                    } 
                }

                // start no easy
                if(c.HasAButton)
                {

                    // Se apertar A
                    if (state.IsButtonDown(Buttons.A))
                    {
                        jogo = Jogo.GamePlay;
                        dificuldade = 1;
                    }
                }

                // start no hard
                if (c.HasXButton)
                {

                    // Se apertar X
                    if (state.IsButtonDown(Buttons.X))
                    {
                        jogo = Jogo.GamePlay;
                        dificuldade = 3;
                    }
                }

                // start no hardcore
                if (c.HasYButton)
                {

                    // Se apertar Y
                    if (state.IsButtonDown(Buttons.Y))
                    {
                        jogo = Jogo.GamePlay;
                        dificuldade = 4;
                    }
                }

                if (c.HasBButton)
                {
                    // Se apertar B
                    if (state.IsButtonDown(Buttons.B))
                    {
                        jogo = Jogo.GameStart;
                        RestartGame();
                    }
                }
            }

            // TODO: Add your update logic here
            //imput por teclado
            KeyboardState teclado = Keyboard.GetState();
            //verifica se tecla esta pressionada W e move para cima 
            if (teclado.IsKeyDown(Keys.W))
            {
                jogador1.Direcao = new Vector2(0.0f, -2.0f);

                if (jogador1.Posicao.Y < 0.0f)
                {
                    jogador1.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            //verifica se tecla esta pressionada S e move para cima 
            else if (teclado.IsKeyDown(Keys.S))
            {
                jogador1.Direcao = new Vector2(0.0f, 2.0f);
                if (jogador1.Posicao.Y + jogador1.Textura.Height > 600.0f)
                {
                    jogador1.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            else if (teclado.IsKeyDown(Keys.Enter))
            {
                jogo = Jogo.GamePlay;
                dificuldade = 2;
            }
            else if (teclado.IsKeyDown(Keys.Escape))
            {
                Exit();
            } 
            else if (teclado.IsKeyDown(Keys.Back))
            {
                jogo = Jogo.GameStart;
                RestartGame();
            }
            else if (teclado.IsKeyDown(Keys.Space))
            {
                jogo = Jogo.GamePlay;
                dificuldade = 2;
            }
            else if (teclado.IsKeyDown(Keys.A))
            {
                jogo = Jogo.GamePlay;
                dificuldade = 1;
            }
            else if (teclado.IsKeyDown(Keys.X))
            {
                jogo = Jogo.GamePlay;
                dificuldade = 3;
            }
            else if (teclado.IsKeyDown(Keys.Y))
            {
                jogo = Jogo.GamePlay;
                dificuldade = 4;
            }
            // Verifica parado
            else
            {
                jogador1.Direcao = Vector2.Zero;
            }

            jogador1.Update(gameTime);

            /*
            if (teclado.IsKeyDown(Keys.Up))
            {
                jogador2.Direcao = new Vector2(0.0f, -2.0f);

                if (jogador2.Posicao.Y < 0.0f)
                {
                    jogador2.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            //verifica se tecla esta pressionada S e move para cima 
            else if (teclado.IsKeyDown(Keys.Down))
            {
                jogador2.Direcao = new Vector2(0.0f, 2.0f);
                if (jogador2.Posicao.Y + jogador2.Textura.Height > 600.0f)
                {
                    jogador2.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            // Verifica parado
            else
            {
                jogador2.Direcao = Vector2.Zero;
            }

            jogador2.Update(gameTime);

            */

            MoveBastaoComputador();
            jogador2.Update(gameTime);

            switch (jogo)
            {
                case Jogo.GameStart:

                    break;
                case Jogo.GamePlay:
                    // Verifica colisao da bola com as paredes de cima e baixo
                    if (bola.Posicao.Y + bola.Textura.Height > 600.0f)
                    {
                        bola.Direcao *= new Vector2(1.0f, -1.0f);
                    }

                    if (bola.Posicao.Y < 0.0f)
                    {
                        bola.Direcao *= new Vector2(1.0f, -1.0f);
                    }


                    bola.Update(gameTime);

                    // verifica a colisao da bola com os bastoes
                    if (bola.VerificarColisao().Intersects(jogador1.VerificarColisao()) || bola.VerificarColisao().Intersects(jogador2.VerificarColisao()))
                    {
                        // Inverter a direcao de X da bola
                        bola.Direcao *= new Vector2(-1.0f, 1.0f);

                        Random rnd = new Random();
                        // de /2(50%) a /5(20%)
                        int indice = rnd.Next(2, 5);

                        bola.AumentarVelocidade(indice);
                    }


                    // Verifica a saida da bola da tela
                    if (bola.Posicao.X + bola.Textura.Width > 800.0f)
                    {
                        pontoSom.Play();
                        bola = new Bola(this, new Vector2(384.0f, 300.0f));
                        score[0] += 1;
                        if (score[0] >= totalpontos)
                        {
                            jogo = Jogo.GameWin;
                            RestartGame();
                        }
                    }

                    if (bola.Posicao.X < 0.0f)
                    {
                        pontoSom.Play();
                        bola = new Bola(this, new Vector2(384.0f, 300.0f));
                        score[1] += 1;
                        if (score[1] >= totalpontos)
                        {
                            jogo = Jogo.GameOver;
                            RestartGame();
                        }
                    }
                    break;
                case Jogo.GameOver:

                    break;
            }


            
            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           //inicializa o desenho do jogo

            spriteBatch.Begin();

            switch (jogo)
            {
                case Jogo.GameStart:
                    spriteBatch.Draw(gamestart, Vector2.Zero, Color.White);
                    break;
                case Jogo.GamePlay:
                    spriteBatch.Draw(gameplay, Vector2.Zero, Color.White);
                    bola.Draw(spriteBatch);
                    jogador1.Draw(spriteBatch);
                    jogador2.Draw(spriteBatch);

                    // inicializa o HUD
                    Vector2 score1Posicao = placarFont.MeasureString(score[0].ToString("000"));
                    Vector2 score2Posicao = placarFont.MeasureString(score[1].ToString("000"));

                    spriteBatch.DrawString(placarFont, score[0].ToString("000"), new Vector2(300.0f, 35.0f) - score1Posicao / 2, Color.White);
                    spriteBatch.DrawString(placarFont, score[1].ToString("000"), new Vector2(500.0f, 35.0f) - score2Posicao / 2, Color.White);
                    break;
                case Jogo.GameOver:
                    spriteBatch.Draw(gameover, Vector2.Zero, Color.White);
                    break;
                case Jogo.GameWin:
                    spriteBatch.Draw(gamewin, Vector2.Zero, Color.White);
                    break;
            }
            

            //finaliza o desenho do jogo

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public void RestartGame()
        {
            score[0] = score[1] = 000;
            bola = new Bola(this, new Vector2(384.0f, 300.0f));
        }

        public void MoveBastaoComputador()
        {
            
            switch (dificuldade)
            {
                case 1:
                    enemyYacelera = 1.8f;
                    break;
                case 2:
                    enemyYacelera = 2.0f;
                    break;
                case 3:
                    enemyYacelera = 2.5f;
                    break;
                case 4:
                    enemyYacelera = 3.0f;
                    break;
            }
            
            if (bola.Posicao.Y < jogador2.Posicao.Y)
            {
                jogador2.Direcao = new Vector2(0.0f, -enemyYacelera);

                if (jogador2.Posicao.Y < 0.0f)
                {
                    jogador2.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            
            else if (bola.Posicao.Y > jogador2.Posicao.Y)
            {
                jogador2.Direcao = new Vector2(0.0f, enemyYacelera);
                if (jogador2.Posicao.Y + jogador2.Textura.Height > 600.0f)
                {
                    jogador2.Direcao = new Vector2(0.0f, 0.0f);
                }
            }
            // Verifica parado
            else
            {
                jogador2.Direcao = Vector2.Zero;
            }

            // jogador2.Update(gameTime);
        }

    }

    public enum Jogo
    {
        GameStart,
        GamePlay,
        GameOver,
        GameWin
    }
}
