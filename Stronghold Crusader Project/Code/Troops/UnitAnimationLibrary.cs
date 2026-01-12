namespace Stronghold_Crusader_Project.Code.Troops;

/// <summary>
/// This class will hold all the animations for the units
/// units animation handler can access the libary if they are looking for an animation to do
/// </summary>

public class UnitAnimationLibrary
{
    Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, SpriteBatch[]>>> AnimationLibary = new Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, SpriteBatch[]>>>(); //Dictionary to hold the animations
    //To access the animation libary you have to go through the troop name, troop state and then troop direction to get the frames for the animation

    public UnitAnimationLibrary(ContentManager Content)
    {

    }

    #region Open end
    //Classes that will be used outside of this class

    #endregion

    #region Helper Classes
    //Classes that will help this class

    public void LoadAnimations(ContentManager Content) //A class to load in all the animations
    {
        if (Path.Exists(UnitsFolder))
        {
            foreach(string ActiveUnitName in Directory.GetDirectories(UnitsFolder))
            {
                foreach (string ActiveUnitState in Directory.GetDirectories(ActiveUnitName))
                {
                    foreach (string ActiveUnitDirection in Directory.GetDirectories(ActiveUnitState))
                    {
                        string UnitName = Path.GetFileNameWithoutExtension(ActiveUnitName);
                        string UnitState = Path.GetFileNameWithoutExtension(ActiveUnitState);
                        string UnitDirection = Path.GetFileNameWithoutExtension(ActiveUnitDirection);

                        if (Enum.TryParse<UnitState>(UnitState, out _)) //Checking if state is hardcoded
                        { //out _ deletes the output
                            if (Enum.TryParse<UnitDirection>(UnitDirection, out _)) //Checking if direction is hardcoded
                            {

                            }
                            else
                            {
                                LogEvent($"{UnitDirection} isn't a coded direction", LogType.Warning);
                            }
                        }
                        else
                        {
                            LogEvent($"{UnitState} isn't a coded state", LogType.Warning);
                        }
                    }
                }
            }
            return;
        }
        LogEvent($"{UnitsFolder} is not found", LogType.Error);
    }

    #endregion
}
