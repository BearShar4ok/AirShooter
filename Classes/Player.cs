using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Player
    {
        // Поля
        private Texture2D texture;
        private Vector2 position;
        private int speed;
        private float bulletDelay;
        private Texture2D textureBullet;
        private List<Bullet> bulletList = new List<Bullet>();
        private bool isFire;
        private int score;
        private int health;
        private int framesAmount;
        Animator a;
        // Свойства
        public Rectangle BoundingBox { get; set; }
        public int Score { get { return score; } set { score = value; } }
        public int Health { get { return health; } set { health = value; } }
        public List<Bullet> BulletList { get { return bulletList; } }
        // Конструкторы
        public Player()
        {
            texture = null;
            position = new Vector2(145, 122);
            speed = 5;
            bulletDelay = 60;
            isFire = true;
            health = 10;
            score = 0;
            framesAmount = 8;
        }
        // Методы
        public void Damage(int damage)
        {
            Health -= damage;
        }
        public void AddScores(int score)
        {
            if (score >= 0)
            {
                Score += score;
            }
        }
        public void LoadContent(ContentManager manager)
        {
            //texture = manager.Load<Texture2D>("shipAnimation");
            texture = manager.Load<Texture2D>("shipAnimation");
            textureBullet = manager.Load<Texture2D>("laser");
            a = new Animator(texture, 55, texture.Width / 8, texture.Height, framesAmount, false);
        }
        public void Draw(SpriteBatch brush)
        {
            a.Draw(brush, position);
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Draw(brush);
            }
        }
        public void Update(GameTime gametime)
        {
            BoundingBox = new Rectangle((int)position.X, (int)position.Y,
                texture.Width/framesAmount, texture.Height);
            KeyboardState Key = Keyboard.GetState();
            // стрельба
            if (Key.IsKeyDown(Keys.Space))
            {
                PlayerShoot();
                isFire = false;
            }
            if (Key.IsKeyUp(Keys.Space))
            {
                isFire = true;
            }
            // считывание клавиатуры


            if (Key.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (Key.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            if (Key.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (Key.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            //стенки

            if (position.Y<=2)
            {
                position.Y = 2;
            }
            if (position.X <= -18)
            {
                position.X = -18;
            }
            if (position.X + texture.Width / 8 >= 800)
            {
                position.X = 800 - texture.Width / 8;
            }
            if (position.Y + texture.Height >= 480)
            {
                position.Y = 480 - texture.Height;
            }
            a.Update(gametime);
            UpdateBullets();
        }
        private void UpdateBullets()
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].IsVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
        private void PlayerShoot()
        {
            if (bulletDelay > 0)
            {
                bulletDelay--;
            }
            if (isFire)
            {
                bulletDelay = 0;
            }
            if (bulletDelay <= 0 && bulletList.Count < 20) 
            {
                Vector2 bulpos = new Vector2(position.X + texture.Width/8 - 10,
                    position.Y + texture.Height/2 + 2);
                Bullet bullet = new Bullet(textureBullet, bulpos);
                bullet.Speed = 5;
                if (bulletList.Count < 20)
                {
                    bulletList.Add(bullet);
                }
            }
            if (bulletDelay <= 0)
            {
                bulletDelay = 60;
            }
        }
    }
}
