namespace Stronghold_Crusader_Project.Code.Units;

/// <summary>
/// This class will hold all the animations for the units
/// units animation handler can access the libary if they are looking for an animation to do
/// </summary>

public class UnitAnimationLibrary
{
    //Class Variables

    Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>> AnimationLibrary = new Dictionary<String, Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>>(); //Dictionary to hold the animations
    //To access the animation libary you have to go through the troop name, troop state and then troop direction to get the frames for the animation

    //Class Methods

    public UnitAnimationLibrary(ContentManager Content)
    {
        LoadAnimations(Content);
    }

    #region Open end
    //Classes that will be used outside of this class

    public Texture2D[] GetAnimationList(string UnitName, UnitState ActiveState, UnitDirection ActiveDirection) //A class that returns the list of animations
    {
        if (CheckIfAnimationExists(UnitName, ActiveState, ActiveDirection))
        {
            return AnimationLibrary[UnitName][ActiveState][ActiveDirection];
        }
        return null;
    }

    #endregion

    #region Helper Classes
    //Classes that will help this class

    public void LoadAnimations(ContentManager Content) //A class to load in all the animations
    {
        string UnitsAssetFolder = Path.Combine(ContentFolder, UnitsFolder);
        if (!Path.Exists(UnitsAssetFolder)) //If it doesn't exist
        {
            LogEvent($"{UnitsAssetFolder} is not found", LogType.Error);
            return;
        }

        foreach (string UnitNamePath in Directory.GetDirectories(UnitsAssetFolder)) //Unit Name
        {
            string UnitName = Path.GetFileNameWithoutExtension(UnitNamePath);


            //Checks
            if (!AnimationLibrary.ContainsKey(UnitName)) //If it doesn't contain the unit name
            {
                AnimationLibrary.Add(UnitName, new Dictionary<UnitState, Dictionary<UnitDirection, Texture2D[]>>());
            }


            foreach (string UnitStatePath in Directory.GetDirectories(UnitNamePath)) //Unit State
            {
                string UnitStateName = Path.GetFileNameWithoutExtension(UnitStatePath);


                //Checks
                if (!Enum.TryParse<UnitState>(UnitStateName, out UnitState ActualUnitState)) //Checking if state is hardcoded
                {
                    LogEvent($"{UnitStateName} isn't a hardcoded state", LogType.Warning);
                    continue;
                }

                if (!AnimationLibrary[UnitName].ContainsKey(ActualUnitState)) //If it doesn't contain the unit state
                {
                    AnimationLibrary[UnitName].Add(ActualUnitState, new Dictionary<UnitDirection, Texture2D[]>());
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


                    if (!AnimationLibrary[UnitName][ActualUnitState].ContainsKey(ActualUnitDirection)) //If it doesn't contain the unit direction
                    {
                        AnimationLibrary[UnitName][ActualUnitState].Add(ActualUnitDirection, new Texture2D[Frames]);
                    }
                    


                    //Actual logic
                    for (int Count = 0; Count < Frames; Count++)
                    {
                        try
                        {
                            string TextureName = Path.GetFileNameWithoutExtension(FrameFiles[Count]);
                            string TexturePath = Path.Combine(UnitsFolder, UnitName, UnitStateName, UnitDirection, TextureName) + MonoGameAddon;
                            int TextureFrame = int.Parse(TextureName.Replace(UnitAnimationName, ""));
                            Texture2D NewTexture = Content.Load<Texture2D>(TexturePath);
                            AnimationLibrary[UnitName][ActualUnitState][ActualUnitDirection][TextureFrame] = NewTexture;
                        }
                        catch(Exception Error)
                        {
                            LogEvent($"{FrameFiles[Count]} could not be loaded, {Error.Message}", LogType.Warning);
                        }
                    }

                }
            }
        }
        Console.WriteLine();
    }

    public bool CheckIfAnimationExists(string UnitName, UnitState ActiveState, UnitDirection ActiveDirection) //A class that checks an animation set exists
    {
        if (!AnimationLibrary.ContainsKey(UnitName)) //Checking if the unit name exists
        {
            LogEvent($"{UnitName} doesn't exist", LogType.Warning);
            return false;
        }
        
        if (!AnimationLibrary[UnitName].ContainsKey(ActiveState)) //Checking if the unit state exists
        {
            LogEvent($"{ActiveState} doesn't exist", LogType.Warning);
            return false;
        }
        
        if (!AnimationLibrary[UnitName][ActiveState].ContainsKey(ActiveDirection)) //Checking if the unit direction exists
        {
            LogEvent($"{ActiveDirection} doesn't exist", LogType.Warning);
            return false;
        }
        
        return true;
    }

    #endregion
}
