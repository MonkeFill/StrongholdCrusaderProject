using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Stronghold_Crusader_Project.Other;

namespace Stronghold_Crusader_Project.Mapping;

public static class MapHandler //Handles Map logic
{
    static private MapData Map = new MapData(); 
    static string ActiveMapName = "TestMap";
    static string MapFolder = GlobalConfig.MapsFolder;
    static string MapPath = "";
    
    public static void AddTestData()
    {
        Map.TileMap[0, 0] = "G";
        Map.TileMap[0, 1] = "G";
        Map.TileMap[0, 2] = "L";
        Map.TileMap[1, 0] = "W";
        Map.TileMap[1, 1] = "R";
        Map.TileMap[1, 2] = "G";
        Map.TileMap[2, 0] = "W";
        Map.TileMap[2, 1] = "R";
        Map.TileMap[2, 2] = "G";
    }

    public static void DisplayMap(ContentManager Content, SpriteBatch spriteBatch)
    {
        Map.DrawMap(Content, spriteBatch);
    }

    public static void ExportMap()
    {
        MapPath = Path.Combine(MapFolder, (ActiveMapName + ".json"));
        if (File.Exists(MapPath))
        {
            EventLogger.LogEvent($"{MapPath} already exists!", EventLogger.LogType.Warning);
        }
            string Json = JsonConvert.SerializeObject(Map.TileMap, Formatting.Indented);
            File.WriteAllText(MapPath, Json);
            EventLogger.LogEvent($"Map {Json} saved to {MapPath}", EventLogger.LogType.Info);
    }

    public static bool ImportMap(string MapName)
    {
        ActiveMapName = MapName;
        MapPath = Path.Combine(MapFolder, (ActiveMapName + ".json"));
        if (File.Exists(MapPath)) //Checking if the map eists
        {
            string Json = File.ReadAllText(MapPath);
            string[,] LoadedMap = new string[,]{};
            EventLogger.LogEvent($"{MapPath} found!", EventLogger.LogType.Info);
            try //Trying to deseralise it
            {
                LoadedMap = JsonConvert.DeserializeObject<string[,]>(Json);
                if (Map.MapValid(LoadedMap)) //Checking if it is valid
                {
                    EventLogger.LogEvent($"Map {MapName} is a valid map!", EventLogger.LogType.Info);
                }
                else
                {
                    return false;
                }
                Map.TileMap = LoadedMap;
                EventLogger.LogEvent($"Map {MapName} has been imported!", EventLogger.LogType.Info);
                EventLogger.LogEvent($"New map looks like this {Map.MapAsText(Map.TileMap)}", EventLogger.LogType.Info);
                return true;
            }
            catch (JsonSerializationException) //If it can't deseralise it
            {
                EventLogger.LogEvent($"Map {MapName} is not in the correct format or size!", EventLogger.LogType.Error);
            }
            catch (Exception Error) //Other errors
            {
                EventLogger.LogEvent($"Map {MapName} could not be loaded {Error.Message}!", EventLogger.LogType.Error);
            }
        }
        else //Not found
        {
            EventLogger.LogEvent($"Map {MapPath} does not exist!", EventLogger.LogType.Error);
            return false;
        }
        return false;
    }
}

