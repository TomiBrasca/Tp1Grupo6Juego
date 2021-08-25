using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Test1
{
    public class Sprite : ICloneable
    {
        protected Texture2D _texture { get; set; }
        
        public int _imgWidth;
        public int _imgHeight;
        public Vector2 _position;
        public Vector2 Velocity;
        public Vector2 _direction;
        public float Speed = 6f;
        protected float _rotation;
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;
        public Sprite Parent;
        public int score;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }
        public float lifeSpan = 0f;
        public bool IsRemoved = false;
        //public float _rotation;
        //public int rotationVelocity = 3;
        public Vector2 Origin;
        public float _scale;

        public Sprite(Texture2D texture,float scale)
        {
            _texture = texture;
            _imgWidth = texture.Width/2;
            _imgHeight = texture.Height / 2;
            Origin = new Vector2(_texture.Width/2, _texture.Height/3);
            _scale = scale;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites, int limitX, int limitY)
        {
           
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, _rotation, Origin, _scale, SpriteEffects.None, 0);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion

    }
}
