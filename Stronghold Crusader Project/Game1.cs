namespace Stronghold_Crusader_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager ActiveGraphics;
    private SpriteBatch ActiveSpriteBatch;

    double FpsTimer;
    int FpsCounter;
    int Fps;
    SpriteFont Font;

    public PlayGame GameHandler;

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
        Window.IsBorderless = false;
        ActiveGraphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        ActiveGraphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        ActiveGraphics.ApplyChanges();
        
        StartEventLog();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ActiveSpriteBatch = new SpriteBatch(GraphicsDevice);
        GameHandler = new PlayGame(this);
        Font = Content.Load<SpriteFont>("DefaultFont");
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
        FpsTimer += gameTime.ElapsedGameTime.TotalSeconds;
        FpsCounter++;
        if (FpsTimer >= 1)
        {
            Fps = FpsCounter;
            FpsCounter = 0;
            FpsTimer -= 1.0;
        }
        
        GraphicsDevice.Clear(Color.Red);
        GameHandler.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.Begin(); 
        ActiveSpriteBatch.DrawString(Font, "FPS: " + Fps.ToString(), new Vector2(20, 20), Color.White);
        ActiveSpriteBatch.End();
        base.Draw(gameTime);
    }
}
