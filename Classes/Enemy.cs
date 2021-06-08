using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Enemy
    {
        protected Texture2D texture;
        private Vector2 position;
        protected int speed;
        private Rectangle boundingBox;
        private bool isVisible;
        private Rectangle destinationRectangle;
        protected float rotation;

        public Enemy(Vector2 pos)
        {
            texture = null;
            isVisible = true;
            rotation = 0;
            position = pos;
            speed = 5;
        }
        public virtual void LoadContent(ContentManager content)
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            Vector2 origin = new Vector2(destinationRectangle.Width / 2,
                destinationRectangle.Height / 2);
            spriteBatch.Draw(texture, destinationRectangle, null,
                Color.White, rotation, origin, SpriteEffects.None, 1);
        }
        public virtual void Update(GameTime gameTime)
        {
            position.Y += speed;
            if (position.Y >= 700)
            {
                isVisible = false;
            }
        }
    }
}
