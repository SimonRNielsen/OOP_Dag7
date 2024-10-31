using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace MonogameProject
{
    internal class Background : GameObject
    {
        private int backgroundIndex;
        private GraphicsDeviceManager _graphics;
        public Background(GraphicsDeviceManager graphics, int background)
        {
            _graphics = graphics;
            this.velocity.Y = 1f;
            this.speed = 150f;
            this.health = 2;
            this.position.Y = 0;
            //int scaleint = _graphics.PreferredBackBufferWidth / 256;
            this.scale = 8f;
            if (background == 1)
            {
                this.position.Y = -(scale * 256) * background;
            }
            if (background == 2)
            {
                this.position.Y = -(scale * 256) * background;
            }
            if (background == 3)
            {
                this.position.Y = -(scale * 256) * background;
            }
            this.backgroundIndex = background;
            this.layer = 0f;
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];
            this.sprites[0] = content.Load<Texture2D>($"Backgrounds\\black");
            this.sprites[1] = content.Load<Texture2D>($"Backgrounds\\blue");
            this.sprites[2] = content.Load<Texture2D>($"Backgrounds\\darkPurple");
            this.sprites[3] = content.Load<Texture2D>($"Backgrounds\\purple");

            this.sprite = this.sprites[backgroundIndex];
            this.position.X = _graphics.PreferredBackBufferHeight-(sprite.Width/2);
            
        }

        public override void OnCollision(GameObject other)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);
            if (position.Y > _graphics.PreferredBackBufferHeight * 2)
            {
                this.position.Y = -((int)scale * 256) * 3;
            }
        }
    }
}
