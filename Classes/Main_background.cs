using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;
namespace AirWar.Classes
{
    class Main_background
    {
        // Поля
        private Texture2D texture;
        private Vector2 position1;
        private Vector2 position2;
        private int speed;
        // Свойства

        // конструктор
        public Main_background()
        {
            texture = null;
            position1 = new Vector2(0, 0);
            position2 = new Vector2(800, 0);
            speed = 1;
        }
        // методы
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("mbg");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position1, Color.White);
            brush.Draw(texture, position2, Color.White);
        }
        public void Update()
        {
            position1.X -= speed;
            position2.X -= speed;
            if (position1.X <= -800)
            {
                position1.X = 0;
                position2.X = 800;
            }
        }
    }
}
