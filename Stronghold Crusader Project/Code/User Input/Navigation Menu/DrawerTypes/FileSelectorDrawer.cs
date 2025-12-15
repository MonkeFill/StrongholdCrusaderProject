namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class FileSelecterDrawer : BaseButtonDrawer //A drawer that will show file buttons
{
    //Class Variables
    private string FilePath;
    private SpriteFont TextFont;
    private Color ButtonColour;
    private Color HoverButtonColour;
    private Color ActiveButtonColour;
    private Texture2D Pixel;
    private float FontScale;
    private float ScaleForFileName = 0.6f;
    private int TextOffSetX = 10;
    
    //Class Methods
    public FileSelecterDrawer(string Input_FilePath, SpriteFont Input_TextFont, Color Input_ButtonColour, Color Input_HoverButtonColour, Color Input_ActiveButtonColour, Texture2D Input_Pixel, float Input_FontScale)
    {
        FilePath = Input_FilePath;
        TextFont = Input_TextFont;
        ButtonColour = Input_ButtonColour;
        HoverButtonColour = Input_HoverButtonColour;
        ActiveButtonColour = Input_ActiveButtonColour;
        Pixel = Input_Pixel;
        Pixel.SetData(new[] { Color.White });
        FontScale = Input_FontScale;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton) //Drawing the button
    {
        string FileDate = "";
        string FileName = "";
        if (FilePath != "null" && FilePath != "title")
        { 
            FileDate = File.GetCreationTime(FilePath).ToShortDateString();
            FileName = Path.GetFileNameWithoutExtension(FilePath); //Getting file information
        }
        else if (FilePath == "title")
        {
            FileName = "File Name";
            FileDate = "Date";
        }

        FileName = FileName.Replace(".json", "");
        double Length = ActiveButton.Bounds.Width * ScaleForFileName;
        while (TextFont.MeasureString(FileName).X * FontScale >= Length - Length * 0.1) //Shortening the file name if its too long to fit
        {
            FileName = FileName.Substring(0, FileName.Length - 1);
        }
        Color ActiveColour = ButtonColour;

        if (ActiveButton.Active) //if button is active
        {
            if (CheckIfNormalButton(FileName, FilePath))
            {
                ActiveColour = ActiveButtonColour;
            }
        }
        else if (ActiveButton.Hover) //If button is hovered over
        {
            if (CheckIfNormalButton(FileName, FilePath))
            {
                ActiveColour = HoverButtonColour;
            }
        }

        float TextYOffset = (TextFont.MeasureString(FileDate).Y * FontScale) / 2f;
        ActiveSpriteBatch.Draw(Pixel, ActiveButton.Bounds, ActiveColour);
        DrawOutline(ActiveButton.Bounds, 1, ActiveSpriteBatch, Color.FromNonPremultiplied(245, 245, 245, 150));
        ActiveSpriteBatch.DrawString(TextFont, FileName, new Vector2(ActiveButton.Bounds.X + TextOffSetX, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
        ActiveSpriteBatch.DrawString(TextFont, FileDate, new Vector2(ActiveButton.Bounds.X + (float)Length, ActiveButton.Bounds.Y + (ActiveButton.Bounds.Height / 2f) - TextYOffset), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }

    private void DrawOutline(Rectangle Bounds, int Stroke, SpriteBatch ActiveSpriteBatch, Color Colour) //Draws 4 boxes to create an outline for something
    {
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Left stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - Stroke, Bounds.Width, Stroke), Colour); //Drawing the Bottom stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X + Bounds.Width - Stroke, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Right stroke
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(Bounds.X + (int)(Bounds.Width * ScaleForFileName) - (int)(TextOffSetX * 0.5)- Stroke, Bounds.Y, Stroke, Bounds.Height), Colour); //Drawing the Right stroke
    }

    private bool CheckIfNormalButton(string FileName, string FilePath) //Makes sure it is not a title button
    {
        if (FileName != "" && FilePath != "title")
        {
            return true;
        }
        return false;
    }
}

