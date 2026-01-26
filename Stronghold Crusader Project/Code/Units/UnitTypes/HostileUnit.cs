namespace Stronghold_Crusader_Project.Code.Units.UnitTypes;

/// <summary>
/// A unit type
/// This unit thats primary thing is to attack
/// </summary>

public class HostileUnit : UnitTemplate
{
    public HostileUnit(
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
