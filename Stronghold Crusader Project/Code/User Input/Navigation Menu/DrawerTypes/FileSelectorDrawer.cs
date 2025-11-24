using Assimp;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class FileSelecterDrawer : BaseButtonDrawer //A drawer that will show files
{
    //Class Variables
    private string FilePath;
    private SpriteFont TextFont;
    private Color ButtonColour;
    private Texture2D Pixel;
    private float FontScale = 0.85f;
    
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
        int TextOffSetX = 5;
        int TextOffSetY = 10;
        int WhiteOffSet = 2;
        double Length = ActiveButton.Bounds.Width * 0.7;
        while (TextFont.MeasureString(FileName).X >= Length)
        {
            FileName.Substring(0, FileName.Length);
        }
        Color ActiveColour = ButtonColour;
        if (ActiveButton.Active)
        {
            ActiveColour = Color.FromNonPremultiplied(155, 155, 125, 200); //TODO CHANGE HOW TEXT WORKS IN BUTTONS SO IT IS ALWAYS CENTERED IN THE Y
        }
        ActiveSpriteBatch.Draw(Pixel, ActiveButton.Bounds, Color.White);
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(ActiveButton.Bounds.X + (WhiteOffSet / 2), ActiveButton.Bounds.Y + (WhiteOffSet / 2), ActiveButton.Bounds.Width - WhiteOffSet, ActiveButton.Bounds.Height - WhiteOffSet), ButtonColour);
        ActiveSpriteBatch.DrawString(TextFont, FileName, new Vector2(ActiveButton.Bounds.X + TextOffSetX, ActiveButton.Bounds.Y), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
        ActiveSpriteBatch.DrawString(TextFont, FileDate, new Vector2(ActiveButton.Bounds.X + (float)Length - TextOffSetX, ActiveButton.Bounds.Y), Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }
}

