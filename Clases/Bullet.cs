using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test1
{
    public class Bullet : Sprite
    {

        public float Timer;

        public Bullet(Texture2D texture)
        : base(texture, 1f)
        {
           
        }


        public override void Update(GameTime gameTime, List<Sprite> sprites, int limitX, int limitY)
        {
            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer > lifeSpan)
            {
                IsRemoved = true;
            }

            Velocity +=_direction * Speed * 2;

            foreach (var sprite in sprites)
            {
                if (sprite == this || sprite == this.Parent)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)) ||
                    (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                {
                    sprite.IsRemoved = true;
                    this.Parent.score += 100;
                }
                    
            }

            _position += Velocity;
            Velocity = Vector2.Zero;


        }
        

    }
}
