using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;


namespace AirWar.Classes.UI
{
    class Menu
    {
        private List<Label> items;
        private Texture2D texture;
        private string[] texts = { "play", "info", "exit" };
        private int selected = 0;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;

        public Vector2 Position { get; set; }

        public Menu()
        {
            items = new List<Label>();

            Vector2 position = Position;
            for (int i = 0; i < texts.Length; i++)
            {
                position.Y += 30;
                Label l = new Label(texts[i], position, Color.Gold);
                items.Add(l);
            }
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("mainMenu");
            foreach (Label item in items)
            {
                item.LoadContent(manager);
            }
        }
        public void Update()
        {
            keyboard = Keyboard.GetState();
            if ((keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                && (keyboard != prevKeyboard) && selected < items.Count - 1)
            {
                items[selected].ResetColor();
                selected++;
            }
            if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                && (keyboard != prevKeyboard) && selected > 0)
            {
                items[selected].ResetColor();
                selected--;
            }
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                switch (selected)
                {
                    case 0: //play
                        Game1.gameState = GameState.Game;
                        break;
                    case 1: //info
                        Game1.gameState = GameState.Info;
                        break;
                    case 2: //exit
                        Game1.gameState = GameState.Exit;
                        break;
                    default:
                        break;
                }
            }
            prevKeyboard = keyboard;
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, new Vector2(0,0), Color.White);
            items[selected].Color = Color.Lime;
            foreach (Label item in items)
            {
                item.Draw(brush);
            }
        }
        public void SetMenuPosition(Vector2 Position)
        {
            this.Position = Position;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Position = Position;
                Position.Y += 30;
            }
        }
    }
}
