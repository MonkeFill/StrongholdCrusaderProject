namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapTile
{
    public string TileKey;
    bool Walkable;
    Texture2D Texture;
    Vector2 Position;
    
    //Static readonly variables that will be used across multiple methods but don't want it to update constantly or keep storing it in every instance
    static readonly int TileWidth = GlobalConfig.TileWidth;
    static readonly int TileHeight = GlobalConfig.TileHeight;
    
    public MapTile(string InputTileKey, Texture2D InputTexture, Vector2 InputPosition)
    {
        TileKey = InputTileKey;
        Walkable = true;
        Texture = InputTexture;
        Position = InputPosition;
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        Vector2 TileCentre = new Vector2(TileWidth / 2f, TileHeight / 2f);
        Vector2 IsometricPosition = ConvertPositionToIsometric();
        ActiveSpriteBatch.Draw(Texture, IsometricPosition + TileCentre,null, Color.White, Camera2D.Rotation, TileCentre, 1f, SpriteEffects.None, 0f);
    }

    public void UpdateWalkable(bool NewWalkable)
    {
        Walkable = NewWalkable;
    }

    public bool TileContains(Vector2 MousePosition) //Checks to see if the position of the mouse contains a tile 
    {
        //Halfing to get the centres
        float TileHalfWidth = TileWidth / 2.0f;
        float TileHalfHeight = TileHeight / 2.0f;
        
        //getting the centre of the tile
        float TileCentreX = Position.X + TileHalfWidth;
        float TileCentreY = Position.Y + TileHalfHeight;
        
        //Getting how far it is from the centre and then turning it into a percentage based off the tile size
        float PercentageAwayX = MathF.Abs((MousePosition.X - TileCentreX) / TileHalfWidth);
        float PercentageAwayY = MathF.Abs((MousePosition.Y - TileCentreY) / TileHalfHeight);
        
        //To check bounds for a diamond you have to make sure the percentage away it is from the centre is equal to 1 since you can't give defined bounds like a square
        if (PercentageAwayX + PercentageAwayY <= 1)
        {
            return true;
        }
        return false;
    }

    private Vector2 ConvertPositionToIsometric()
    {
        float IsometricPositionX = (Position.X - Position.Y) * (TileWidth / 2.0f); 
        float IsometricPositionY = (Position.Y + Position.X) * (TileHeight / 2.0f);
        return new Vector2(IsometricPositionX, IsometricPositionY);
    }
    
}

