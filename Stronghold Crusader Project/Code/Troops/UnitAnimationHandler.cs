namespace Stronghold_Crusader_Project.Code.Troops;

/// <summary>
/// This class will handle the animations of a unit
/// </summary>

public class UnitAnimationHandler
{
    private string Name;
    private int ActiveFrame = 0;
    private string ActiveFolder;
    private UnitDirection PreviousDirection;
    private UnitState PreviousState;

    public UnitAnimationHandler(string InputName)
    {
        Name = InputName;
    }

    public void Update(UnitDirection ActiveDirection, UnitState ActiveState)
    {
        if(PreviousState != ActiveState) //If the state is changed
        {
            ActiveFrame = 0;
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {

    }
}
