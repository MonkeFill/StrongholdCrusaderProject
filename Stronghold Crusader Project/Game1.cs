namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch MapSpriteBatch;
    private SpriteBatch UISpriteBatch;
    private StartupManager GameManager = new StartupManager();
    private MenuManager Menus;

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
        //Setting the game to full screen borderless
        Window.IsBorderless = true;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.ApplyChanges();
        
        //Starting the game and initialising everything
        GameManager.StartGame(Content);
        MapHandlerInitializer(Content);
        //MapImportHandler("ValidMap.json");
        Camera2D.Initialize(GraphicsDevice.Viewport);
        CreateViewScale(_graphics);
        Menus = new MenuManager(this);
        InputManagerInitialiser(Menus);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        //Load sprite batches
        MapSpriteBatch = new SpriteBatch(GraphicsDevice);
        UISpriteBatch = new SpriteBatch(GraphicsDevice);

    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive) //If the game window is the top window (not tab out)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            if (deltaTime > 0)
            {
                double FPS = 1.0 / deltaTime;
                //Console.WriteLine($"FPS - {FPS:F2}");
            }
            InputManagerUpdate();
            Menus.Update();
            GetTileMousePosition();
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        //Starting sprite batches
        MapSpriteBatch.Begin(transformMatrix: Camera2D.GetViewMatrix(), samplerState: SamplerState.AnisotropicClamp); //Has the camera for the matrix
        if (ScaleUI)
        {
            UISpriteBatch.Begin(transformMatrix: MatrixScale);
        }
        else
        {
            UISpriteBatch.Begin();
        }
        
        DrawMap(MapSpriteBatch);
        Menus.Draw(UISpriteBatch);
        
        //Ending sprite batches
        MapSpriteBatch.End();
        UISpriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void CreateViewScale(GraphicsDeviceManager Graphics)
    {
        //Getting the scale against the monitor sides
        float ScaleY = (float)Graphics.PreferredBackBufferHeight / VirtualHeight;
        float ScaleX = (float)Graphics.PreferredBackBufferWidth / VirtualWidth;
        float Scale = Math.Min(ScaleX, ScaleY); //Getting whatever is the smaller scale

        //Getting the Offset to position it in the centre
        float OffSetX = (Graphics.PreferredBackBufferWidth - (VirtualWidth * Scale)) / 2f;
        float OffSetY = (Graphics.PreferredBackBufferHeight - (VirtualHeight * Scale)) / 2f;
        
        MatrixScale = Matrix.CreateScale(Scale, Scale, 1f) * Matrix.CreateTranslation(OffSetX, OffSetY, 0f); //Creating the scale
    }
}
