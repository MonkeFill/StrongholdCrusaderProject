namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private MapHandler Mapping;
    private Borders BorderHandler;
    float RotationCooldownTime = 1f;
    float RotationCoolDown = 0f;
    Texture2D TempPixel;
    enum GameState
    {
        StartMenu,
        GamePicking,
        GamePreview,
        PausedMenu,
        LoadGame,
        SaveMenu,
        SettingsMenu,
        ErrorCatch
    }

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
    }

    protected override void Initialize()
    {
        Window.IsBorderless = true;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.ApplyChanges();
        StartEventLog();
        Mapping = new MapHandler(Content);
        Mapping.MapImportHandler("ValidMap");
        Camera2D.Initialize(GraphicsDevice.Viewport);
        BorderHandler = new Borders(Content);
        InitializeDefaultKeybinds();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        TempPixel = new Texture2D(GraphicsDevice, 1, 1);
        TempPixel.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    { 
        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        RotationCoolDown -= (float)deltaTime;

        if (deltaTime > 0)
        {
            double FPS = 1.0 / deltaTime;
            //Console.WriteLine($"FPS - {FPS:F2}");
        }
        UpdateInputManager(gameTime);
        /*if (RotationCoolDown <= 0)
        {
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                Action = CameraAction.Rotate;
                RotationChange = 1;
                RotationCoolDown = RotationCooldownTime;
            }
            else if (keyboardState.IsKeyDown(Keys.E))
            {
                Action = CameraAction.Rotate;
                RotationChange = -1;
                RotationCoolDown = RotationCooldownTime;
            }
        }*/
        base.Update(gameTime);
        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        _spriteBatch.Begin(transformMatrix: Camera2D.GetViewMatrix());
        Mapping.DrawMap(_spriteBatch);
        BorderHandler.Draw(_spriteBatch);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
