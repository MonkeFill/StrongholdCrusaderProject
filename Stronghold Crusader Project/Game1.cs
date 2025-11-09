namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private StartupManager GameManager = new StartupManager();
    private MapHandler Mapping;
    Texture2D TempPixel;

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
        Mapping = new MapHandler(Content);
        Mapping.SetupNewMap();
        Camera2D.Initialize(GraphicsDevice.Viewport);
        CreateViewScale(_graphics);
        
        //Testing
        
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

        if (deltaTime > 0)
        {
            double FPS = 1.0 / deltaTime;
            //Console.WriteLine($"FPS - {FPS:F2}");
        }
        UpdateInputManager(gameTime);
        base.Update(gameTime);
        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        //Drawing anything that requires the camera like the map
        _spriteBatch.Begin(transformMatrix: Camera2D.GetViewMatrix(), samplerState: SamplerState.AnisotropicClamp); 
        Mapping.DrawMap(_spriteBatch);
        _spriteBatch.End();
        
        //Anything else that will be drawn using the Matrix Scale depending on the monitor
        _spriteBatch.Begin(transformMatrix: MatrixScale);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void CreateViewScale(GraphicsDeviceManager Graphics)
    {
        //Getting the scale against the monitor sides
        float ScaleY = Graphics.PreferredBackBufferHeight / VirtualHeight;
        float ScaleX = Graphics.PreferredBackBufferWidth / VirtualWidth;

        float Scale = Math.Min(ScaleX, ScaleY); //Getting whatever is the smaller scale
        MatrixScale = Matrix.CreateScale(Scale, Scale, 1f); //Creating the scale
    }
}
