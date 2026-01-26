using Stronghold_Crusader_Project.Code.Units.UnitTypes;

namespace Stronghold_Crusader_Project.Code.Units;


/// <summary>
/// A class that will manager all the units on screen
/// the class will handle hostile and non hostile units
/// </summary>

public class UnitHandler
{
    //Class Variables
    private List<HostileUnit> PlayerHostileUnits;
    private List<PassiveUnit> PlayersPassiveUnits;
    private List<HostileUnit> EnemyUnits;
    private Pathing PathManager;
    private UnitFactory UnitCreator;
    private UnitAnimationLibrary AnimationLibrary;
    public List<UnitTemplate> SelectedUnits;

    public enum AddUnitType
    {
        Hostile,
        Passive,
        Enemy
    }

    //Class Methods

    public UnitHandler(ContentManager Content)
    {
        PathManager = new Pathing();
        AnimationLibrary = new UnitAnimationLibrary(Content);
        UnitCreator = new UnitFactory(AnimationLibrary);
    }

    #region Public Methods
    //Classes that are publicly accessible

    public void AddUnit(AddUnitType UnitType)
    {
        switch (UnitType)
        {
            case AddUnitType.Hostile:
                break;
            case AddUnitType.Passive:
                break;
            case AddUnitType.Enemy:
                break;
        }
    }

    public void Update(GameTime TimeOfGame, Tile[,] Map) //Updates all the units
    {
        foreach(UnitTemplate ActiveUnit in PlayerHostileUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
        foreach (UnitTemplate ActiveUnit in PlayersPassiveUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
        foreach (UnitTemplate ActiveUnit in EnemyUnits)
        {
            ActiveUnit.Update(TimeOfGame, Map);
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach (UnitTemplate ActiveUnit in PlayerHostileUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        foreach (UnitTemplate ActiveUnit in PlayersPassiveUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        foreach (UnitTemplate ActiveUnit in EnemyUnits)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
    }
    #endregion
}

