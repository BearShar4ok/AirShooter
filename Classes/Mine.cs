using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Mine
    {
        // поля
        private Vector2 position;
        private Texture2D texture;
        private Rectangle boundingBox;
        private int speed;
        private bool isVisible;
        Animator a;
        // свойства
        public bool IsVisible { get { return isVisible; } }
        public Rectangle BoundingBox { get { return boundingBox; } }
        // конструкторы
        public Mine(Vector2 pos)
        {
            isVisible = true;
            position = pos;
            speed = 4;
            texture = null;
        }
        // методы
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("mineAnimation");
            a = new Animator(texture, 55, texture.Width / 8, texture.Height, 8, false);
        }
        public void Draw(SpriteBatch brush)
        {
            a.Draw(brush, position);
        }
        public void Update(GameTime gametime)
        {
            position.X -= speed;
            if (position.X < -texture.Width / 8 - 10)
            {
                isVisible = false;
            }
            boundingBox = new Rectangle((int)position.X, (int)position.Y,
                texture.Width / 8, texture.Height);
            a.Update(gametime);
        }
    }
}
