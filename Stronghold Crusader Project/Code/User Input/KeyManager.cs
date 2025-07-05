using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Stronghold_Crusader_Project.Other;

namespace Stronghold_Crusader_Project.Code.User_Input;

public static class KeyManager //Keybinds that will be used to control everything
{
    public static readonly Dictionary<string, KeyMap> Keybinds = new();

    public static void CreateDefaults()
    {
        AddNewKeybind("MoveUp", Keys.W);
        AddNewKeybind("MoveDown", Keys.S);
        AddNewKeybind("MoveLeft", Keys.A);
        AddNewKeybind("MoveRight", Keys.D);
        AddNewKeybind("RotateCameraLeft", Keys.Q);
        AddNewKeybind("RotateCameraRight", Keys.E);
        AddNewKeybind("PreviousMenu", Keys.Escape);
        AddNewKeybind("test", Keys.Escape);
        RemoveKeybind("MoveUp");
        RemoveKeybind("MoveUp");
        UpdateKeybind("MoveUp", Keys.Up);
        UpdateKeybind("MoveDown", Keys.Down);
        UpdateKeybind("MoveDown", Keys.A);
        GetKeyFromControl("MoveUp");
        GetKeyFromControl("MoveDown");
        ResetAllToDefault();

    }
        
    public static void AddNewKeybind(string Control, Keys DefaultKey)
    {
        if (Keybinds.ContainsKey(Control))
        {
            EventLogger.LogEvent($"Keybind for {Control} is used somewhere else", EventLogger.LogType.Warning);
        }
        else if (KeyAlreadyUsed(DefaultKey))
        {
            EventLogger.LogEvent($"Keybind for {Control} - {DefaultKey} already used", EventLogger.LogType.Warning);
        }
        else
        {

            KeyMap NewKey = new KeyMap(Control, DefaultKey);
            Keybinds.Add(Control, NewKey);
            EventLogger.LogEvent($"Added new keybind for {Control} - {NewKey.CurrentKey}", EventLogger.LogType.Info);
        }
    }
    
    public static void RemoveKeybind(string Control)
    {
        if (Keybinds.ContainsKey(Control))
        {
            KeyMap ActiveKey =  Keybinds[Control];
            EventLogger.LogEvent($"Removed Keybind {Control} - {ActiveKey.CurrentKey}", EventLogger.LogType.Info);
            Keybinds.Remove(Control);
        }
        else
        {
            EventLogger.LogEvent($"No Keybind found for {Control} to be removed", EventLogger.LogType.Warning);
        }
    }

    public static bool UpdateKeybind(string Control, Keys NewKey)
    {
        if (Keybinds.ContainsKey(Control))
        {
            if (!KeyAlreadyUsed(NewKey))
            {
                KeyMap ActiveKey = Keybinds[Control];
                EventLogger.LogEvent($"Updating Keybind for {Control}, OldKey - {ActiveKey.CurrentKey}, NewKey - {NewKey}", EventLogger.LogType.Info);
                ActiveKey.CurrentKey = NewKey;
                return true;
            }
            else
            {
                EventLogger.LogEvent($"Keybind for {Control} already exists", EventLogger.LogType.Warning);
                return false;
            }
        }
        else
        {
            EventLogger.LogEvent($"No Keybind found for {Control}", EventLogger.LogType.Warning);
            return false;
        }
    }
    
    public static void ResetAllToDefault()
    {
        EventLogger.LogEvent("Currently resetting all keys to default",EventLogger.LogType.Info);
        foreach (KeyMap Key in Keybinds.Values)
        {
            Key.CurrentKey = Key.DefaultKey;
            EventLogger.LogEvent($"Resetting Keybind for {Key.Control} - {Key.CurrentKey}", EventLogger.LogType.Info);
        }
        EventLogger.LogEvent("All keys reset to default", EventLogger.LogType.Info);
    }

    public static Keys GetKeyFromControl(string Control)
    {
        if (Keybinds.ContainsKey(Control))
        {
            EventLogger.LogEvent($"Lookup for {Control} - {Keybinds[Control].CurrentKey}", EventLogger.LogType.Info);
            return Keybinds[Control].CurrentKey;
        }
        EventLogger.LogEvent($"No Keybind found for {Control}", EventLogger.LogType.Warning);
        return Keys.None;
    }

    public static bool KeyAlreadyUsed(Keys KeyChange)
    {
        foreach (KeyMap ActiveKey in Keybinds.Values)
        {
            if (ActiveKey.CurrentKey == KeyChange)
            {
                return true;
            }
        }
        return false;
    }
}
