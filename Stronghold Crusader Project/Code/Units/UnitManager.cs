using Assimp;
using Stronghold_Crusader_Project.Code.Units.UnitTypes;

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

    public enum AddUnitType
    {
        Hostile,
        Passive,
        Enemy
    }

    //Class Methods

    public UnitManager(ContentManager Content)
    {
        PathManager = new Pathing();
        AnimationLibrary = new UnitAnimationLibrary(Content);
        UnitCreator = new UnitFactory(AnimationLibrary);
        PlayerHostileUnits = new List<HostileUnit>();
        PlayersPassiveUnits = new List<PassiveUnit>();
        EnemyUnits = new List<HostileUnit>();
        SelectedUnits = new List<UnitTemplate>();
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
        UpdateList(PlayerHostileUnits.Cast<UnitTemplate>().ToList(), TimeOfGame, Map);
        UpdateList(PlayersPassiveUnits.Cast<UnitTemplate>().ToList(), TimeOfGame, Map);
        UpdateList(EnemyUnits.Cast<UnitTemplate>().ToList(), TimeOfGame, Map);

        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);

        if (InputHandler.IsLeftClickedOnce()) //Adding units to the selected units list
        {
            SelectedUnits.Clear();
            foreach (HostileUnit ActiveUnit in PlayerHostileUnits)
            {
                if (Vector2.Distance(ActiveUnit.GetPosition(), MousePosition) < 30) //If the click within 30 pixels of the unit
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
            }
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        DrawList(PlayerHostileUnits.Cast<UnitTemplate>().ToList(), ActiveSpriteBatch);
        DrawList(PlayersPassiveUnits.Cast<UnitTemplate>().ToList(), ActiveSpriteBatch);
        DrawList(EnemyUnits.Cast<UnitTemplate>().ToList(), ActiveSpriteBatch);
    }
    #endregion
    
    #region Helper Classes
    //Methods that help the class

    private void UpdateList(List<UnitTemplate> Units, GameTime TimeOfGame, Tile[,] Map)
    {
        foreach (UnitTemplate ActiveUnit in Units )
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
    }

    private void DrawList(List<UnitTemplate> Units, SpriteBatch ActiveSpriteBatch)
    {
        foreach (UnitTemplate ActiveUnit in Units)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
    }

    private Point WorldToGrid(Vector2 WorldPosition) //Convert World to grid positions
    {
        int PositionY = (int)(WorldPosition.Y / (TileHeight / 2f));
        float OffSetX = 0;
        if (PositionY % 2 != 0)
        {
            OffSetX = TileWidth / 2f;
        }
        int PositionX = (int)((WorldPosition.X - OffSetX) / TileWidth);
        if (PositionX < 0 || PositionX >= MapWidth)
        {
            PositionX = 0;
        }
        if (PositionY < 0 || PositionY >= MapHeight)
        {
            PositionY = 0;
        }
        return new  Point(PositionX, PositionY);
    }
    
    #endregion
}

