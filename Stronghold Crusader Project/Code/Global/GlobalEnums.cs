namespace Stronghold_Crusader_Project.Code.Global;

/// <summary>
/// This class contains all of the enums that will be used throughout the project
/// they are accessiable to all classes
/// </summary>


#region Unit
//Enums for the units

public enum UnitState
{
    Idle,
    Sitting,
    Walking,
    Running,
    Attacking,
    Melee,
    Dying
}

public enum UnitDirection
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

#endregion

#region Other
//Other enums
public enum LogType
{
    Error,
    Warning,
    Debug,
    Info,
    Status,
}

#endregion
