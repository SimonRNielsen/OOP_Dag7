using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameProject
{
    internal class Player2 : Player
    {
        public Player2(GraphicsDeviceManager graphics) : base(graphics)
        {
            _graphics = graphics;
        }

        protected override void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                if (position.Y < 64) { }
                else
                {
                    velocity += new Vector2(0, -1);
                }
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                if (position.Y > (_graphics.PreferredBackBufferHeight) - 32) { }
                else
                {
                    velocity += new Vector2(0, 1);
                }
            }
            if (keyState.IsKeyDown(Keys.Left))
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
            if (keyState.IsKeyDown(Keys.Right))
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
            if (keyState.IsKeyDown(Keys.L) && canShoot)
            {
                canShoot = false;
                GameWorld.InstantiateGameObject(new Laser(this));
            }
            if (keyState.IsKeyUp(Keys.L) && !canShoot)
            {
                canShoot = true;
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }
    }
}
