using Assimp;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class FileSelecterDrawer : BaseButtonDrawer //A drawer that will show files
{
    //Class Variables
    private string FilePath;
    private SpriteFont TextFont;
    private Color ButtonColour;
    private Texture2D Pixel;
    private float FontScale = 1f;
    
    //Class Methods
    public FileSelecterDrawer(string Input_FilePath, SpriteFont Input_TextFont, Color Input_ButtonColour, Texture2D Input_Pixel)
    {
        FilePath = Input_FilePath;
        TextFont = Input_TextFont;
        ButtonColour = Input_ButtonColour;
        Pixel = Input_Pixel;
        Pixel.SetData(new[] { Color.White });
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton)
    {
        string FileDate = File.GetCreationTime(FilePath).ToShortDateString();
        string FileName = FilePath.Replace(SavesFolder, "");
        FileName = FileName.Replace(".json", "");
        int TextOffSetX = 10;
        double Length = ActiveButton.Bounds.Width * 0.75;
        while (TextFont.MeasureString(FileName).X * FontScale >= Length)
        {
            FileName.Substring(0, FileName.Length);
        }
        Color ActiveColour = ButtonColour;
        if (ActiveButton.Hover == true)
        {
            ActiveColour = Color.FromNonPremultiplied(150, 150, 120, 200);
        }
        float TextYOffset = (TextFont.MeasureString(FileDate).Y * FontScale) / 2f;
        ActiveSpriteBatch.Draw(Pixel, ActiveButton.Bounds, ActiveColour);
        DrawOutline(ActiveButton.Bounds, 1, ActiveSpriteBatch, Color.White);
        ActiveSpriteBatch.DrawString(TextFont, FileName, new Vector2(ActiveButton.Bounds.X + TextOffSetX, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
        ActiveSpriteBatch.DrawString(TextFont, FileDate, new Vector2(ActiveButton.Bounds.X - TextOffSetX + (float)Length, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }

    private void DrawOutline(Rectangle Bounds, int Stroke, SpriteBatch ActiveSpriteBatch, Color Colour)
    {
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Stroke), Colour); //Drawing the top stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Left stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - Stroke, Bounds.Width, Stroke), Colour); //Drawing the Bottom stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X + Bounds.Width - Stroke, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Right stroke
    }
}

