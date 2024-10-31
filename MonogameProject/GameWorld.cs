using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection.Metadata;

namespace MonogameProject
{
    public class GameWorld : Game
    {
        private Random random = new Random();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsUpdater = new List<GameObject>();
        private Texture2D sprite;
        public Texture2D collisionTexture;
        private Rectangle rectangle;
        private float spawnTimer = 5f;
        private float timer;
        private bool keepPlaying;
        private bool playerAlive = true;
        private SpriteFont gameWorldFont;
        public static int killCount = 0;
        public static int playerHealth;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            gameObjects.Add(new Player(_graphics));
            //gameObjects.Add(new Player2(_graphics));

            gameObjects.Add(new Background(_graphics, 0));
            gameObjects.Add(new Background(_graphics, 1));
            gameObjects.Add(new Background(_graphics, 2));
            gameObjects.Add(new Background(_graphics, 3));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            collisionTexture = Content.Load<Texture2D>("pixel");
            gameWorldFont = Content.Load<SpriteFont>("Game");

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keepPlaying = false;
            // TODO: Add your update logic here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);

                if (gameObject is Player)
                {
                    keepPlaying = true;
                    playerHealth = gameObject.Health;
                }

                foreach (GameObject other in gameObjects)
                {
                    if (gameObject is Player && other is Enemy)
                    {
                        gameObject.CheckCollision(other);
                        other.CheckCollision(gameObject);
                    }
                    if (gameObject is Laser && other is Enemy)
                    {
                        gameObject.CheckCollision(other);
                        other.CheckCollision(gameObject);
                    }
                }

            }

            if (!keepPlaying && timer >= spawnTimer)
            {
                Exit();
            }

            if (!keepPlaying && playerAlive)
            {
                playerAlive = false;
                timer = 0f;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= spawnTimer && keepPlaying)
            {
                timer = 0f;

                gameObjectsUpdater.Add(new Enemy(_graphics));

            }

            foreach (GameObject gameObject in gameObjectsUpdater)
            {
                gameObject.LoadContent(Content);
                gameObjects.Add(gameObject);
            }
            gameObjectsUpdater.Clear();

            gameObjects.RemoveAll(gameObject => gameObject.Health < 1);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
#if DEBUG
                DrawCollisionBox(gameObject);
#endif
            }
            if (!playerAlive)
            {
                _spriteBatch.DrawString(gameWorldFont, $"GAME OVER!\nScore {killCount}", new Vector2((_graphics.PreferredBackBufferWidth/2) - 50, (_graphics.PreferredBackBufferHeight/2) - 29), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            else
            {
                _spriteBatch.DrawString(gameWorldFont, $"Score {killCount}", Vector2.Zero, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                _spriteBatch.DrawString(gameWorldFont, $"Health {playerHealth}", new Vector2(0, _graphics.PreferredBackBufferHeight - 30), Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCollisionBox(GameObject gameObject)
        {
            Rectangle collisionBox = gameObject.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1f);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1f);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1f);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public static void InstantiateGameObject(GameObject gameObject)
        {
            gameObjectsUpdater.Add(gameObject);
        }
    }
}
