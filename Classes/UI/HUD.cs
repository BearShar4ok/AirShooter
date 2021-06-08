using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace AirWar.Classes.UI
{
    class HUD
    {
        //score
        private Label lblScore;
        //helthbar
        private Bar healthBar;
        //hud
        private bool isVisible;

        public HUD()
        {
            lblScore = new Label("5", new Vector2(530, 435), Color.Tomato);
            isVisible = true;
            healthBar = new Bar(null, new Vector2(530, 460), Color.White, 10, 25, 10);
        }
        public void LoadContent(ContentManager manager)
        {
            lblScore.LoadContent(manager);
            healthBar.LoadContent(manager);
        }
        public void Update(int score, int health)
        {
            lblScore.Text = score.ToString();
            healthBar.Update(health);
        }
        public void Draw(SpriteBatch brush)
        {
            if (isVisible)
            {
                lblScore.Draw(brush);
                healthBar.Draw(brush);
            }
        }
    }
}
