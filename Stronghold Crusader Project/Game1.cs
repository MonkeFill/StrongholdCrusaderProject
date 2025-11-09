using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private StartupManager GameManager = new StartupManager();
    private MapHandler Mapping;
    Texture2D TempPixel;
    BaseButtonDrawer Drawer;
    Button TestButton1;
    Button TestButton2;
    Button TestButton3;
    Button TestButton4;
    Button TestButton5;

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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        TempPixel = new Texture2D(GraphicsDevice, 1, 1);
        TempPixel.SetData(new[] { Color.White });
        
        //Testing
        Texture2D Background = Content.Load<Texture2D>("Assets/UI/Menus/Buttons/Backgrounds/Button5");
        Texture2D Background_Hover = Content.Load<Texture2D>("Assets/UI/Menus/Buttons/Backgrounds/Button5_Hover");
        Texture2D HoverIcon = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/Selected");
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        float Scale = 1f;
        Color TextColour = Color.White;
        string Text = "Test";
        Drawer = new SelectionDrawer(Background, Background_Hover, HoverIcon, Font, Scale, TextColour, Text );
        TestButton1 = new Button("TestButton", "TestCategory", new Rectangle(120, 40, 360, 54), Drawer, null);
        TestButton2 = new Button("TestButton", "TestCategory", new Rectangle(120, 120, 360, 54), Drawer, null);
        TestButton3 = new Button("TestButton", "TestCategory", new Rectangle(120, 200, 360, 54), Drawer, null);
        TestButton4 = new Button("TestButton", "TestCategory", new Rectangle(120, 280, 360, 54), Drawer, null);
        TestButton5 = new Button("TestButton", "TestCategory", new Rectangle(120, 360, 360, 54), Drawer, null);
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
        TestButton1.Update(Mouse.GetState());
        TestButton2.Update(Mouse.GetState());
        TestButton3.Update(Mouse.GetState());
        TestButton4.Update(Mouse.GetState());
        TestButton5.Update(Mouse.GetState());
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
        _spriteBatch.Begin();
        TestButton1.Draw(_spriteBatch);
        TestButton2.Draw(_spriteBatch);
        TestButton3.Draw(_spriteBatch);
        TestButton4.Draw(_spriteBatch);
        TestButton5.Draw(_spriteBatch);
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
