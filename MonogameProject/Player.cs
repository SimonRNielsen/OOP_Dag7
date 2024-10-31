using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MonogameProject
{
    class Player : GameObject
    {
        public int SpriteHeight;
        protected GraphicsDeviceManager _graphics;
        public Texture2D laserSprite;
        protected bool canShoot = true;
        protected SoundEffect laserPewPew;
        protected SoundEffect takeDamage;
        protected SoundEffect playerDead;
        protected float timer = 1;
        protected float dmgTaken;
        protected bool recievedDmg = false;

        public Player(GraphicsDeviceManager graphics)
        {
            this._graphics = graphics;
            this.position.X = (_graphics.PreferredBackBufferWidth / 2);
            this.fps = 15f;
            this.health = 3;
            this.speed = 200f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];
            laserSprite = content.Load<Texture2D>($"Sprites\\Lasers\\laserGreen08");
            string path = "Sprites\\PlayerAnimation\\PlayerNormal\\Forward\\";
            laserPewPew = content.Load<SoundEffect>("Sounds\\sfx_laser2");
            takeDamage = content.Load<SoundEffect>("Sounds\\sfx_shieldUp");
            playerDead = content.Load<SoundEffect>("Sounds\\sfx_twoTone");

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"{path}{(i + 1)}fwd");
            }

            this.sprite = sprites[0];
            this.SpriteHeight = sprite.Height / 2;
            this.position.Y = (_graphics.PreferredBackBufferHeight - (sprite.Height / 2));

        }

        public override void OnCollision(GameObject other)
        {
            this.health--;
            if (Health == 0)
            {
                GameWorld.playerHealth--;
                playerDead.Play();
            }
            takeDamage.Play();
            recievedDmg = true;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animate(gameTime);

            this.dmgTaken += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (recievedDmg)
            {
                this.color = Color.Red;
                this.dmgTaken = 0f;
                this.recievedDmg = false;
            }

            if (dmgTaken > timer)
            {
                this.color = Color.White;
            } 

        }

        protected virtual void HandleInput()
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
            if (keyState.IsKeyDown(Keys.Space) && canShoot)
            {
                canShoot = false;
                GameWorld.InstantiateGameObject(new Laser(this));
                //GameWorld.InstantiateGameObject(new Laser(position, laserSprite));
                laserPewPew.Play();
            }
            if (keyState.IsKeyUp(Keys.Space) && !canShoot)
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
