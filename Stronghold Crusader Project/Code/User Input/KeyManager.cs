namespace Stronghold_Crusader_Project.Code.User_Input;

public static class KeyManager //Keybinds that will be used to control everything
{
    public static readonly Dictionary<string, KeyMap> Keybinds = new(); //Dictionary that will store the control of the key and then use KeyMap class to store data for it

    public static void CreateDefaults() //Creating the default keybinds to add 
    {
        AddNewKeybind("MoveUp", Keys.W);
        AddNewKeybind("MoveDown", Keys.S);
        AddNewKeybind("MoveLeft", Keys.A);
        AddNewKeybind("MoveRight", Keys.D);
        AddNewKeybind("RotateCameraLeft", Keys.Q);
        AddNewKeybind("RotateCameraRight", Keys.E);
        AddNewKeybind("PreviousMenu", Keys.Escape);
    }
        
    public static void AddNewKeybind(string Control, Keys DefaultKey) //Method for adding a new keybind to the dictionary 
    {
        if (Keybinds.ContainsKey(Control)) //If the control is already used
        {
            EventLogger.LogEvent($"Keybind for {Control} is used somewhere else", EventLogger.LogType.Warning);
        }
        else if (KeyAlreadyUsed(DefaultKey)) //If the keybind is used for another control
        {
            EventLogger.LogEvent($"Keybind for {Control} - {DefaultKey} already used", EventLogger.LogType.Warning);
        }
        else
        {
            //Valid so its both a unique control and keybind
            KeyMap NewKey = new KeyMap(Control, DefaultKey);
            Keybinds.Add(Control, NewKey);
            EventLogger.LogEvent($"Added new keybind for {Control} - {NewKey.CurrentKey}", EventLogger.LogType.Info);
        }
    }
    
    public static void RemoveKeybind(string Control) //Removing a keybind from the dictionary
    {
        if (Keybinds.ContainsKey(Control)) //Checking if control is in the dictionary
        {
            KeyMap ActiveKey =  Keybinds[Control];
            EventLogger.LogEvent($"Removed Keybind {Control} - {ActiveKey.CurrentKey}", EventLogger.LogType.Info);
            Keybinds.Remove(Control);
        }
        else //If not found in dictionary
        {
            EventLogger.LogEvent($"No Keybind found for {Control} to be removed", EventLogger.LogType.Warning);
        }
    }

    public static bool UpdateKeybind(string Control, Keys NewKey) //Change a keybind to a new key
    {
        if (Keybinds.ContainsKey(Control)) //If the dictionary even has the control
        {
            if (!KeyAlreadyUsed(NewKey)) //Checking if the keybind is already used
            {
                KeyMap ActiveKey = Keybinds[Control];
                EventLogger.LogEvent($"Updating Keybind for {Control}, OldKey - {ActiveKey.CurrentKey}, NewKey - {NewKey}", EventLogger.LogType.Info);
                ActiveKey.CurrentKey = NewKey;
                return true;
            }
            else //If keybind is already used
            {
                EventLogger.LogEvent($"Keybind for {Control} already exists", EventLogger.LogType.Warning);
                return false;
            }
        }
        else //If control isn't found
        {
            EventLogger.LogEvent($"No Keybind found for {Control}", EventLogger.LogType.Warning);
            return false;
        }
    }
    
    public static void ResetAllToDefault() //Resetting all the keybinds to their default value
    {
        EventLogger.LogEvent("Currently resetting all keys to default",EventLogger.LogType.Info);
        foreach (KeyMap Key in Keybinds.Values) //Going through each keybind int he dictionary
        {
            Key.CurrentKey = Key.DefaultKey;
            EventLogger.LogEvent($"Resetting Keybind for {Key.Control} - {Key.CurrentKey}", EventLogger.LogType.Info);
        }
        EventLogger.LogEvent("All keys reset to default", EventLogger.LogType.Info);
    }

    public static Keys GetKeyFromControl(string Control) //Getting the key of a control
    {
        if (Keybinds.ContainsKey(Control)) //Checking if it exists
        {
            EventLogger.LogEvent($"Lookup for {Control} - {Keybinds[Control].CurrentKey}", EventLogger.LogType.Info);
            return Keybinds[Control].CurrentKey;
        }
        EventLogger.LogEvent($"No Keybind found for {Control}", EventLogger.LogType.Warning);
        return Keys.None;
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
}
