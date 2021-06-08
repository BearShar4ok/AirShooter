using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;


namespace AirWar.Classes.UI
{
    class GameOver
    {
        private Texture2D texture;
        private bool isReset = false;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        public bool IsReset { get { return isReset; } }

        public Vector2 position;

        public GameOver()
        {
            position = new Vector2(350, 250);
        }
        public void Update()
        {
            keyboard = Keyboard.GetState();
            if (prevKeyboard.IsKeyDown(Keys.Enter) && (keyboard != prevKeyboard))
            {
                if (keyboard.IsKeyUp(Keys.Enter))
                {
                    Game1.gameState = GameState.Menu;
                    isReset = true;
                }
            }
            prevKeyboard = keyboard;
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("endMenu");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, new Vector2(0,0), Color.White);
        }
    }
}
