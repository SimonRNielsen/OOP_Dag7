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
    internal abstract class GameObject
    {
        private int currentIndex;
        protected int health;
        private float timeElapsed;
        protected float speed;
        protected float fps;
        protected float rotation;
        protected float scale = 1f;
        protected float layer = 1f;
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Rectangle collisionBox;


        public int Health { get => health; private set => health = value; }

        public Rectangle CollisionBox
        {
            get { return new Rectangle((int)position.X-(sprite.Width/2), (int)position.Y-(sprite.Height/2), sprite.Width, sprite.Height); }
        }

        public abstract void LoadContent(ContentManager content);
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, new Vector2(sprite.Width/2, sprite.Height/2), scale, SpriteEffects.None, layer);
        }

        public abstract void Update(GameTime gameTime);

        protected void Animate(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentIndex = (int)(timeElapsed * fps);

            sprite = sprites[currentIndex];

            if (currentIndex >= sprites.Length - 1)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }
        }

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }

        public bool IsColliding(GameObject other)
        {
            bool isColliding = false;

            return isColliding;
        }

        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }

    }
}
