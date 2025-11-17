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
        int HorizontalLines = (int)Math.Ceiling(Bounds.Width / (double)BoxSize) - 1;
        int VerticalLines = (int)Math.Ceiling(Bounds.Height / (double)BoxSize) - 1;
        DrawHorizontal(ActiveSpriteBatch, Bounds.X + BoxSize, Bounds.Y, HorizontalLines, LineTextures["Top"]); //Draw Top Line
        DrawHorizontal(ActiveSpriteBatch, Bounds.X + BoxSize, Bounds.Y + Bounds.Height - BoxSize, HorizontalLines, LineTextures["Bottom"]); //Draw Bottom Line
        DrawVertical(ActiveSpriteBatch, Bounds.X, Bounds.Y + BoxSize, VerticalLines, LineTextures["Left"]); //Draw Left Line
        DrawVertical(ActiveSpriteBatch, Bounds.X + Bounds.Width - BoxSize, Bounds.Y + BoxSize, VerticalLines, LineTextures["Right"]); //Draw Right Line
        ActiveSpriteBatch.Draw(LineTextures["TLCorner"], new Vector2(Bounds.X, Bounds.Y), Color.White); //Draw Top Left Corner
        ActiveSpriteBatch.Draw(LineTextures["TRCorner"], new Vector2(Bounds.X + Bounds.Width - BoxSize, Bounds.Y), Color.White); //Draw Top Right Corner 
        ActiveSpriteBatch.Draw(LineTextures["BLCorner"], new Vector2(Bounds.X, Bounds.Y + Bounds.Height - BoxSize), Color.White); //Draw Bottom Left Corner
        ActiveSpriteBatch.Draw(LineTextures["BRCorner"], new Vector2(Bounds.X + Bounds.Width - BoxSize, Bounds.Y + Bounds.Height - BoxSize), Color.White); //Draw Top Left Corner
    }

    private void DrawHorizontal(SpriteBatch ActiveSpriteBatch, int OffSetX, int OffSetY, int HorizontalLines, Texture2D TextureToUse)
    {
        for (int Count = 0; Count < HorizontalLines - 1; Count++)
        {
            Vector2 NewPosition = new Vector2(OffSetX + (BoxSize * Count), OffSetY);
            ActiveSpriteBatch.Draw(TextureToUse, NewPosition, Color.White);
        }

    }

    private void DrawVertical(SpriteBatch ActiveSpriteBatch, int OffSetX, int OffSetY, int HorizontalLines, Texture2D TextureToUse)
    {
        for (int Count = 0; Count < HorizontalLines - 1; Count++)
        {
            Vector2 NewPosition = new Vector2(OffSetX, OffSetY + (BoxSize * Count));
            ActiveSpriteBatch.Draw(TextureToUse, NewPosition, Color.White);
        }
    }
}

