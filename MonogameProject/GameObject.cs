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
        private float timeElapsed;
        private int currentIndex;
        protected float speed;
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        protected Vector2 position;
        protected float fps;
        protected float rotation;
        protected int health;

        public int Health { get => health; private set => health = value; }

        //public Texture2D Sprite { get => sprite; private set => sprite = value; }

        public abstract void LoadContent(ContentManager content);
        //{
        //    Sprite = content.Load<Texture2D>("Sprites\\PlayerAnimation\\PlayerNormal\\Forward\\1fwd");
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, new Vector2(sprite.Width/2, sprite.Height/2), 1f, SpriteEffects.None, 0f);
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
        
    }
}
