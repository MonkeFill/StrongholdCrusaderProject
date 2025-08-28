namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapTile
{
    //Class Variables
    public string TileKey;
    private bool Walkable;
    private Texture2D Texture;
    private Vector2 Position;
    
    //Methods
    public MapTile(string InputTileKey, Texture2D InputTexture, Vector2 InputPosition)
    {
        TileKey = InputTileKey;
        Walkable = true;
        Texture = InputTexture;
        Position = InputPosition;
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        Vector2 IsometricPosition = GridToStaggeredDraw();
        ActiveSpriteBatch.Draw(Texture, IsometricPosition,null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }

    public void UpdateWalkable(bool NewWalkable)
    {
        Walkable = NewWalkable;
    }

    public bool TileContains(Vector2 MousePosition) //Checks to see if the position of the mouse contains a tile 
    {
        //Halving to get the centres
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

    private Vector2 GridToStaggeredDraw()
    {
        float OffSetX = 0;
        if ((int)Position.Y % 2 != 0)//If it is an even row it will be shifted right by half a tile
        {
            OffSetX = TileWidth / 2f; 
        }
        float NewPositionX = (Position.X * TileWidth) + OffSetX;
        float NewPositionY = (Position.Y * (TileHeight / 2f));
        return new Vector2(NewPositionX, NewPositionY);
    }
    
}

