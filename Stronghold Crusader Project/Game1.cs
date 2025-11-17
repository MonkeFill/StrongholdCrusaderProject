using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;
using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private StartupManager GameManager = new StartupManager();
    private MapHandler Mapping;
    private MenuManager Menus;
    private Box TestBox;

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
        Menus = new MenuManager(this);
        TestBox = new Box(new Rectangle(50, 50, 750, 500), Content);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    { 
        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

        if (deltaTime > 0)
        {
            double FPS = 1.0 / deltaTime;
            //Console.WriteLine($"FPS - {FPS:F2}");
        }
        MouseState ActiveMouse = Mouse.GetState();

        UpdateInputManager(gameTime);
        Menus.Update(ActiveMouse);

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
        //transformMatrix: MatrixScale
        _spriteBatch.Begin(transformMatrix: MatrixScale);
        Menus.Draw(_spriteBatch);
        TestBox.Draw(_spriteBatch);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void CreateViewScale(GraphicsDeviceManager Graphics)
    {
        //Getting the scale against the monitor sides
        float ScaleY = (float)Graphics.PreferredBackBufferHeight / VirtualHeight;
        float ScaleX = (float)Graphics.PreferredBackBufferWidth / VirtualWidth;

        float Scale = Math.Min(ScaleX, ScaleY); //Getting whatever is the smaller scale
        MatrixScale = Matrix.CreateScale(Scale, Scale, 1f); //Creating the scale
    }
}
