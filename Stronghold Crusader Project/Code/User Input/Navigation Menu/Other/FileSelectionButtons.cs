namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;

public class FileSelectionButtons
{
    //Class Variables
    private string ActiveDirectory;
    private string FileExtension;
    private List<string> FileNames;
    private int CurrentFileSkip = 0;
    private int FileHeight = 35;
    private float FontScale;
    private Rectangle Bounds;
    private SpriteFont Font;
    private Texture2D Pixel;
    private Box SavesBox;

    //Class Methods
    public FileSelectionButtons(string Input_ActiveDirectory, string Input_FileExtension, Rectangle Input_Bounds, ContentManager Content, Texture2D Input_Pixel, GraphicsDevice GraphicDevice)
    {
        ActiveDirectory = Input_ActiveDirectory;
        FileExtension = Input_FileExtension;
        Bounds = Input_Bounds;
        Font = Content.Load<SpriteFont>("DefaultFont");
        FontScale = 22f / Font.LineSpacing;
        Pixel = Input_Pixel;
        SavesBox = new Box(Bounds, Color.FromNonPremultiplied(0, 0, 0, 0), Content, GraphicDevice);
    }

    public void ChangeFileSkip(int Amount)
    {
        CurrentFileSkip += Amount;
        if (CurrentFileSkip > FileNames.Count)
        {
            CurrentFileSkip = FileNames.Count;
        }
        else if (CurrentFileSkip < 0)
        {
            CurrentFileSkip = 0;
        }
    }

    public List<Button> Update() //Returns the buttons that can actively be seen
    {
        FileNames = Directory.GetFiles(ActiveDirectory, "*" + FileExtension).ToList();
        ChangeFileSkip(0); //To make sure that the current file skip isn't out of bounds
        List<Button> ButtonsMade = new List<Button>();
        Color ActiveButtonColour = Color.FromNonPremultiplied(150, 150, 120, 200);
        Color ActiveColour;
        List<Color> ButtonColours = new List<Color> { Color.FromNonPremultiplied(110, 25, 0, 150), Color.FromNonPremultiplied(100, 35, 5, 150) };
        for (int Count = 0; Count < Math.Floor(Bounds.Height / (float)FileHeight) - 2; Count++) //Adding the buttons that can be seen to MenuButtons
        {
            string ActiveFile;
            if (Count < FileNames.Count) //If there aren't enough saves to display
            {
                ActiveFile = FileNames.ElementAt(Count + CurrentFileSkip);
            }
            else
            {
                ActiveFile = "null";
            }
            ActiveColour = ButtonColours.ElementAt(ButtonsMade.Count % ButtonColours.Count);
            BaseButtonDrawer ButtonDrawer = new FileSelecterDrawer(ActiveFile, Font, ActiveColour, ActiveButtonColour, Pixel, FontScale);
            Rectangle FileBounds = new Rectangle(Bounds.X + BoxSmallSize, Bounds.Y + BoxSmallSize + (FileHeight * (Count + 1)), Bounds.Width - BoxSmallSize, FileHeight);
            Button ActiveButton = new Button(ActiveFile, "", FileBounds, ButtonDrawer, null);
            ButtonsMade.Add(ActiveButton);
        }
        return ButtonsMade;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        SavesBox.Draw(ActiveSpriteBatch);
    }
}

