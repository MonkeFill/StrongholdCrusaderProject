namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;

public class FileSelectionButtons
{
    //Class Variables
    private string ActiveDirectory;
    private string FileExtension;
    private List<string> FileNames;
    private int CurrentPage = 1;
    private int ActivePages;
    private int FileHeight = 35;
    private int NavigationHeight = 25;
    private int FilesPerPage;
    private int StartPoint = 0;
    private float FontScale;
    private Rectangle Bounds;
    private SpriteFont Font;
    private Texture2D Pixel;
    public Box SavesBox;
    private BaseFileMenu Menu;

    //Class Methods
    public FileSelectionButtons(string Input_ActiveDirectory, string Input_FileExtension, Rectangle Input_Bounds, BaseFileMenu Input_Menu, Texture2D Input_Pixel)
    {
        Menu = Input_Menu;
        ContentManager Content = Menu.Menus.Content;
        GraphicsDevice GraphicDevice = Menu.Menus.GraphicDevice;
        List<Button> MenuButtons = Menu.MenuButtons;
        ActiveDirectory = Input_ActiveDirectory;
        FileExtension = Input_FileExtension;
        Bounds = Input_Bounds;
        Font = Content.Load<SpriteFont>("DefaultFont");
        FontScale = 22f / Font.LineSpacing;
        Pixel = Input_Pixel;
        FilesPerPage = (int)Math.Floor(Bounds.Height / (float)FileHeight) - 3;
        SavesBox = new Box(Bounds, Color.FromNonPremultiplied(0, 0, 0, 0), Content, GraphicDevice);
        MenuButtons.Add(GetGlobalNavigationButton(true, Content, new Vector2(SavesBox.Bounds.X + NavigationHeight, SavesBox.Bounds.Y + SavesBox.Bounds.Height - NavigationHeight), () => ChangePage(-1), "Previous", 0.5f));
        MenuButtons.Add(GetGlobalNavigationButton(false, Content, new Vector2(SavesBox.Bounds.X + SavesBox.Bounds.Width - NavigationHeight, SavesBox.Bounds.Y + SavesBox.Bounds.Height - NavigationHeight), () => ChangePage(1), "Next", 0.5f)); 
    }

    public void ChangePage(int Amount)
    {
        CurrentPage += Amount;
        if (CurrentPage > ActivePages)
        {
            CurrentPage = ActivePages;
        }
        else if (CurrentPage < 1)
        {
            CurrentPage = 1;
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        SavesBox.Draw(ActiveSpriteBatch);
        string PageText = $"{CurrentPage}/{ActivePages}";
        Vector2 PageTextPosition = new Vector2(SavesBox.Bounds.X + ((SavesBox.Bounds.Width - (Font.MeasureString(PageText).X) * FontScale)/ 2f), SavesBox.Bounds.Y + SavesBox.Bounds.Height - (NavigationHeight * 1.75f));
        ActiveSpriteBatch.DrawString(Font, PageText, PageTextPosition, Color.White, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }
    
    public List<Button> ReplaceButtons(List<Button> MenuButtons)
    {
        List<Button> NewButtons = Update();
        if (StartPoint == 0)
        {
            StartPoint = MenuButtons.Count;
        }
        for(int Count = 0; Count < NewButtons.Count; Count++)
        {
            int ActivePosition = StartPoint + Count;
            if(MenuButtons.Count <= ActivePosition)
            {
                MenuButtons.Add(NewButtons[Count]);
            }
            else
            {
                if (MenuButtons[ActivePosition].Name != NewButtons[Count].Name) //Only replace the button if they are different
                {
                    MenuButtons[ActivePosition] = NewButtons[Count];
                }
            }
        }
        return MenuButtons;
    }
    
    private List<Button> Update() //Returns the buttons that can actively be seen
    {
        FileNames = Directory.GetFiles(ActiveDirectory, "*" + FileExtension).ToList();
        FileNames.Sort();
        ActivePages = (int)Math.Ceiling((float)FileNames.Count / FilesPerPage);
        if(ActivePages < 1) //Can't be zero but will be when no files are found
        {
            ActivePages = 1;
        }
        List<Button> ButtonsMade = new List<Button>();
        Color ActiveButtonColour = Color.FromNonPremultiplied(180, 150, 120, 200);
        Color HoverButtonColour = Color.FromNonPremultiplied(150, 150, 120, 200);
        Color ActiveColour;
        List<Color> ButtonColours = new List<Color> { Color.FromNonPremultiplied(110, 25, 0, 150), Color.FromNonPremultiplied(100, 35, 5, 150) };
        
        //Adding title button first
        ActiveColour = ButtonColours.ElementAt(ButtonsMade.Count % ButtonColours.Count);
        BaseButtonDrawer ButtonDrawer = new FileSelecterDrawer("title",Font, ActiveColour, HoverButtonColour, ActiveButtonColour, Pixel, FontScale);
        Rectangle FileBounds = new Rectangle(Bounds.X + BoxSmallSize, Bounds.Y + BoxSmallSize, Bounds.Width - BoxSmallSize, FileHeight);
        Button ActiveButton = new Button("title","", FileBounds, ButtonDrawer, null);
        ButtonsMade.Add(ActiveButton);
        
        for (int Count = 0; Count < FilesPerPage; Count++) //Adding the buttons that can be seen to MenuButtons
        {
            string ActiveFile;
            Action FileAction;
            int ActivePosition = Count + ((CurrentPage - 1) * FilesPerPage);
            if (ActivePosition < FileNames.Count) //If there aren't enough saves to display
            {
                ActiveFile = FileNames.ElementAt(ActivePosition);
                FileAction = () => Menu.ActiveFile = ActiveFile;
            }
            else
            {
                ActiveFile = "null";
                FileAction = () => { };
            }
            ActiveColour = ButtonColours.ElementAt(ButtonsMade.Count % ButtonColours.Count);
            ButtonDrawer = new FileSelecterDrawer(ActiveFile, Font, ActiveColour, HoverButtonColour, ActiveButtonColour, Pixel, FontScale);
            FileBounds = new Rectangle(Bounds.X + BoxSmallSize, Bounds.Y + BoxSmallSize + (FileHeight * (Count + 1)), Bounds.Width - BoxSmallSize, FileHeight);
            ActiveButton = new Button(ActiveFile, "", FileBounds, ButtonDrawer, FileAction);
            ButtonsMade.Add(ActiveButton);
        }
        return ButtonsMade;
    }
}

