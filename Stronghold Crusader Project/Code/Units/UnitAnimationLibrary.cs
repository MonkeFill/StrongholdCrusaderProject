namespace Stronghold_Crusader_Project.Code.Units;

/// <summary>
/// This class will hold all the animations for the units
/// units animation handler can access the libary if they are looking for an animation to do
/// </summary>

public class UnitAnimationLibrary
{
    //Class Variables

    Dictionary<int, Texture2D[]> AnimationLibrary = new Dictionary<int, Texture2D[]>(); //Dictionary to hold the animations
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
        int Key = GetAnimationKey(UnitName, ActiveState, ActiveDirection);
        if (AnimationLibrary.TryGetValue(Key, out Texture2D[] ActiveFrames))
        {
            return ActiveFrames;
        }
        return null;
    }

    #endregion

    #region Helper Classes
    //Classes that will help this class

    private void LoadAnimations(ContentManager Content) //A class to load in all the animations
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
            
            foreach (string UnitStatePath in Directory.GetDirectories(UnitNamePath)) //Unit State
            {
                string UnitStateName = Path.GetFileNameWithoutExtension(UnitStatePath);


                //Checks
                if (!Enum.TryParse<UnitState>(UnitStateName, out UnitState ActualUnitState)) //Checking if state is hardcoded
                {
                    LogEvent($"{UnitStateName} isn't a hardcoded state", LogType.Warning);
                    continue;
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
                    FrameFiles = FrameFiles.OrderBy(Frame => int.Parse(Path.GetFileNameWithoutExtension(Frame).Replace(UnitAnimationName, ""))).ToArray();
                    int FramesLength = FrameFiles.Length;
                    Texture2D[] TextureArray = new Texture2D[FramesLength];
                    for (int Count = 0; Count < FramesLength; Count++)
                    {
                        string LoadPath = Path.GetRelativePath(ContentFolder, FrameFiles[Count]).Replace(MonoGameAddon, "");
                        TextureArray[Count] = Content.Load<Texture2D>(LoadPath);
                    }
                    int Key = GetAnimationKey(UnitName, ActualUnitState, ActualUnitDirection);
                    if (!AnimationLibrary.ContainsKey(Key)) //If key doesn't exist
                    {
                        AnimationLibrary.Add(Key, TextureArray);
                    }
                }
            }
        }
        Console.WriteLine();
    }

    private bool CheckIfAnimationExists(string UnitName, UnitState ActiveState, UnitDirection ActiveDirection) //A class that checks an animation set exists
    {
        int Key = GetAnimationKey(UnitName, ActiveState, ActiveDirection);
        return AnimationLibrary.ContainsKey(Key);
    }

    private int GetAnimationKey(string UnitName, UnitState State, UnitDirection Direction)
    {
        return HashCode.Combine(UnitName, State, Direction);
    }

    #endregion
}
