using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameProject
{
    internal class Enemy : GameObject
    {
        private GraphicsDeviceManager _graphics;
        private float speed;
        private int currentIndex;
        private int randomSpriteGroup;
        private int randomSpriteNumber;

        public Enemy(GraphicsDeviceManager graphics, Random random)
        {
            _graphics = graphics;
            this.speed = (float)random.Next(80, 160);
            this.position.X = (float)random.Next(0, (_graphics.PreferredBackBufferWidth - 100));
            this.position.Y -= 100; 
            this.randomSpriteGroup = random.Next(1, 6);
            this.randomSpriteNumber = random.Next(1, 6);
            this.fps = 15f;
            this.Health = 10;
        }

        public override void LoadContent(ContentManager content)
        {
            string path;

            if (this.randomSpriteGroup == 5)
            {
                if (this.randomSpriteNumber == 5)
                {
                    this.randomSpriteNumber = 4;
                }
                path = $"Sprites\\Meteors\\meteorGrey_big";
            }
            else
            {
                path = $"Sprites\\Enemies\\enemy{(SpriteGroup)this.randomSpriteGroup}";
            }

            this.sprite = content.Load<Texture2D>($"{path}{this.randomSpriteNumber}");

        }

        public override void Update(GameTime gameTime)
        {
            if (this.position.Y > (_graphics.PreferredBackBufferHeight + (this.sprite.Height / 2)))
            {
                this.Health = 0;
            }
            Move(gameTime);
        }

        public void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.position.Y += (this.speed * deltaTime);
            if (this.randomSpriteGroup == 5)
            {
                this.rotation += 0.07f;
            }
        }
    }

    public enum SpriteGroup : int
    {
        Black = 1,
        Blue = 2,
        Green = 3,
        Red = 4
    }
}
