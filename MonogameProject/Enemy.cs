using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MonogameProject
{
    internal class Enemy : GameObject
    {
        private GraphicsDeviceManager _graphics;
        private int randomSpriteGroup;
        private int randomSpriteNumber;
        private float rotationSpeed;
        SoundEffect die;
        Random random = new Random();

        public Enemy(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            this.health = 4;
            this.velocity.Y = 1f;
        }

        public override void LoadContent(ContentManager content)
        {
            string path;
            sprites = new Texture2D[3];
            for (int i = 0; i < 3; i++)
            {
                this.randomSpriteGroup = random.Next(1, 6);
                this.randomSpriteNumber = random.Next(1, 6);
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
                this.sprites[i] = content.Load<Texture2D>($"{path}{this.randomSpriteNumber}");
                
            }

            die = content.Load<SoundEffect>("Sounds\\sfx_shieldDown");

            Respawn();

        }

        public override void OnCollision(GameObject other)
        {
            Respawn();
            this.health--;
            die.Play();
            GameWorld.killCount++;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.position.Y > (_graphics.PreferredBackBufferHeight + (this.sprite.Height / 2)))
            {
                Respawn();
            }
            if (sprite.Name.Contains("Meteor"))
            {
                this.rotation += rotationSpeed;
            }
            this.Move(gameTime);
        }

        private void Respawn()
        {
            this.randomSpriteGroup = random.Next(1, 6);
            this.randomSpriteNumber = random.Next(1, 6);
            this.sprite = sprites[random.Next(0, 3)];
            this.rotationSpeed = ((float)random.Next(-700, 701) / 10000);
            this.rotation = 0f;
            this.speed = (float)random.Next(80, 160);
            this.position.X = (float)random.Next(50, (_graphics.PreferredBackBufferWidth - 50));
            this.position.Y = 0 - sprite.Height;
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
