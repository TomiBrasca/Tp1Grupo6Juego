using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Test1
{
    
    public class Enemy : Sprite
    {
        public Enemy(Texture2D texture, int randomX, int randomY, float reSize)
            : base(texture, reSize)
        {
            int x = randomX;
            int y = randomY;
            _position = new Vector2(x, y);
            
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites, int limitX, int limitY)
        {

            //obtengo la posicion del jugador
            _direction = sprites[0]._position - _position;
            _direction.Normalize();
            // convierto el vector de 0 a 1
            Velocity = _direction * (Speed/4);
            _position += Velocity;
            
            _position += Velocity;
            Velocity = Vector2.Zero;


        }
    }
}
