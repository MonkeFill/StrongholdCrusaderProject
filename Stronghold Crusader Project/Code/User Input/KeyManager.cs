namespace Stronghold_Crusader_Project.Code.User_Input;

public class KeyManager //Managers a set of keybinds
{
    //Class Variables
    private string ManagerName;
    public Dictionary<string, KeyMap> Keybinds = new Dictionary<string, KeyMap>();
    
    //Class Methods
    public KeyManager(string Input_ManagerName)
    {
        ManagerName = Input_ManagerName;
    }

    public void AddNewKeybind(string Name, Keys DefaultKey, Action KeybindAction, KeybindActiveType KeybindActivationType) //Method for adding a new keybind to the keybinds list
    {
        if (!KeybindExists(Name)) //If the keybind doesn't already exists
        {
            if (!KeyInUse(DefaultKey)) //If the key isn't already used
            {
                KeyMap NewKey = new KeyMap(Name, DefaultKey, DefaultKey, KeybindAction, KeybindActivationType);
                Keybinds.Add(Name, NewKey);
                LogEvent($"Added new keybind, {Name}, {DefaultKey}, {KeybindAction}, {KeybindActivationType}", LogType.Info);
            }
            else
            {
                LogEvent($"{DefaultKey} already in use", LogType.Warning);
            }
        }
        else
        {
            LogEvent($"{Name} already exists", LogType.Warning);
        }
    }

    public void ChangeKeybindKey(string Name, Keys NewKey) //Change a keybinds key to a different one
    {
        if (KeybindExists(Name)) //If the keybind even exists
        {
            if (!KeyInUse(NewKey)) //If the keybind is not in use
            {
                Keybinds[Name].CurrentKey = NewKey;
            }
            else
            {
                LogEvent($"{NewKey} already in use", LogType.Warning);
            }
        }
        else
        {
            LogEvent($"{Name} keybind doesn't exist", LogType.Warning);
        }
    }

    public void ResetKeybindsToDefault() //Setting all keybinds to their default key
    {
        LogEvent($"Starting keybind default reset", LogType.Info);
        foreach (KeyMap ActiveKey in Keybinds.Values) //Looping through all keybinds
        {
            LogEvent($"Reset keybind for {ActiveKey.Name} - {ActiveKey.CurrentKey} -> {ActiveKey.DefaultKey}", LogType.Info);
            ActiveKey.CurrentKey = ActiveKey.DefaultKey;
        }
        LogEvent($"All keys reset to default", LogType.Info);
    }

    public Keys GetKeyFromName(string Name) //Getting the key of a keybind
    {
        if (KeybindExists(Name)) //If the keybind exists
        {
            return Keybinds[Name].CurrentKey;
        }
        LogEvent($"No keybind found for {Name}", LogType.Warning);
        return Keys.None;
    }

    public void PressKeybind(string Name) //Starting the action of a keybind
    {
        if (KeybindExists(Name)) //If the keybind exists
        {
            Keybinds[Name].KeybindAction.Invoke();
        }
        else
        {
            LogEvent($"No keybind found for {Name}", LogType.Warning);
        }
    }

    private bool KeybindExists(string Name) //Checking if the keybind already exists
    {
        if (Keybinds.ContainsKey(Name))
        {
            return true;
        }
        
        return false;
    }

    private bool KeyInUse(Keys Key) //Checking if a key is already in use
    {
        foreach (KeyMap ActiveKey in Keybinds.Values) //Looping through all the keybinds and their values
        {
            if (ActiveKey.CurrentKey == Key) //If they are equal to each other it means its being used
            {
                return true;
            }
        }
        return false;
    }
}

