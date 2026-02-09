namespace Stronghold_Crusader_Project.Code.Units.UnitTypes;

/// <summary>
/// This class will hold all units possible
/// </summary>

public class UnitFactory
{
    //Class Variables
    UnitAnimationLibrary AnimationLibrary;

    //Class Methods

    public UnitFactory(UnitAnimationLibrary AnimationLibrary)
    {
        this.AnimationLibrary = AnimationLibrary;
    }

    #region Units
    //Where all the units live
    public HostileUnit GetArcher(Vector2 Position)
    {
        return CreateHostileUnit(
            "Archer",
            100f,
            15f,
            15f,
            1.0f,
            2f,
            Position
        );
    }
    
    #endregion

    #region Class Helpers
    //Methods to help the class

    private HostileUnit CreateHostileUnit(
        string Name,
        float Health,
        float Speed,
        float AttackPower,
        float AttackSpeed,
        float Range,
        Vector2 Position
    )
    {
        return new HostileUnit(
            Name,
            Health,
            Speed,
            AttackPower,
            AttackSpeed,
            Range,
            AnimationLibrary,
            Position
        );
    }
    private PassiveUnit CreatePassiveUnit(
        string Name,
        float Health,
        float Speed,
        float AttackPower,
        float AttackSpeed,
        Vector2 Position
    )
    {
        return new PassiveUnit(
            Name,
            Health,
            Speed,
            AttackPower,
            AttackSpeed,
            AnimationLibrary,
            Position
        );
    }

    #endregion
}
