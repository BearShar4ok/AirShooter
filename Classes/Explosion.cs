using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Explosion
    {
        // поля
        private Texture2D texture;
        private Vector2 position;
        private int speed;
        Animator a;
        // свойства
        public bool IsVisible { get { return a.IsVisible; } }
        public Explosion(Vector2 pos)
        {
            position = pos;
            speed = 3;
        }
        // методы
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("explosion");
            a = new Animator(texture, 60, 134, 134, 12, true);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            a.Draw(spriteBatch, position);
        }
        public void Update(GameTime gametime)
        {
            position.X -= speed;
            a.Update(gametime);
        }
    }
}
