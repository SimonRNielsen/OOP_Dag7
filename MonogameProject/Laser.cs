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
    internal class Laser : GameObject
    {
        /*
        public Laser(Vector2 playerPos, Texture2D sprite)
        {
            this.health = 1;
            this.position = playerPos;
            this.position.Y = playerPos.Y - 64;
            this.speed = 300f;
            this.velocity.Y = -1f;
            this.sprite = sprite;
        }
        */

        public Laser(Player player)
        {
            this.health = 1;
            this.position = player.Position;
            this.position.Y = player.Position.Y - (player.SpriteHeight);
            this.speed = 300f;
            this.velocity.Y = -1f;
            this.sprite = player.laserSprite;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {
            this.health--;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.position.Y < -128)
            {
                this.health--;
            }
            this.Move(gameTime);
        }
    }
}
