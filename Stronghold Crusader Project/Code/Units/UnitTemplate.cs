namespace Stronghold_Crusader_Project.Code.Units;

/// <summary>
/// This class is a template class for all troops in the game
/// </summary>

public abstract class UnitTemplate
{
    //Class Variables

    private string UnitName;
    private float MaxHealth;
    private float MovementSpeed;
    private float AttackPower;
    private float AttackSpeed;
    private float Rotation = 0f;
    private UnitState ActiveState;
    private UnitAnimationHandler AnimationHandler;


    //Class Methods
    public UnitTemplate(UnitAnimationLibrary AnimationLibrary)
    {
        AnimationHandler = new UnitAnimationHandler(UnitName, AnimationLibrary);
    }

    public UnitDirection GetDirection() //A class to get which direction the unit is pointing
    {
        int DirectionAmount = Enum.GetNames(typeof(UnitDirection)).Length; //Getting how many directions are possible
        float RotationPer = 360f / DirectionAmount;
        int Index = (int)((Rotation / RotationPer) % DirectionAmount);
        return (UnitDirection)Index;
    }
}
