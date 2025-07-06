using System;
using System.IO;
using Stronghold_Crusader_Project.Other;

namespace Stronghold_Crusader_Project;

public static class GlobalConfig
{
    private static string CurrentDirectory = Environment.CurrentDirectory; //Directory of where the .csproj is 
    private static string DefaultFolder = Directory.GetParent(CurrentDirectory).Parent.Parent.FullName;
    public static string GameDataFolder = Path.Combine(DefaultFolder, "GameData");
    public static string MapsFolder = Path.Combine(GameDataFolder, "Maps");
    public static string SavesFolder = Path.Combine(GameDataFolder, "Saves");

    public static int MapHeght = 3;
    public static int MapLength = 3;
    public static void CheckGameDataFolder()
    {
        if (Directory.Exists(GameDataFolder)) //Does exist which it should do
        {
            EventLogger.LogEvent("Found GameData folder", EventLogger.LogType.Info);
            if (Directory.Exists(MapsFolder))
            {
                EventLogger.LogEvent("Found Maps folder", EventLogger.LogType.Info);
            }
            else
            {
                CreateMapsFolder();
            }
            if (Directory.Exists(SavesFolder))
            {
                EventLogger.LogEvent("Found Saves folder", EventLogger.LogType.Info);
            }
            else
            {
                CreateSavesFolder();
            }
        }
        else
        {
            EventLogger.LogEvent("GameData Folder not found", EventLogger.LogType.Error);
            Directory.CreateDirectory(GameDataFolder);
            EventLogger.LogEvent($"GameData Folder created at {GameDataFolder}", EventLogger.LogType.Info);
            CreateMapsFolder();
            CreateSavesFolder();
        }
    }

    private static void CreateMapsFolder()
    {
        EventLogger.LogEvent("MapsFolder folder not found", EventLogger.LogType.Error);
        Directory.CreateDirectory(MapsFolder);
        EventLogger.LogEvent($"MapsFolder created at {MapsFolder}", EventLogger.LogType.Info);
    }
    private static void CreateSavesFolder()
    {
        EventLogger.LogEvent("SavesFolder folder not found", EventLogger.LogType.Error);
        Directory.CreateDirectory(SavesFolder);
        EventLogger.LogEvent($"SavesFolder created at {SavesFolder}", EventLogger.LogType.Info);
    }
}
