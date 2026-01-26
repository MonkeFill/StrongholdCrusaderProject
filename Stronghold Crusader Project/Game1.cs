namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager ActiveGraphics;
    private SpriteBatch ActiveSpriteBatch;

    private PlayGame GameHandler;

    public Game1()
    {
        ActiveGraphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
    }

    protected override void Initialize()
    {
        //Setting the game to full screen borderless
        Window.IsBorderless = true;
        ActiveGraphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        ActiveGraphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        ActiveGraphics.ApplyChanges();
        
        StartEventLog();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ActiveSpriteBatch = new SpriteBatch(GraphicsDevice);
        GameHandler = new PlayGame(Content, GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive) //If the game window is the top window (not tab out)
        {
            GameHandler.Update(gameTime);
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        GameHandler.Draw();
        base.Draw(gameTime);
    }
}
