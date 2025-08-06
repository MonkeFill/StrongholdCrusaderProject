namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private MapHandler Mapping;
    float RotationCooldownTime = 1f;
    float RotationCoolDown = 0f;
    Texture2D TempPixel;
    static readonly int MapTotalHeight = GlobalConfig.MapTotalHeight;
    static readonly int MapTotalWidth = GlobalConfig.MapTotalWidth;
    static readonly int BorderHeight = GlobalConfig.BorderHeight;
    static readonly int BorderWidth = GlobalConfig.BorderWidth;
    static readonly int MaxMapHeight = GlobalConfig.MaxMapHeight;
    static readonly int MaxMapWidth = GlobalConfig.MaxMapWidth;
    static readonly int TileWidth = GlobalConfig.TileWidth;
    static readonly int TileHeight = GlobalConfig.TileHeight;
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
        Mapping.MapImportHandler("Map1");
        Camera2D.Initialize(GraphicsDevice.Viewport);
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
        
        
        CameraAction Action = CameraAction.None;
        KeyboardState keyboardState = Keyboard.GetState();
        Vector2 PositionChange = Vector2.Zero;
        float ZoomChange = 0f;
        float  RotationChange = 0f;
        if (keyboardState.IsKeyDown(Keys.W))
        {
            PositionChange.Y--;
            Action = CameraAction.Move;
        }
        else if (keyboardState.IsKeyDown(Keys.S))
        {
            PositionChange.Y++;
            Action = CameraAction.Move;
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            PositionChange.X--;
            Action = CameraAction.Move;
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
            PositionChange.X++;
            Action = CameraAction.Move;
        }
        if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            Action = CameraAction.Zoom;
            ZoomChange = 1; 
        }
        else if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            Action = CameraAction.Zoom;
            ZoomChange = -1; 
        }
        if (RotationCoolDown <= 0)
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
        }
        UpdateCamera(gameTime, Action, PositionChange, RotationChange, ZoomChange);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        _spriteBatch.Begin(transformMatrix: Camera2D.GetViewMatrix(), samplerState: SamplerState.PointClamp);
        Mapping.DrawMap(_spriteBatch);
        _spriteBatch.Draw(TempPixel, new Rectangle(TileWidth / 2,-BorderHeight + (TileHeight / 2), MaxMapWidth - (BorderWidth * 2) - (TileWidth / 2), BorderHeight), Color.Red); //Top
        _spriteBatch.Draw(TempPixel, new Rectangle(-BorderWidth + (TileWidth / 2),TileHeight / 2 ,BorderWidth, MaxMapHeight - (BorderHeight * 2) - TileHeight / 2), Color.Pink); //Left
        _spriteBatch.Draw(TempPixel, new Rectangle(TileWidth / 2 ,MaxMapHeight - (BorderHeight * 2), MaxMapWidth - (BorderWidth * 2) - (TileWidth / 2), BorderHeight), Color.BlueViolet); //Bottom
        _spriteBatch.Draw(TempPixel, new Rectangle(MaxMapWidth - (BorderWidth * 2),TileHeight / 2, BorderWidth, MaxMapHeight - (BorderHeight * 2)), Color.Orange); //Right
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
