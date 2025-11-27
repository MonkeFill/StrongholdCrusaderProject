namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class Box //Drawing a custom rectangle that has multiple colours and works a bit differently
{
    //Class Variables
    public Rectangle Bounds;
    private Texture2D Pixel;
    private Color BoxBackground;
    private List<string> TextureNames = new List<string> { "TLCorner", "TRCorner", "BLCorner", "BRCorner", "Top", "Bottom", "Left", "Right" };
    private Dictionary<string, Texture2D> LineTextures = new Dictionary<string, Texture2D>();
    private int BoxBigSize = 24;
    private int BoxSmallSize = 8;

    //Class Methods
    public Box(Rectangle Input_Bounds, Color Input_BoxBackground, ContentManager Content, GraphicsDevice Graphics)
    {
        Bounds = Input_Bounds;
        BoxBackground = Input_BoxBackground;
        foreach (string ActiveTextureName in TextureNames) //Adding in every texture
        {
            LineTextures.Add(ActiveTextureName, Content.Load<Texture2D>(Path.Combine(BoxMenuFolder, ActiveTextureName)));
        }
        TextureNames.Clear();
        Pixel = new Texture2D(Graphics, 1, 1);
        Pixel.SetData(new[] { Color.White });
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //Drawing everything needed
    {
        ActiveSpriteBatch.Draw(Pixel, Bounds, BoxBackground);
        int HorizontalLines = (int)Math.Ceiling(Bounds.Width / (double)BoxBigSize) - 1;
        int VerticalLines = (int)Math.Ceiling(Bounds.Height / (double)BoxBigSize) - 1;
        DrawHorizontal(ActiveSpriteBatch, Bounds.X + BoxBigSize, Bounds.Y, HorizontalLines, LineTextures["Top"]); //Draw Top Line
        DrawHorizontal(ActiveSpriteBatch, Bounds.X + BoxBigSize, Bounds.Y + Bounds.Height - BoxSmallSize, HorizontalLines, LineTextures["Bottom"]); //Draw Bottom Line
        DrawVertical(ActiveSpriteBatch, Bounds.X, Bounds.Y + BoxBigSize, VerticalLines, LineTextures["Left"]); //Draw Left Line
        DrawVertical(ActiveSpriteBatch, Bounds.X + Bounds.Width - BoxSmallSize, Bounds.Y + BoxBigSize, VerticalLines, LineTextures["Right"]); //Draw Right Line
        ActiveSpriteBatch.Draw(LineTextures["TLCorner"], new Vector2(Bounds.X, Bounds.Y), Color.White); //Draw Top Left Corner
        ActiveSpriteBatch.Draw(LineTextures["TRCorner"], new Vector2(Bounds.X + Bounds.Width - BoxBigSize, Bounds.Y), Color.White); //Draw Top Right Corner 
        ActiveSpriteBatch.Draw(LineTextures["BLCorner"], new Vector2(Bounds.X, Bounds.Y + Bounds.Height - BoxBigSize), Color.White); //Draw Bottom Left Corner
        ActiveSpriteBatch.Draw(LineTextures["BRCorner"], new Vector2(Bounds.X + Bounds.Width - BoxBigSize, Bounds.Y + Bounds.Height - BoxBigSize), Color.White); //Draw Top Left Corner
    }

    private void DrawHorizontal(SpriteBatch ActiveSpriteBatch, int OffSetX, int OffSetY, int HorizontalLines, Texture2D TextureToUse) //Draw method for horizontal lines
    {
        for (int Count = 0; Count < HorizontalLines - 1; Count++)
        {
            Vector2 NewPosition = new Vector2(OffSetX + (BoxBigSize * Count), OffSetY);
            ActiveSpriteBatch.Draw(TextureToUse, NewPosition, Color.White);
        }

    }

    private void DrawVertical(SpriteBatch ActiveSpriteBatch, int OffSetX, int OffSetY, int HorizontalLines, Texture2D TextureToUse) //Draw method for vertical lines
    {
        for (int Count = 0; Count < HorizontalLines - 1; Count++)
        {
            Vector2 NewPosition = new Vector2(OffSetX, OffSetY + (BoxBigSize * Count));
            ActiveSpriteBatch.Draw(TextureToUse, NewPosition, Color.White);
        }
    }
}

