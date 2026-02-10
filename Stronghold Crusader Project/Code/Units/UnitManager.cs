using System.Numerics;
using Assimp;
using Stronghold_Crusader_Project.Code.Units.UnitTypes;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Units;


/// <summary>
/// A class that will manager all the units on screen
/// the class will handle hostile and non hostile units
/// </summary>

public class UnitManager
{
    //Class Variables
    private List<HostileUnit> PlayerHostileUnits;
    private List<PassiveUnit> PlayersPassiveUnits;
    private List<HostileUnit> EnemyUnits;
    private Pathing PathManager;
    public UnitFactory UnitCreator;
    private UnitAnimationLibrary AnimationLibrary;
    public List<UnitTemplate> SelectedUnits;
    
    //Debugging
    private Texture2D Pixel;
    Point DebugMouseGrid;
    Point OldDebugMouseGrid = Point.Zero;
    Point UnitPosition;
    List<Point> DebugPath;
    List<Point> DebugNeighbours;
    int RectangleSize = TileSize.Y / 2;
    int RectangleOffset = TileSize.Y / 4;
    

    public enum AddUnitType
    {
        Hostile,
        Passive,
        Enemy
    }

    //Class Methods

    public UnitManager(ContentManager Content, GraphicsDevice Graphics)
    {
        PathManager = new Pathing();
        AnimationLibrary = new UnitAnimationLibrary(Content);
        UnitCreator = new UnitFactory(AnimationLibrary);
        PlayerHostileUnits = new List<HostileUnit>();
        PlayersPassiveUnits = new List<PassiveUnit>();
        EnemyUnits = new List<HostileUnit>();
        SelectedUnits = new List<UnitTemplate>();
        Pixel = new Texture2D(Graphics, 1, 1);
        Pixel.SetData(new[] {Color.White});
    }

    #region Public Methods
    //Classes that are publicly accessible

    public void AddUnit(UnitTemplate Unit, bool IsEnemy = false)
    {
        if (Unit is PassiveUnit Passive)
        {
            PlayersPassiveUnits.Add(Passive);
        }
        else if (IsEnemy)
        {
            EnemyUnits.Add(Unit as HostileUnit);
        }
        else 
        {
            PlayerHostileUnits.Add(Unit as HostileUnit);
        }
    }

    public void Update(GameTime TimeOfGame, Tile[,] Map, InputManager InputHandler, Camera2D CameraHandler) //Updates all the units
    {
        foreach(HostileUnit ActiveUnit in PlayerHostileUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
        foreach(HostileUnit ActiveUnit in EnemyUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
        foreach(PassiveUnit ActiveUnit in PlayersPassiveUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }

        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);

        if (InputHandler.IsLeftClickedOnce()) //Adding units to the selected units list
        {
            SelectedUnits.Clear();
            foreach (HostileUnit ActiveUnit in PlayerHostileUnits)
            {
                if (Vector2.Distance(ActiveUnit.GetPosition(), MousePosition) < 75) //If the click within 75 pixels of the unit
                {
                    SelectedUnits.Add(ActiveUnit);
                }
            }
        }

        if (InputHandler.IsRightClickedOnce()) //Moving the unit to where the mouse is clicked
        {
            Point EndPoint = WorldToGrid(MousePosition);
            foreach (HostileUnit ActiveUnit in SelectedUnits)
            {
                Point StartPoint = WorldToGrid(ActiveUnit.GetPosition());
                List<Point> ActivePath = PathManager.FindPath(StartPoint, EndPoint, Map);
                ActiveUnit.MoveTo(ActivePath);
                
                //Debug stuff
                if (DebugPathfinding)
                {
                    UnitPosition = StartPoint;
                    DebugPath = ActivePath;
                    DebugMouseGrid = WorldToGrid(MousePosition);
                    try
                    {
                        if (OldDebugMouseGrid != DebugMouseGrid)
                        {
                            DebugNeighbours = PathManager.GetNeighbours(DebugMouseGrid);
                            OldDebugMouseGrid = DebugMouseGrid;
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach(HostileUnit ActiveUnit in PlayerHostileUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        foreach(HostileUnit ActiveUnit in EnemyUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        foreach(PassiveUnit ActiveUnit in PlayersPassiveUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        DrawDebug(ActiveSpriteBatch);
    }
    #endregion
    
    #region Helper Classes
    //Methods that help the class

    private Point WorldToGrid(Vector2 WorldPosition) //Convert World to grid positions
    {
        int PositionX = (int)WorldPosition.X / TileSize.X;
        int PositionY = (int)WorldPosition.Y / TileSize.Y;
        return new Point(PositionX, PositionY);
    }

    private void DrawDebug(SpriteBatch ActiveSpriteBatch)
    {
        if (!DebugPathfinding)
        {
            return;
        }
        Vector2 MouseWorld = GridToWorld(DebugMouseGrid);
        Vector2 UnitWorld = GridToWorld(UnitPosition);
        if (DebugPath != null)
        {
            foreach (Point ActivePoint in DebugPath)
            {
                Vector2 Position = GridToWorld(ActivePoint);
                ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)Position.X - RectangleOffset, (int)Position.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Green);
            }
        }

        if (DebugNeighbours != null)
        {
            foreach (Point ActivePoint in DebugNeighbours)
            {
                Vector2 Position = GridToWorld(ActivePoint);
                ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)Position.X - RectangleOffset, (int)Position.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Yellow);
            }
        }
        
        ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)MouseWorld.X - RectangleOffset, (int)MouseWorld.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Blue);
        ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)UnitWorld.X - RectangleOffset, (int)UnitWorld.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Orange);
    }

    private Vector2 GridToWorld(Point GridPosition)
    {
        int PositionX = (GridPosition.X * TileSize.X) + (TileSize.X / 2);
        int PositionY = (GridPosition.Y * TileSize.Y) + (TileSize.Y / 2);
        return new Vector2(PositionX, PositionY);
    }
    
    #endregion
}

