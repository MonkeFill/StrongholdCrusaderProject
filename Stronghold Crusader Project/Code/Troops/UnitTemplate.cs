namespace Stronghold_Crusader_Project.Code.Troops;

/// <summary>
/// This class is a template class for all troops in the game
/// </summary>

public abstract class UnitTemplate
{
    //Class Variables

    public string UnitName;
    public float Rotation;
    private string UnitCategory;
    private float MaxHealth;
    private float MovementSpeed;
    private float AttackPower;
    private float AttackSpeed;

    //Class Methods
    public UnitTemplate()
    {

    }

    public UnitDirection GetDirection() //A class to get which direction the unit is pointing
    {
        int DirectionAmount = Enum.GetNames(typeof(UnitDirection)).Length; //Getting how many directions are possible
        float RotationPer = 360f / DirectionAmount;
        int Index = (int)((Rotation / RotationPer) % DirectionAmount);
        return (UnitDirection)Index;
    }
}
