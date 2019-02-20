using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //entidade
        Bola bola;
        Bastao jogador1;
        Bastao jogador2;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //dimencionar a tela
            //comprimento
            graphics.PreferredBackBufferWidth = 800;
            //altura
            graphics.PreferredBackBufferHeight = 600;

            //titolo do jogo
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

            //istancia do objeto
            bola = new Bola(this, new Vector2(384.0f, 300.0f));
            jogador1 = new Bastao(this,new Vector2(2, 250.0f));
            jogador2 = new Bastao(this, new Vector2(765.0f, 250.0f));
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
                    

                if(c.HasRightYThumbStick)
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
            // Verifica parado
            else
            {
                jogador1.Direcao = Vector2.Zero;
            }

            jogador1.Update(gameTime);


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
            
            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //inicializa o desenho do jogo

            spriteBatch.Begin();

            spriteBatch.Draw(gameplay, Vector2.Zero, Color.White);
            bola.Draw(spriteBatch);
            jogador1.Draw(spriteBatch);
            jogador2.Draw(spriteBatch);

            //finaliza o desenho do jogo

            spriteBatch.End();


            base.Draw(gameTime);
        }
        
    }
}
