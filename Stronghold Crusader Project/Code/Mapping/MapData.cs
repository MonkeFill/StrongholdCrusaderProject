using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Stronghold_Crusader_Project.Other;
using static Stronghold_Crusader_Project.Other.EventLogger;

namespace Stronghold_Crusader_Project.Mapping;
public class MapData
{
    public string[,] TileMap;
    private string[,] WalkSpace;
    private int MapHeight;
    private int MapLength;
    private int TileSize;

    // Tiles and the reference to their texture
    private Dictionary<string, string> TileReference = new Dictionary<string, string>(); //Reference to where the tiles type folder can be found
    private string TilesFolderFullPath = GlobalConfig.TilesFolderFullPath; //Folderpath for all the tile types
    private string TilesFolderPathFromContent = GlobalConfig.TilesFolderPathFromContent; //Folderpath from content for loading content

    public MapData()
    {
        MapHeight = GlobalConfig.MapHeght;
        MapLength = GlobalConfig.MapLength;
        TileSize = 20;
        TileMap = new string[MapHeight, MapLength];
        //Adding all the tiles
        TileReference.Add("W", "Water"); //Water
        TileReference.Add("L", "Land"); //Land
        TileReference.Add("R", "Rock"); //Rock
        TileReference.Add("S", "Stone"); //Stone
        TileReference.Add("I", "Iron"); //Iron
        TileReference.Add("G", "Grass"); //Grass
    }

    public void DrawMap(ContentManager Content, SpriteBatch ActiveSprite)
    {
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapLength; PositionX++)
            {
                string ActiveTile = TileMap[PositionY, PositionX];
                Texture2D ActiveTexture = LoadRandomTileTexture(TileReference[ActiveTile], Content);
                Vector2 ActivePosition = new Vector2(PositionX * TileSize, PositionY * TileSize);
                ActiveSprite.Draw(ActiveTexture, ActivePosition, Color.White);
            }
        }
    }

    public bool CheckIfWalkable(Vector2 ActiveCoordinate) //Method to check if a space is free
    {
        if (WalkSpace[(int)ActiveCoordinate.X, (int)ActiveCoordinate.Y] != " ") //Blank is space is free
        {
            return false;
        }
        return true;
    }

    public void ChangeWalkable(List<Vector2> PositionsToChange, string ChangeTo) //Making a part of the map either walkable or unwalkable
    {
        for (int Count = 0; Count < PositionsToChange.Count; Count++)
        {
            Vector2 ActivePosition = PositionsToChange[Count];
            WalkSpace[(int)ActivePosition.X, (int)ActivePosition.Y] = ChangeTo;
        }
    }

    private Texture2D LoadRandomTileTexture(string TileName, ContentManager Content)
    {
        string ActiveContentPath = Path.Combine(TilesFolderPathFromContent, TileName);
        string ActiveFilePath = Path.Combine(TilesFolderFullPath, TileName);
        if (Directory.Exists(ActiveFilePath))
        {
            LogEvent($"{ActiveFilePath} is being accessed for the tiles", LogType.Info);
            int FolderFilesCount = Directory.GetFiles(ActiveFilePath).Length;
            Random RNG = new Random();
            int TileVariantNumber = RNG.Next(1, FolderFilesCount + 1);
            string TexturePath = Path.Combine(ActiveContentPath, (TileName + TileVariantNumber.ToString()));
            return Content.Load<Texture2D>(TexturePath);
        }
        else
        {
            LogEvent($"{ActiveFilePath} does not exist!", LogType.Error);
        }
        return null;
    }

    public bool MapValid(string[,] ActiveMap)
    {
        if (ActiveMap.GetLength(0) != MapHeight || ActiveMap.GetLength(1) != MapLength) //If it is the correct size
        {
            EventLogger.LogEvent("Map isn't the correct size", EventLogger.LogType.Error);
            return false;
        }

        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapLength; PositionX++) //if any of the tiles are blank
            {
                string ActiveTile = ActiveMap[PositionY, PositionX];
                if (string.IsNullOrEmpty(ActiveTile))
                {
                    EventLogger.LogEvent($"Map is empty at [{PositionY}, {PositionX}]", EventLogger.LogType.Error);
                    return false;
                }
            }
        }

        return true;
    }


    public string MapAsText(string[,] ActiveMap)
    {
        StringBuilder MapText = new System.Text.StringBuilder();
        MapText.AppendLine("Map = [");
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapLength; PositionX++)
            {
                string ActiveTile = ActiveMap[PositionY, PositionX];
                MapText.Append(ActiveTile);
                if (PositionX < MapLength - 1)
                {
                    MapText.Append(", ");
                }
            }
            MapText.AppendLine();
        }
        MapText.AppendLine("]");
        return MapText.ToString();
    }
}
