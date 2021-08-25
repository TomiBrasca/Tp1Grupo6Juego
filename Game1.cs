using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test1
{
    public class Game1 : Game
    {
        private Random rdn = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteFont _spParaTest;
        private int DIFFICULTY = 4;
        private int countEnemysDefeated = 0;
        private List<Sprite> _sprites;
        private Texture2D enemyTexture;
        private Camera newCamera;
        private Sprite background;
        public static int ScreenHeigth;
        public static int ScreenWidth;
        private float spawn;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;

            ScreenHeigth = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Contiene metodos para dibujar
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Texturas
            var boxTexture = Content.Load<Texture2D>("ship");
            var vidaTexture = Content.Load<Texture2D>("vida");
            enemyTexture = Content.Load<Texture2D>("enemy2");
            _spParaTest = Content.Load<SpriteFont>("pixelFont");
            
            //Fondo
            background = new Sprite(Content.Load<Texture2D>("back3"), 1f);
            background.Origin = new Vector2(0,background._imgHeight);
            
            //Camara
            newCamera = new Camera();
            
            //Sprites el juego
            _sprites = new List<Sprite>()
            {
                new Prota(boxTexture, vidaTexture, enemyTexture)
                {
                    _position = new Vector2(ScreenWidth/2,background._imgHeight-200),
                    Bullet = new Bullet(Content.Load<Texture2D>("bullet4")),
                },
            };
        }

       


        protected override void Update(GameTime gameTime)
        {
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach (var sprite in _sprites.ToArray())
            {
                loadEnemys(enemyTexture);
                sprite.Update(gameTime, _sprites, ScreenWidth, background._imgHeight);
                // _sprites[0] es Player
                newCamera.Follow(_sprites[0]);
            }
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }

            countEnemysDefeated = _sprites[0].score;
 
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            _spriteBatch.Begin(transformMatrix: newCamera.Transform);
            background.Draw(_spriteBatch);
            
            foreach (var sprite in _sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            _spriteBatch.Begin();
            foreach (var sprite in Prota.health)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(_spParaTest, Convert.ToString(countEnemysDefeated), new Vector2(10, 10), Color.White);

            _spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
        // Carga enemigos en el juego
        public void loadEnemys(Texture2D enemyTexture)
        {
            if (spawn >= 1.2)
            {
                spawn = 0;
                if (countEnem() < DIFFICULTY)
                {
                    Enemy testEnemy = new Enemy(enemyTexture, rdn.Next(0, 1200 - enemyTexture.Width), rdn.Next(-600, -280 - enemyTexture.Height), 0.75f);
                    _sprites.Add(testEnemy);
                }
            }
        }

        // Cuenta los enemigos del juego
        public int countEnem()
        {
            int countEnemys = 0;
            foreach (var sprite in _sprites)
            {
                if (sprite is Enemy)
                {
                    countEnemys++;
                }
            }
            return countEnemys;
        }

    }
}
