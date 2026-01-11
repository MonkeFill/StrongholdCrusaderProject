namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// This class is a part of what makes up the map within the code, it stores information about possible tile types
/// information in tile type will be used to make up the tiles for the map
/// </summary>

public class TileType
{
    //Class Variables
    public string Name { get; private set; }
    public string Category { get; private set; }
    public Color MinimapColor { get; private set; }
    public Texture2D Texture { get; private set; }
    public bool IsWalkable { get; private set; }
    
    //Class Methods
    public TileType(string InputCategory, string InputName, Texture2D InputTexture, bool InputIsWalkable = true)
    {
        Category = InputCategory;
        Name = InputName;
        Texture = InputTexture;
        IsWalkable = InputIsWalkable;
        MinimapColor = GetMiniMapColour();
    }
    
    #region Helper Methods
    //Functions that help with the class

    private Color GetMiniMapColour() //A function that gets the middle tile of a texture to return its colour
    {
        if (Texture != null) //Making sure the texture is there
        {
            Color[] Data = new Color[1];
            Texture.GetData(0, new Rectangle(Texture.Width / 2, Texture.Height / 2, 1, 1), Data, 0, 1); //Retrieving the pixel data from the texture
            return Data[0];
        }
        LogEvent($"{Name} has no texture", LogType.Error);
        return Color.White;
    }
    
    #endregion
}

