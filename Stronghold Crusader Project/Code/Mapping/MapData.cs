using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Stronghold_Crusader_Project.Other;
using JsonException = System.Text.Json.JsonException;

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
    private string TileFolderPath = ""; //Folderpath for all the tile types

    public MapData()
    {
        MapHeight = GlobalConfig.MapHeght;
        MapLength = GlobalConfig.MapLength;
        TileSize = 20;
        TileMap = new string[MapHeight, MapLength];
        //Adding all the tiles
        TileReference.Add("W", Path.Combine(TileFolderPath, "Water")); //Water
        TileReference.Add("G", Path.Combine(TileFolderPath, "Grass")); //Grass
        TileReference.Add("L", Path.Combine(TileFolderPath, "Land")); //Land
        TileReference.Add("S", Path.Combine(TileFolderPath, "Stone")); //Stone
        TileReference.Add("I", Path.Combine(TileFolderPath, "Iron")); //Iron
    }

    public void DrawMap(ContentManager Content, SpriteBatch ActiveSprite)
    {
        for (int HeightCount = 0; HeightCount < MapHeight; HeightCount++)
        {
            for (int LengthCount = 0; LengthCount < MapLength; LengthCount++)
            {
                string ActiveTile = TileMap[LengthCount, HeightCount];
                Texture2D ActiveTexture = LoadRandomTileTexture(TileReference[ActiveTile], Content);
                Vector2 ActivePosition = new Vector2(LengthCount * TileSize, HeightCount * TileSize);
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

    private Texture2D LoadRandomTileTexture(string FolderPath, ContentManager Content)
    {
        int FolderFilesCount = Directory.GetFiles(FolderPath).Length;
        Random RNG = new Random();
        int TileVariantNumber = RNG.Next(1, FolderFilesCount + 1);
        string FolderName = Path.GetFileName(FolderPath);
        string TexturePath = Path.Combine(FolderPath, FolderName + TileVariantNumber.ToString());
        return Content.Load<Texture2D>(TexturePath);
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
