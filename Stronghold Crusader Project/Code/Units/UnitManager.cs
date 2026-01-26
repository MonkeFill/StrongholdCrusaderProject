using Stronghold_Crusader_Project.Code.Units.UnitTypes;

namespace Stronghold_Crusader_Project.Code.Units;


/// <summary>
/// A class that will manager all the units on screen
/// the class will handle hostile and non hostile units
/// </summary>

public class UnitManager
{
    //Class Variables
    private List<HostileUnit> MilitaryUnits;
    private List<PassiveUnit> NonMilitaryUnits;
    private Pathing PathManager;
    private List<UnitTemplate> SelectedUnits;
    private Tile[,] Map;
    
    //Class Methods
    
    public UnitManager()
    {
        PathManager = new Pathing();
    }
}

