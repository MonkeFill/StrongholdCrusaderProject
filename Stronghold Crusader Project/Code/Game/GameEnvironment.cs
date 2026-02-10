using Assimp;

namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// Game environment will host all the classes needed to create it
/// it will be the main point of everything and only UI will be outside of this
/// </summary>

public class GameEnvironment
{
    //Class Variables
    private ContentManager ActiveContent;
    private GraphicsDevice ActiveGraphics;

    private TileLibary TilesHandler;
    private Borders BorderHandler;
    private KeyManager KeyHandler;
    private InputManager InputHandler;

    private GameWorld GameWorldHandler;
    private MenuManager MenuHandler;
    private Camera2D CameraHandler;
    private UnitManager UnitHandler;
    private bool MapActive = false;
    
    public GameEnvironment(ContentManager InputContent, GraphicsDevice InputGraphics)
    {
        ActiveContent = InputContent;
        ActiveGraphics = InputGraphics;
    }
    
    #region Public methods
    //Methods that will be used within game1

    public void LoadContent(Game1 Game) //Loads all the content needed like tiles and initalising the handlers
    {
        TilesHandler = new TileLibary(ActiveContent);
        BorderHandler = new Borders(ActiveContent);
        KeyHandler = new KeyManager();
        InputHandler = new InputManager(KeyHandler);
        GameWorldHandler = new GameWorld(TilesHandler, BorderHandler);
        UnitHandler = new UnitManager(ActiveContent, ActiveGraphics);
        CameraHandler = new Camera2D(ActiveGraphics.Viewport);
        MenuHandler = new MenuManager(Game, GameWorldHandler);
    }

    public void Update(GameTime TimeOfGame)
    {
        InputHandler.Update();
        MenuHandler.Update(InputHandler);
        HandleCameraInput(TimeOfGame);
        UnitHandler.Update(TimeOfGame, GameWorldHandler.Tiles, InputHandler, CameraHandler);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //Draws all the content
    {
        if (MapActive)
        {
            ActiveSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, CameraHandler.ViewMatrix);
            //Anything that is drawn with the camera should be here
            GameWorldHandler.Draw(ActiveSpriteBatch);
            UnitHandler.Draw(ActiveSpriteBatch);
            ActiveSpriteBatch.End();
        }
        
        ActiveSpriteBatch.Begin();
        //Anything that is drawn without the camera should be here
        MenuHandler.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.End();
    }

    public void StartTestGame()
    {
        GameWorldHandler.LoadWorld("GeneratedMap_50x50.json");
        MapActive = true;
        MenuHandler.AddMenu(new BlankMenu(MenuHandler, GameWorldHandler));
        HostileUnit Unit = UnitHandler.UnitCreator.GetArcher(new Vector2(576, 50));
        UnitHandler.AddUnit(Unit);
        Console.WriteLine();
    }
    
    #endregion
    
    #region Helper Classes
    //Classes to help the update method

    private void HandleCameraInput(GameTime TimeOfGame) //Handles camera movement
    {
        Vector2 Movement = Vector2.Zero;
        float Zoom = InputHandler.GetMouseChangedScrollWheel();
        float Rotation = 0;

        if (InputHandler.IsKeybindHeldDown(KeyAction.CameraUp))
        {
            Movement.Y = -1;
        }
        if (InputHandler.IsKeybindHeldDown(KeyAction.CameraDown))
        {
            Movement.Y = 1;
        }
        if (InputHandler.IsKeybindHeldDown(KeyAction.CameraLeft))
        {
            Movement.X = -1;
        }
        if (InputHandler.IsKeybindHeldDown(KeyAction.CameraRight))
        {
            Movement.X = 1;
        }
        
        CameraHandler.Update(TimeOfGame, Movement, Zoom, Rotation);
    }
    
    #endregion
}

