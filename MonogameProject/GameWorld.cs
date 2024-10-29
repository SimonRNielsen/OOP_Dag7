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
        private List<GameObject> gameObjectsUpdater = new List<GameObject>();
        private Texture2D sprite;
        private Rectangle rectangle;
        private float spawnTimer = 5f;
        private float timer;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            gameObjects.Add(new Player(_graphics));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= spawnTimer)
            {
                timer = 0f;

                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    gameObjectsUpdater.Add(new Enemy(_graphics, random));
                }

                foreach (GameObject gameObject in gameObjectsUpdater)
                {
                    gameObject.LoadContent(Content);
                    gameObjects.Add(gameObject);
                }

                gameObjectsUpdater.Clear();

            }

            gameObjects.RemoveAll(c => c.Health == 0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
