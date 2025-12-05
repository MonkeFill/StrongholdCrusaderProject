namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class FileSelecterDrawer : BaseButtonDrawer //A drawer that will show file buttons
{
    //Class Variables
    private string FilePath;
    private SpriteFont TextFont;
    private Color ButtonColour;
    private Color ActiveButtonColour;
    private Texture2D Pixel;
    private float FontScale = 1f;
    
    //Class Methods
    public FileSelecterDrawer(string Input_FilePath, SpriteFont Input_TextFont, Color Input_ButtonColour, Color Input_ActiveButtonColour, Texture2D Input_Pixel)
    {
        FilePath = Input_FilePath;
        TextFont = Input_TextFont;
        ButtonColour = Input_ButtonColour;
        ActiveButtonColour = Input_ActiveButtonColour;
        Pixel = Input_Pixel;
        Pixel.SetData(new[] { Color.White });
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton) //Drawing the button
    {
        string FileDate = "";
        string FileName = "";
        if (FilePath != "null")
        { 
            FileDate = File.GetCreationTime(FilePath).ToShortDateString();
            FileName = FilePath.Replace(SavesFolder, ""); //Getting file information
        }

        FileName = FileName.Replace(".json", "");
        int TextOffSetX = 10;
        double Length = ActiveButton.Bounds.Width * 0.75;
        while (TextFont.MeasureString(FileName).X * FontScale >= Length) //Shortening the file name if its too long to fit
        {
            FileName.Substring(0, FileName.Length);
        }
        Color ActiveColour = ButtonColour;
        if (ActiveButton.Hover == true) //If button is hovered over
        {
            ActiveColour = ActiveButtonColour;
        }
        float TextYOffset = (TextFont.MeasureString(FileDate).Y * FontScale) / 2f;
        ActiveSpriteBatch.Draw(Pixel, ActiveButton.Bounds, ActiveColour);
        DrawOutline(ActiveButton.Bounds, 1, ActiveSpriteBatch, Color.White);
        ActiveSpriteBatch.DrawString(TextFont, FileName, new Vector2(ActiveButton.Bounds.X + TextOffSetX, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
        ActiveSpriteBatch.DrawString(TextFont, FileDate, new Vector2(ActiveButton.Bounds.X - TextOffSetX + (float)Length, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }

    private void DrawOutline(Rectangle Bounds, int Stroke, SpriteBatch ActiveSpriteBatch, Color Colour) //Draws 4 boxes to create an outline for something
    {
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Stroke), Colour); //Drawing the top stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Left stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - Stroke, Bounds.Width, Stroke), Colour); //Drawing the Bottom stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X + Bounds.Width - Stroke, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Right stroke
    }
}

