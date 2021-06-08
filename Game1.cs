using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using AirWar.Classes;
using AirWar.Classes.UI;
using System;

namespace AirWar
{
    public enum GameState { Menu, Game, Exit, Info, Pause, GameOver }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // Поля игры
        public static GameState gameState = GameState.Menu;
        private Player player;
        private First_background bg1;
        private Second_background bg2;
        private Main_background mbg;
        private GameOver gameOver = new GameOver();
        private List<Mine> mineList;
        private Menu menu = new Menu();
        private List<Explosion> explosionList;
        private Random random;
        private Label lblInfo;
        private HUD hud;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //смена размера экрана
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            random = new Random();
            player = new Player();
            bg1 = new First_background();
            bg2 = new Second_background();
            mbg = new Main_background();
            mineList = new List<Mine>();
            hud = new HUD();
            explosionList = new List<Explosion>();
            lblInfo = new Label("This game has been made with a support Noname Viosagmir", new Vector2(200, 240), Color.Red);
            menu.SetMenuPosition(new Vector2(_graphics.PreferredBackBufferWidth / 2, 200));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.LoadContent(Content);
            bg1.LoadContent(Content);
            bg2.LoadContent(Content);
            mbg.LoadContent(Content);
            menu.LoadContent(Content);
            hud.LoadContent(Content);
            lblInfo.LoadContent(Content);
            gameOver.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            //UpdateMine();
            switch (gameState)
            {
                case GameState.Menu:
                    bg1.Update();
                    bg2.Update();
                    mbg.Update();
                    menu.Update();
                    break;
                case GameState.Game:
                    UpdateGame(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.Menu;
                    }
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.Info:
                    bg1.Update();
                    bg2.Update();
                    mbg.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.Menu;
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    gameOver.Update();
                    if (gameOver.IsReset)
                    {
                        ResetGame();
                    }
                    break;
                default:
                    break;
            }
            
            base.Update(gameTime);
        }
        private void UpdateGame(GameTime gameTime)
        {
            hud.Update(player.Score, player.Health);
            for (int i = 0; i < mineList.Count; i++)
            {
                mineList[i].Update(gameTime);
            }
            // TODO: Add your update logic here
            player.Update(gameTime);
            bg1.Update();
            UpdateMine();
            bg2.Update();
            mbg.Update();
            UpdateExplosions(gameTime);
            for (int i = 0; i < mineList.Count; i++)
            {
                Mine m = mineList[i];
                for (int j = 0; j < player.BulletList.Count; j++)
                {
                    Bullet b = player.BulletList[j];
                    if (m.BoundingBox.Intersects(b.BoundingBox))
                    {
                        mineList.RemoveAt(i);
                        player.BulletList.RemoveAt(j);
                        Explosion e = new Explosion(new Vector2
                            (m.BoundingBox.X - 60, m.BoundingBox.Y - 30));
                        e.LoadContent(Content);
                        explosionList.Add(e);
                        player.AddScores(1);
                        break;
                    }
                }

                if (m.BoundingBox.Intersects(player.BoundingBox))
                {
                    mineList.RemoveAt(i);
                    Explosion e = new Explosion(new Vector2
                            (m.BoundingBox.X - 35, m.BoundingBox.Y));
                    e.LoadContent(Content);
                    explosionList.Add(e);
                    player.Damage(1);
                    if (player.Health <= 0)
                    {
                        gameState = GameState.GameOver;
                    }
                }
            }
        }
        private void ResetGame()
        {
            mineList.Clear();
            player.BulletList.Clear();
            explosionList.Clear();
            player = new Player();
            player.LoadContent(Content);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(_spriteBatch);
                    break;
                case GameState.Game:
                    DrawGame();
                    break;
                case GameState.Exit:
                    break;
                case GameState.Info:
                    mbg.Draw(_spriteBatch);
                    bg1.Draw(_spriteBatch);
                    bg2.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    lblInfo.Draw(_spriteBatch);
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    mbg.Draw(_spriteBatch);
                    bg1.Draw(_spriteBatch);
                    bg2.Draw(_spriteBatch);
                    gameOver.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawGame()
        {
            mbg.Draw(_spriteBatch);
            bg1.Draw(_spriteBatch);
            bg2.Draw(_spriteBatch);

            player.Draw(_spriteBatch);
            for (int i = 0; i < mineList.Count; i++)
            {
                mineList[i].Draw(_spriteBatch);
            }
            DrawExplosions();
            hud.Draw(_spriteBatch);
        }
        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = 0; i < explosionList.Count; i++)
            {
                explosionList[i].Update(gameTime);
                if (!explosionList[i].IsVisible)
                {
                    explosionList.RemoveAt(i);
                    i--;
                }
            }
        }
        private void DrawExplosions()
        {
            foreach (Explosion e in explosionList)
            {
                e.Draw(_spriteBatch);
            }
        }
        private void UpdateMine()
        {
            int max = 10;
            for (int i = 0; i < mineList.Count; i++)
            {
                if (!mineList[i].IsVisible)
                {
                    mineList.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < max - mineList.Count; i++)
            {
                Mine m = new Mine(
                    new Vector2(random.Next(16, 24) * 50,
                random.Next(0, 8) * 50));
                m.LoadContent(Content);
                mineList.Add(m);
            }
        }
    }
}
