namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// A class that handles drawing borders around the map
/// </summary>
///
/// 
public class Borders 
{
    //Class Variables
    private struct BorderPiece //structure for each individual border
    {
        public Vector2 Position;
        public Texture2D Texture;

        public BorderPiece(Vector2 InputPosition, Texture2D InputTexture)
        {
            Position = InputPosition;
            Texture = InputTexture;
        }
    }
    private List<BorderPiece> BorderList = new List<BorderPiece>();
    private Texture2D DefaultTexture;
    private Texture2D CornerBorderTexture;
    private Texture2D ShortBorderTexture;
    private Texture2D NarrowBorderTexture;
    
    //Class Methods
    public Borders(ContentManager Content)
    {
        LoadTextures(Content);
        CreateBorders();
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //Draws all the borders
    {
        foreach (BorderPiece ActiveBorder in BorderList)
        {
            ActiveSpriteBatch.Draw(ActiveBorder.Texture, ActiveBorder.Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
    
    #region Helper Methods
    //Functions to help the class

    private void LoadTextures(ContentManager Content) //Function that loads all the border textures 
    {
        LogEvent("Loading border textures", LogType.Info);
        DefaultTexture = Content.Load<Texture2D>(Path.Combine(BorderFolder, DefaultBorderName));
        ShortBorderTexture = Content.Load<Texture2D>(Path.Combine(BorderFolder, ShortBorderName));
        NarrowBorderTexture = Content.Load<Texture2D>(Path.Combine(BorderFolder, NarrowBorderName));
        LogEvent("Loaded border textures successfully", LogType.Info);
    }

    private void CreateBorders()
    {
        CreateHorizontalEdge(-BorderSize.X, -BorderSize.Y); //Top Edge
        CreateVerticalEdge(-BorderSize.X, 0); //Left Edge
        CreateHorizontalEdge(-BorderSize.X, MapSize.Y); //Bottom Edge
        CreateVerticalEdge(MapSize.X , 0); //Right Edge
    }

    private void CreateHorizontalEdge(int StartX, int StartY)
    {
        int Amount = (MapSize.X / BorderSize.X) + 2;
        for (int Count = 0; Count < Amount; Count++)
        {
            Texture2D ActiveTexture = DefaultTexture;
            if (Count == Amount / 2) //Middle piece
            {
                //ActiveTexture = NarrowBorderTexture;
            }
            Vector2 ActivePosition = new Vector2(StartX, StartY);
            BorderList.Add(new BorderPiece(ActivePosition, ActiveTexture));
            StartX += ActiveTexture.Width;
        }
    }
    
    private void CreateVerticalEdge(int StartX, int StartY)
    {
        int Amount = (MapSize.Y / BorderSize.Y);
        for (int Count = 0; Count < Amount; Count++)
        {
            Texture2D ActiveTexture = DefaultTexture;
            if (Count == Amount / 2) //Middle piece
            {
                //ActiveTexture = ShortBorderTexture;
            }
            Vector2 ActivePosition = new Vector2(StartX, StartY);
            BorderList.Add(new BorderPiece(ActivePosition, ActiveTexture));
            StartY += ActiveTexture.Height;
        }
    }
    
    #endregion
}
