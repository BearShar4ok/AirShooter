using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes.UI
{
    class Bar
    {
        private Texture2D texture;
        private Vector2 position;
        private Color color;
        private int size;
        private int width;
        private int widthSection;
        private Rectangle sourseSectangle;
        private int height;
        public Bar(Texture2D texture, Vector2 position, Color color,
            int size, int sectionWidth, int height)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.size = size;
            this.widthSection = sectionWidth;
            this.width = sectionWidth * size;
            this.height = height;
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("hbar");
        }
        public void Update(int size)
        {
            this.size = size;
            this.width = this.widthSection * size;
        }
        public void Draw(SpriteBatch brush)
        {
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                width, height);
            sourseSectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            brush.Draw(texture,
                destinationRectangle, sourseSectangle, color);
        }
    }
}
