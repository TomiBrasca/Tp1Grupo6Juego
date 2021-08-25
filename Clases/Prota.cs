using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test1
{
    public class Prota : Sprite
    {
        public Bullet Bullet;
        public static List<Sprite> health;
        public Texture2D enemyTexture;
        public Random rdn = new Random();
        public Prota(Texture2D texture, Texture2D vida, Texture2D enemyTxt)
            :base(texture, 0.75f)
        {
            enemyTexture = enemyTxt;
            health = new List<Sprite>()
            {
                new Sprite(vida, 0.5f)
                {
                _position = new Vector2(30, 80),
                },
                new Sprite(vida, 0.5f)
                {
                    _position = new Vector2(30, 100),
                },
                new Sprite(vida, 0.5f)
                {
                    _position = new Vector2(30, 120),
                },
            };
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites, int limitX, int limitY)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.D) && (_position.X <= limitX - _imgWidth / 2))
            {
                Velocity.X += Speed;
            }

            else if (currentKey.IsKeyDown(Keys.A) && (_position.X >= _imgWidth / 2))
            {
                Velocity.X -= Speed;

            }
            else if (currentKey.IsKeyDown(Keys.W))
            {
                Velocity.Y -= Speed;

            }
            else if (currentKey.IsKeyDown(Keys.S) && (_position.Y <= limitY - _imgHeight))
            {
                Velocity.Y += Speed;

            }


            _direction = new Vector2(0, -1);


            if (currentKey.IsKeyDown(Keys.Space) &&
                previousKey.IsKeyUp(Keys.Space))
            {
                addBullet(sprites);
            }


            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))
                {
                    sprite.IsRemoved = true;
                    if (!(sprite is Bullet))
                    {
                        if (health.Count > 0)
                            health.RemoveAt(health.Count - 1);

                    }
                }

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                {
                    sprite.IsRemoved = true;
                    if (!(sprite is Bullet))
                    {
                        if (health.Count > 0)
                            health.RemoveAt(health.Count - 1);
                    }
                }
            }
            _position += Velocity;
            Velocity = Vector2.Zero;
        }

        private void addBullet(List<Sprite> sprites)
        {
            Bullet bullet = Bullet.Clone() as Bullet;
            bullet._direction = this._direction;
            bullet._position = this._position;
            bullet.Speed = this.Speed;
            bullet.lifeSpan = 2f;
            bullet.Parent = this;
            sprites.Add(bullet);
        }


    }
}
