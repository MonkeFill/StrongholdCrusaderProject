namespace Stronghold_Crusader_Project.Code.User_Input;

/// <summary>
/// The key manager is a class that will hold all information about a set of keybinds
/// the key manager can then be used to check what should happen if a key is pressed
/// </summary>

public class KeyManager
{
    //Class Variables
    private Dictionary<KeyAction, Keys> Keybinds;
    
    //Class Methods
    public KeyManager()
    {
        Keybinds = new Dictionary<KeyAction, Keys>();
        InitialiseDefaultKeybinds();
        Console.WriteLine();
    }
    
    #region Public Facing 
    //Classes that are public facing and to be used by the other clases

    public Keys GetKeyFromKeybind(KeyAction ActiveKeybind) //returns the key of a keybind
    {
        if (KeybindExists(ActiveKeybind))
        {
            return Keybinds[ActiveKeybind];
        }
        return Keys.None;
    }

    public void KeyRebind(KeyAction ActiveKeybind, Keys NewKey) //a method to add a new key to a keybind
    {
        if (KeybindExists(ActiveKeybind))
        {
            if (!KeyInUse(NewKey))
            {
                Keybinds[ActiveKeybind] = NewKey;
            }
        }
    }
    
    #endregion
    
    #region File Managing
    //Classes that handle importing and exporting the keybinds

    public void SaveBindings()//Exporting the keybinds to the file
    {
        string Json = JsonConvert.SerializeObject(Keybinds, Formatting.Indented);
        File.WriteAllText(KeybindsFile, Json);
    }

    public void LoadBindings() //Importing the keybinds to the game from the file
    {
        if (File.Exists(KeybindsFile))
        {
            string Json = File.ReadAllText(KeybindsFile);
            Dictionary<KeyAction, Keys> NewKeybinds = JsonConvert.DeserializeObject<Dictionary<KeyAction, Keys>>(Json);
            foreach (KeyValuePair<KeyAction, Keys> ActiveKeybind in NewKeybinds)
            {
                if (KeybindExists(ActiveKeybind.Key))
                {
                    Keybinds[ActiveKeybind.Key] = ActiveKeybind.Value;
                }
            }
        }
        else
        {
            LogEvent($"{KeybindsFile} not found", LogType.Info);
        }
    }
    
    #endregion
    
    #region Helpers
    //Classes that are here to help the class

    private bool KeybindExists(KeyAction ActiveKeybind) //Checks if the keybind exists
    {
        if (Keybinds.ContainsKey(ActiveKeybind))
        {
            return true;
        }
        return false;
    }

    private bool KeyInUse(Keys KeyToCheck) //Checks if a key is already being used
    {
        foreach (Keys ActiveKey in Keybinds.Values)
        {
            if (KeyToCheck == ActiveKey)
            {
                return true;
            }
        }
        return false;
    }

    private void AddKeybind(KeyAction ActiveKeybind, Keys NewKey) //a method to add a new keybind
    {
        if (!KeybindExists(ActiveKeybind))
        {
            Keybinds.Add(ActiveKeybind, NewKey);
        }
        else
        {
            LogEvent($"{ActiveKeybind} has already been added", LogType.Warning);
        }
    }

    private void InitialiseDefaultKeybinds() //Creates the default keybinds
    {
        AddKeybind(KeyAction.CameraUp, Keys.W);
        AddKeybind(KeyAction.CameraDown, Keys.S);
        AddKeybind(KeyAction.CameraLeft, Keys.A);
        AddKeybind(KeyAction.CameraRight, Keys.D);
        AddKeybind(KeyAction.CameraRotateLeft, Keys.E);
        AddKeybind(KeyAction.CameraRotateRight, Keys.Q);
        AddKeybind(KeyAction.MenuBack, Keys.Escape);
    }
    
    #endregion
}

