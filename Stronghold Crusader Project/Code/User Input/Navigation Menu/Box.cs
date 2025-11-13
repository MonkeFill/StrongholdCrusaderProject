namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class Box
{
    Rectangle Bounds;
    List<string> TextureNames = new List<string> { "TLCorner", "TRCorner", "BLCorner", "BRCorner", "Top", "Bottom", "Left", "Right" };
    Dictionary<string, Texture2D> LineTextures = new Dictionary<string, Texture2D>();

    public Box(Rectangle Input_Bounds, ContentManager Content)
    {
        Bounds = Input_Bounds;
        foreach (string ActiveTextureName in TextureNames)
        {
            LineTextures.Add(ActiveTextureName, Content.Load<Texture2D>(Path.Combine(BoxMenuFolder, ActiveTextureName)));
        }
        TextureNames.Clear();
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        ActiveSpriteBatch.Draw(LineTextures["TLCorner"], new Vector2(Bounds.X, Bounds.Y), Color.White); //Draw Top Left Corner
        ActiveSpriteBatch.Draw(LineTextures["TRCorner"], new Vector2(Bounds.X - BoxWidth, Bounds.Y), Color.White); //Draw Top Right Corner //Will cause issues because im not subtracting how wide it is 8px, check later
        ActiveSpriteBatch.Draw(LineTextures["BLCorner"], new Vector2(Bounds.X, Bounds.Y - BoxWidth), Color.White); //Draw Bottom Left Corner
        ActiveSpriteBatch.Draw(LineTextures["BRCorner"], new Vector2(Bounds.X - BoxWidth, Bounds.Y - BoxWidth), Color.White); //Draw Top Left Corner
        int HorizontalLines = (int)Math.Ceiling((double)BoxWidth / Bounds.Width) - 2;
        int VerticalLines = (int)Math.Ceiling((double)BoxWidth / Bounds.Height) - 2;
    }

    private void DrawHorizontal(SpriteBatch ActiveSpriteBatch, int OffSetX, int OffSetY, int HorizontalLines, Texture2D TextureToUse)
    {
        int StartX = BoxWidth + OffSetX;
        int StartY = 0 + OffSetY;
        for (int Count = 0; Count < HorizontalLines - 1; Count++)
        {
            Vector2 NewPosition = new Vector2(StartX + (BoxWidth * BoxWidth));
            ActiveSpriteBatch.Draw(TextureToUse, NewPosition, Color.White);
        }
        ActiveSpriteBatch.Draw(TextureToUse, new Vector2(Bounds.X - (2 * BoxWidth)), Color.White);

    }
}

