namespace Stronghold_Crusader_Project.Code.User_Input;

public class KeyMap //Keybind class that will store data about keybinds to be used
{
    //Class Variables
    public string Name;
    public Keys CurrentKey;
    public Keys DefaultKey;
    public Action KeybindAction;
    public KeybindActiveType KeybindActivationType;
    
    public KeyMap(string Input_Name, Keys Input_CurrentKey, Keys Input_DefaultKey, Action Input_KeybindAction, KeybindActiveType Input_KeybindActivationType)
    {
        Name = Input_Name;
        CurrentKey = Input_CurrentKey;
        DefaultKey = Input_DefaultKey;
        KeybindAction = Input_KeybindAction;
        KeybindActivationType = Input_KeybindActivationType;
    }
}

