namespace Stronghold_Crusader_Project.Code.Units;

/// <summary>
/// This class will hold all the animations for the units
/// units animation handler can access the libary if they are looking for an animation to do
/// </summary>

public class UnitAnimationLibrary
{
    Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>> AnimationLibary = new Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>>(); //Dictionary to hold the animations
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
        if (!Path.Exists(UnitsFolder)) //If it doesn't exist
        {
            LogEvent($"{UnitsFolder} is not found", LogType.Error);
            return;
        }

        foreach (string UnitNamePath in Directory.GetDirectories(UnitsFolder)) //Unit Name
        {
            string UnitName = Path.GetFileNameWithoutExtension(UnitNamePath);


            //Checks
            if (!AnimationLibary.ContainsKey(UnitName)) //If it doesn't contain the unit name
            {
                AnimationLibary.Add(UnitName, new Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>());
            }


            foreach (string UnitStatePath in Directory.GetDirectories(UnitNamePath)) //Unit State
            {
                string UnitStateName = Path.GetFileNameWithoutExtension(UnitStatePath);


                //Checks
                if (!Enum.TryParse<UnitState>(UnitStateName, out UnitState AcutalUnitState)) //Checking if state is hardcoded
                {
                    LogEvent($"{UnitStateName} isn't a hardcoded state", LogType.Warning);
                    continue;
                }

                if (!AnimationLibary[UnitName].ContainsKey(AcutalUnitState)) //If it doesn't contain the unit state
                {
                    AnimationLibary[UnitName].Add(AcutalUnitState, new Dictionary<UnitDirection, Texture2D[]>());
                }




                foreach (string UnitDirectionPath in Directory.GetDirectories(UnitStatePath))
                {
                    string UnitDirection = Path.GetFileNameWithoutExtension(UnitDirectionPath);


                    //Checks
                    if (!Enum.TryParse<UnitDirection>(UnitDirection, out UnitDirection ActualUnitDirection)) //Checking if state is hardcoded
                    {
                        LogEvent($"{UnitDirection} isn't a hardcoded state", LogType.Warning);
                        continue;
                    }


                    string[] FrameFiles = Directory.GetFiles(UnitDirectionPath, $"*{MonoGameAddon}");
                    int Frames = FrameFiles.Length;


                    if (!AnimationLibary[UnitName][AcutalUnitState].ContainsKey(ActualUnitDirection)) //If it doesn't contain the unit direction
                    {
                        AnimationLibary[UnitName][AcutalUnitState].Add(ActualUnitDirection, new Texture2D[Frames]);
                    }
                    


                    //Actual logic
                    for (int Count = 0; Count < Frames; Count++)
                    {
                        try
                        {
                            string TexturePath = FrameFiles[Count].Replace(ContentFolder + "/", "").Replace(MonoGameAddon, "");
                            int TextureFrame = int.Parse(Path.GetFileNameWithoutExtension(FrameFiles[Count]).Replace(UnitAnimationName, ""));
                            Texture2D NewTexture = Content.Load<Texture2D>(TexturePath);
                            AnimationLibary[UnitName][AcutalUnitState][ActualUnitDirection][TextureFrame] = NewTexture;
                        }
                        catch(Exception Error)
                        {
                            LogEvent($"{FrameFiles[Count]} count not be loaded, {Error.Message}", LogType.Warning);
                        }
                    }

                }
            }
        }
    }

    #endregion
}
