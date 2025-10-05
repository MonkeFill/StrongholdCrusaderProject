namespace Stronghold_Crusader_Project.Code.User_Input;

public static class KeyManager //Keybinds that will be used to control everything
{
    //Class Variables
    public static readonly Dictionary<string, KeyMap> Keybinds = new(); //Dictionary that will store the control of the key and then use KeyMap class to store data for it
    
    //Methods
    public static void AddNewKeybind(string Control, Keys DefaultKey, object ChangeValue) //Method for adding a new keybind to the dictionary 
    {
        if (Keybinds.ContainsKey(Control)) //If the control is already used
        {
            LogEvent($"Keybind for {Control} is used somewhere else", LogType.Warning);
        }
        else if (KeyAlreadyUsed(DefaultKey)) //If the keybind is used for another control
        {
            LogEvent($"Keybind for {Control} - {DefaultKey} already used", LogType.Warning);
        }
        else
        {
            //Valid so its both a unique control and keybind
            KeyMap NewKey = new KeyMap(Control, DefaultKey, ChangeValue);
            Keybinds.Add(Control, NewKey);
            LogEvent($"Added new keybind for {Control} - {NewKey.CurrentKey}", LogType.Info);
        }
    }
    
    public static void RemoveKeybind(string Control) //Removing a keybind from the dictionary
    {
        if (Keybinds.ContainsKey(Control)) //Checking if control is in the dictionary
        {
            KeyMap ActiveKey =  Keybinds[Control];
            LogEvent($"Removed Keybind {Control} - {ActiveKey.CurrentKey}", LogType.Info);
            Keybinds.Remove(Control);
        }
        else //If not found in dictionary
        {
            LogEvent($"No Keybind found for {Control} to be removed", LogType.Warning);
        }
    }

    public static bool UpdateKeybind(string Control, Keys NewKey) //Change a keybind to a new key
    {
        if (Keybinds.ContainsKey(Control)) //If the dictionary even has the control
        {
            if (!KeyAlreadyUsed(NewKey)) //Checking if the keybind is already used
            {
                KeyMap ActiveKey = Keybinds[Control];
                LogEvent($"Updating Keybind for {Control}, OldKey - {ActiveKey.CurrentKey}, NewKey - {NewKey}", LogType.Info);
                ActiveKey.CurrentKey = NewKey;
                return true;
            }
            else //If keybind is already used
            {
                LogEvent($"Keybind for {Control} already exists", LogType.Warning);
                return false;
            }
        }
        else //If control isn't found
        {
            LogEvent($"No Keybind found for {Control}", LogType.Warning);
            return false;
        }
    }
    
    public static void ResetAllToDefault() //Resetting all the keybinds to their default value
    {
        LogEvent("Currently resetting all keys to default",EventLogger.LogType.Info);
        foreach (KeyMap Key in Keybinds.Values) //Going through each keybind in the dictionary
        {
            Key.CurrentKey = Key.DefaultKey;
            LogEvent($"Resetting Keybind for {Key.Control} - {Key.CurrentKey}", LogType.Info);
        }
        LogEvent("All keys reset to default", LogType.Info);
    }

    public static Keys GetKeyFromControl(string Control) //Getting the key of a control
    {
        if (Keybinds.ContainsKey(Control)) //Checking if it exists
        {
            return Keybinds[Control].CurrentKey;
        }
        LogEvent($"No Keybind found for {Control}", LogType.Warning);
        return Keys.None;
    }
    
    public static object GetValueChangeFromControl(string Control) //Getting the value change of a control
    {
        if (Keybinds.ContainsKey(Control)) //Checking if it exists
        {
            return Keybinds[Control].ValueChange;
        }
        LogEvent($"No Keybind found for {Control}", LogType.Warning);
        return null;
    }

    private static bool KeyAlreadyUsed(Keys KeyChange) //A method to check if a key is being used by any other control
    {
        foreach (KeyMap ActiveKey in Keybinds.Values) //Looping through all the key values
        {
            if (ActiveKey.CurrentKey == KeyChange)
            {
                return true;
            }
        }
        return false;
    }
    
    public static void InitializeDefaultKeybinds() //Method to initialise all the default keybinds
    {
        AddNewKeybind("MoveUp", Keys.W, new Vector2(0,-1));
        AddNewKeybind("MoveDown", Keys.S, new Vector2(0,1));
        AddNewKeybind("MoveLeft", Keys.A, new Vector2(-1, 0));
        AddNewKeybind("MoveRight", Keys.D, new Vector2(1,0));
        AddNewKeybind("RotateClockwise", Keys.Q, 1);
        AddNewKeybind("RotateCounterClockwise", Keys.E, -1);
    }
}
