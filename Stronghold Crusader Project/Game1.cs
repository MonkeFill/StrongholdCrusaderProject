using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;
using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private StartupManager GameManager = new StartupManager();
    private MapHandler Mapping;
    Texture2D TempPixel;

    //Testing
    BaseButtonDrawer Drawer;
    Button SelectButton1;
    Button SelectButton2;
    Button SelectButton3;
    Button SelectButton4;
    Button SelectButton5;
    Button IconButton1;
    Button IconButton2;
    Button ActiveIconButton1;
    Button ActiveIconButton2;
    Button ActiveIconButton3;
    BaseMenu TestMenu;

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
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        float Scale = 1f;
        Color TextColour = Color.White;
        string Text = "Test";

        //Testing Selection Buttons
        Texture2D SelectionBackground = Content.Load<Texture2D>("Assets/UI/Menus/Buttons/Backgrounds/Button5");
        Texture2D SelectionBackground_Hover = Content.Load<Texture2D>("Assets/UI/Menus/Buttons/Backgrounds/Button5_Hover");
        Texture2D SelectionHoverIcon = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/Selected");
        Drawer = new SelectionDrawer(SelectionBackground, SelectionBackground_Hover, SelectionHoverIcon, Font, Scale, TextColour, Text );
        SelectButton1 = new Button("SelectButton1", "", new Rectangle(120, 40, 360, 54), Drawer, null);
        SelectButton2 = new Button("SelectButton2", "", new Rectangle(120, 120, 360, 54), Drawer, null);
        SelectButton3 = new Button("SelectButton3", "", new Rectangle(120, 200, 360, 54), Drawer, null);
        SelectButton4 = new Button("SelectButton4", "", new Rectangle(120, 280, 360, 54), Drawer, null);
        SelectButton5 = new Button("SelectButton5", "", new Rectangle(120, 360, 360, 54), Drawer, null);

        //Testing Icon Buttons
        Texture2D Button1 = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/ExitButton");
        Texture2D Button1_Hover = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/ExitButton_Hover");
        Texture2D Button2 = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/TutorialButton");
        Texture2D Button2_Hover = Content.Load<Texture2D>("Assets/UI/Menus/HomeScreen/TutorialButton_Hover");
        Drawer = new IconDrawer(Button1, Button1_Hover);
        IconButton1 = new Button("IconButton1", "", new Rectangle(120, 450, 75, 82), Drawer, null);
        Drawer = new IconDrawer(Button2, Button2_Hover);
        IconButton2 = new Button("IconButton2", "", new Rectangle(225, 450, 142, 85), Drawer, null);

        //Testing ActiveIcon Buttons
        Texture2D ActiveButton1 = Content.Load<Texture2D>("Assets/UI/Game/Categories/Food");
        Texture2D ActiveButton1_Hover = Content.Load<Texture2D>("Assets/UI/Game/Categories/Food_Hover");
        Texture2D ActiveButton1_Active = Content.Load<Texture2D>("Assets/UI/Game/Categories/Food_Active");
        Drawer = new ActiveIconDrawer(ActiveButton1, ActiveButton1_Hover, ActiveButton1_Active);
        ActiveIconButton1 = new Button("ActiveIconButton1", "test", new Rectangle(120, 575, 30, 35), Drawer, null);
        Texture2D ActiveButton2 = Content.Load<Texture2D>("Assets/UI/Game/Categories/Castle");
        Texture2D ActiveButton2_Hover = Content.Load<Texture2D>("Assets/UI/Game/Categories/Castle_Hover");
        Texture2D ActiveButton2_Active = Content.Load<Texture2D>("Assets/UI/Game/Categories/Castle_Active");
        Drawer = new ActiveIconDrawer(ActiveButton2, ActiveButton2_Hover, ActiveButton2_Active);
        ActiveIconButton2 = new Button("ActiveIconButton2", "test", new Rectangle(220, 575, 30, 35), Drawer, null);
        Texture2D ActiveButton3 = Content.Load<Texture2D>("Assets/UI/Game/Categories/Town");
        Texture2D ActiveButton3_Hover = Content.Load<Texture2D>("Assets/UI/Game/Categories/Town_Hover");
        Texture2D ActiveButton3_Active = Content.Load<Texture2D>("Assets/UI/Game/Categories/Town_Active");
        Drawer = new ActiveIconDrawer(ActiveButton3, ActiveButton3_Hover, ActiveButton3_Active);
        ActiveIconButton3 = new Button("ActiveIconButton3", "test", new Rectangle(320, 575, 30, 35), Drawer, null);

        TestMenu = new HomeScreen(null, Content);
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

        //Testing
        MouseState ActiveMouse = Mouse.GetState();
        SelectButton1.Update(ActiveMouse);
        SelectButton2.Update(ActiveMouse);
        SelectButton3.Update(ActiveMouse);
        SelectButton4.Update(ActiveMouse);
        SelectButton5.Update(ActiveMouse);
        IconButton1.Update(ActiveMouse);
        IconButton2.Update(ActiveMouse);
        ActiveIconButton1.Update(ActiveMouse);
        ActiveIconButton2.Update(ActiveMouse);
        ActiveIconButton3.Update(ActiveMouse);
        TestMenu.Update(ActiveMouse);
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

        //Testing
        /*
        SelectButton1.Draw(_spriteBatch);
        SelectButton2.Draw(_spriteBatch);
        SelectButton3.Draw(_spriteBatch);
        SelectButton4.Draw(_spriteBatch);
        SelectButton5.Draw(_spriteBatch);
        IconButton1.Draw(_spriteBatch);
        IconButton2.Draw(_spriteBatch);
        ActiveIconButton1.Draw(_spriteBatch);
        ActiveIconButton2.Draw(_spriteBatch);
        ActiveIconButton3.Draw(_spriteBatch);
        */
        TestMenu.Draw(_spriteBatch);
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
