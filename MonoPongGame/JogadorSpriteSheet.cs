using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongGame
{
    public class JogadorSpriteSheet : GameComponent
    {
        public Texture2D Textura { get; set; }

        public Vector2 Posicao { get; set; }

        public Vector2 Direcao { get; set; }

        //variaveis

        float velocidade;

        Rectangle quadro;
        Vector2 origem;
        int atualQuadro, larguraQuadro, alturaQuadro;
        float cronometro, intervalo = 25;

        public JogadorSpriteSheet(Game game, Vector2 posicao, int largura, int altura) : base(game)
        {
            Textura = game.Content.Load<Texture2D>("JogadorSprite");
            Posicao = posicao;
            Direcao = new Vector2();
            velocidade = 150.0f;
            alturaQuadro = altura;
            larguraQuadro = largura;
        }


        public override void Update(GameTime gameTime)
        {

            //aceleração para movimento

            Posicao += Direcao * velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // mapear o quadro (frame)
            quadro = new Rectangle(atualQuadro * larguraQuadro, 0, larguraQuadro, alturaQuadro);
            // eixo central
            origem = new Vector2(quadro.Width / 2, quadro.Height / 2);
            // animacao da spritesheet
            Animacao(gameTime);


            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Textura, Posicao, Color.White);
            // desenhar a animacao
            spriteBatch.Draw(Textura, Posicao, quadro, Color.White, 0f, origem, 1.0f, SpriteEffects.None, 0f);
        }

        public Rectangle VerificarColisao()
        {
            return new Rectangle(
                (int)Posicao.X,
                (int)Posicao.Y,
                Textura.Width,
                Textura.Height);
        }

        public void Animacao(GameTime gameTime)
        {
            // tempo real
            cronometro += (float)gameTime.ElapsedGameTime.Milliseconds / 6;
            // Verificacao do intervalo em funcao do cronometro
            if (cronometro > intervalo)
            {
                // atualizando frame atual
                atualQuadro++;

                // reinicia o cronometro
                cronometro = 0f;

                // voltar para o inicio
                if (atualQuadro > 2)
                {
                    // voltar para o quadro inicio
                    atualQuadro = 0;
                }
            }
        }
    }
}
