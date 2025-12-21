namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapTile //Class for each individual tile on the map
{
    //Class Variables
    public string TileKey;
    public bool Walkable;
    private Texture2D Texture;
    private Vector2 Position;
    
    //Methods
    public MapTile(string InputTileKey, Texture2D InputTexture, Vector2 InputPosition) //Initializer
    {
        TileKey = InputTileKey;
        Walkable = true;
        Texture = InputTexture;
        Position = InputPosition;
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch) //Draw method for a tile
    {
        Vector2 IsometricPosition = GridToStaggeredDraw();
        ActiveSpriteBatch.Draw(Texture, IsometricPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
    
    private Vector2 GridToStaggeredDraw() //Method to convert grid position to staggered position for drawing
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

