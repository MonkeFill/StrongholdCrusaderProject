namespace Stronghold_Crusader_Project.Code.Units.UnitTypes;

/// <summary>
/// A unit type
/// This unit does not attack at all like workers
/// </summary>

public class PassiveUnit : UnitTemplate
{
    public PassiveUnit(
        string Name,
        float Health,
        float Speed,
        float AttackPower,
        float AttackSpeed,
        UnitAnimationLibrary AnimationLibrary,
        Vector2 Position
    ) : base(
        Name,
        Health,
        Speed,
        AttackPower,
        AttackSpeed,
        AnimationLibrary,
        Position
    ) { }
}
