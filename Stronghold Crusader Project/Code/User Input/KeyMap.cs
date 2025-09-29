namespace Stronghold_Crusader_Project.Code.User_Input;

public class KeyMap //Keybind class so it has both current key, default key, ways to reset it, update it and make sure it all gets logged
{
    //Class Variables
    public string Control { get;}
    public Keys CurrentKey {get; set;}
    [JsonIgnore] //Won't save the default key
    public Keys DefaultKey {get;}

    [JsonConstructor]
    //Methods
    public KeyMap(string Control, Keys CurrentKey)
    {
        this.Control = Control;
        this.CurrentKey = CurrentKey;
        this.DefaultKey = CurrentKey;
    }

    public void UpdateKeybind(Keys NewKeybind)
    {
        try //If keybind isn't valid to catch it
        {
            CurrentKey = NewKeybind;
        }
        catch
        {
            LogEvent($"Failed to update keybind for {Control} to {NewKeybind}", EventLogger.LogType.Error);
        } 
    }
    public void ResetKeybind()
    {
        CurrentKey = DefaultKey;
    }
    
}
