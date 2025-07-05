using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Stronghold_Crusader_Project.Other;

namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private EventLogger Logger;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private string ErrorMessage;
    bool crash = false;

    public Game1(EventLogger LoggerPass)
    {
        Logger = LoggerPass;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 6.0);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Logger.StartEventLog();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (!crash)
        {
            if (Program.LatestException == null)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                Random RNG = new Random();
                int temp = RNG.Next(1, 4);
                switch (temp)
                {
                    case 1:
                        Logger.LogEvent("Troop Deployed", EventLogger.LogType.Info);
                        break;
                    case 2:
                        Logger.LogEvent("map debug", EventLogger.LogType.Debug);
                        break;
                    case 3:
                        Program.LatestException = new Exception("Simulate a crash");
                        break;
                        
                }
                base.Update(gameTime);
                // TODO: Add your update logic here
                
            }
            else
            {
                ErrorMessage = $"A fatal error has occured on {Program.LatestException.StackTrace}: {Program.LatestException.Message}";
                Program.LatestException = null;
                crash = true;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        // TODO: Add your drawing code here
        if (crash == true)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
            _spriteBatch.DrawString(Font, ErrorMessage, new Vector2(10, 10), Color.Red);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
