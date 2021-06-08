using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes
{
    class Animator
    {
        // поля
        private Texture2D texture;
        private float timer;
        private float interval;
        private Vector2 origin;
        private Rectangle sourceRectangle;
        private int currentFrame;
        private int framesAmount;
        private int spriteWidth;
        private int spriteHeight;
        private bool isOnes;
        public bool IsVisible { get; set; }

        public Animator(Texture2D texture, float intervalTime, int spriteWidth, int spriteHeight, int framesAmount, bool isOnes)
        {
            this.isOnes = isOnes;
            this.texture = texture;
            this.timer = 0;
            IsVisible = true;
            this.interval = intervalTime;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.framesAmount = framesAmount;
            currentFrame = 1;
        }
        public void Draw(SpriteBatch brush, Vector2 position)
        {
            if (IsVisible)
            {
                brush.Draw(texture, position, sourceRectangle, Color.White,
               0, origin, 1, SpriteEffects.None, 0);
            }
           
        }
        public void Update(GameTime gametime)
        {
            float t = (float)gametime.ElapsedGameTime.TotalMilliseconds;
            timer += t;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
            }
            if (currentFrame == framesAmount)
            {
                currentFrame = 0;
                if (isOnes)
                {
                    IsVisible = false;
                }
            }
            sourceRectangle = new Rectangle(spriteWidth * currentFrame, 0, spriteWidth, spriteHeight);
        }
    }
}
