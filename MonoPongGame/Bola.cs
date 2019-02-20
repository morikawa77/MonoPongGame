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

        public Bola(Game game, Vector2 posicao): base(game)
        {
            Textura = game.Content.Load<Texture2D>("bola");
            Posicao = posicao;
            Direcao = new Vector2(1.0f,1.0f);
            velocidade = 150.0f;
        }


        public override void Update(GameTime gameTime)
        {

            //aceleração para movimento

            Posicao += Direcao * velocidade * (float)gameTime.ElapsedGameTime.TotalSeconds;
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textura , Posicao, Color.White);
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
