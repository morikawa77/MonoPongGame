using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongGame
{
    public class Bola: GameComponent
    {
        //propriedades
        public Texture2D Textura { get; set; }

        public Vector2 Posicao { get; set; }

        public Vector2 Direcao { get; set; }

        //variaveis
        float velocidade;
        float angulo;

        public Bola(Game game, Vector2 posicao): base(game)
        {
            Textura = game.Content.Load<Texture2D>("bola");
            Posicao = posicao;
            Direcao = new Vector2(1.0f,1.0f);
            velocidade = 150.0f;
            angulo = 0;
        }


        public override void Update(GameTime gameTime)
        {

            //aceleração para movimento
            Posicao += Direcao * velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
            angulo += 0.30f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(Textura , Posicao, Color.White);

            // desenhar com rotacao
            // eixo de rotacao
            Vector2 eixoPosicaoRotacao = new Vector2(Textura.Width / 2, Textura.Height / 2);
            Rectangle rotacaoRetangulo = new Rectangle(0, 0, Textura.Width, Textura.Height);
            spriteBatch.Draw(Textura, Posicao, rotacaoRetangulo, Color.White, angulo, eixoPosicaoRotacao, 1.0f, SpriteEffects.None, 1);

        }

        // metodo da colisao
        public Rectangle VerificarColisao()
        {
            return new Rectangle(
                (int)Posicao.X,
                (int)Posicao.Y,
                Textura.Width,
                Textura.Height);
        }

        public void AumentarVelocidade(int indice)
        {
            // velocidade por proporcao
            // velocidade += velocidade / 2;
            if (velocidade < 450.0f)
            {
                velocidade += velocidade / indice;
            }    
        }
    }
}
