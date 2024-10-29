using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameProject
{
    class Player : GameObject
    {
        private float speed = 200f;
        private Vector2 velocity;
        private GraphicsDeviceManager _graphics;
                
        public Player(GraphicsDeviceManager graphics)
        {
            this._graphics = graphics;
            this.position.X = (_graphics.PreferredBackBufferWidth / 2) - (64 / 2);
            this.position.Y = (_graphics.PreferredBackBufferHeight - 64);
            this.fps = 15f;
            this.health = 100;
        }

        /*
        public Player(GraphicsDeviceManager graphics, Texture2D sprite)
        {
            this._graphics = graphics;
            this.position.X = (_graphics.PreferredBackBufferWidth / 2) - (64 / 2);
            this.position.Y = (_graphics.PreferredBackBufferHeight - 64);
            this.fps = 15f;
            this.sprite = sprite;
        }
        */

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];
            string path = "Sprites\\PlayerAnimation\\PlayerNormal\\Forward\\";

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"{path}{(i + 1)}fwd");
            }

            this.sprite = sprites[0];
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animate(gameTime);
        }

        private void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                if (position.Y < 64) { }
                else
                {
                    velocity += new Vector2(0, -1);
                }
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                if (position.Y > (_graphics.PreferredBackBufferHeight) - 32) { }
                else
                {
                    velocity += new Vector2(0, 1);
                }
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                if (position.X < (0 - this.sprite.Width))
                {
                    this.position.X = _graphics.PreferredBackBufferWidth + 64;
                }
                else
                {
                    velocity += new Vector2(-1, 0);
                }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                if (position.X > _graphics.PreferredBackBufferWidth + 64)
                {
                    this.position.X = (0 - this.sprite.Width);
                }
                else
                {
                    velocity += new Vector2(1, 0);
                }
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }

        private void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }

    }
}
