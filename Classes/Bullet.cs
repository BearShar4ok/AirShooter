using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Bullet
    {
        // поля
        private Vector2 position;
        private Texture2D texture;
        private int speed;
        private Rectangle boundingBox;
        private bool isVisible;
        private Rectangle rec;
        // свойства
        public int Speed { set { speed = value; } }
        public bool IsVisible { get { return isVisible; } }
        public Rectangle BoundingBox { get { return boundingBox; } }
        // конструктор
        public Bullet(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            speed = 0;
            isVisible = true;
            rec = new Rectangle((int)position.X, (int)position.Y, 15, 15);
        }
        // методы
        public void Draw(SpriteBatch brush)
        {
            if (isVisible)
            {
                brush.Draw(texture, rec, Color.White);
            }
        }

        public void Update()
        {
            position.X += speed;
            rec = new Rectangle((int)position.X, (int)position.Y, 15, 15);
            if (position.X >= texture.Height + 10 + 800)
            {
                isVisible = false;
            }
            boundingBox = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
        }
    }
}
