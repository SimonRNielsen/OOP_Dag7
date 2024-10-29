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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> gameObjectsUpdater = new List<GameObject>();
        private Texture2D sprite;
        private Rectangle rectangle;
        private DateTime timer;
        private DateTime startTime;
        private Texture2D startSprite;

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
            startTime = DateTime.Now;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
                startSprite = Content.Load<Texture2D>("Sprites\\PlayerAnimation\\PlayerNormal\\Forward\\1fwd");
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            timer = DateTime.Now;
            //List<GameObject> gameObjectsUpdater = gameObjects;
            // TODO: Add your update logic here
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
            if (timer.Second >= (startTime.Second + 5))
            {
                startTime = DateTime.Now;
                gameObjectsUpdater.Add(new Player(_graphics, startSprite));
                foreach (GameObject gameObject in gameObjectsUpdater)
                {
                    gameObjects.Add(gameObject);
                    gameObject.LoadContent(Content);
                }
                gameObjectsUpdater.Clear();
            }
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
