namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu //Menu for loading maps
{
    //Class Variables
    private List<string> FileNames;
    private Texture2D Background;
    private Box MainRectangle;
    private Box Title;
    private Box SavesBox;
    private SpriteFont TitleFont;
    private string ActiveFile;
    private List<Button> FileButtons;
    private int ActiveFileSkip;
    private int FileHeight;
    private Texture2D Pixel;

    //Class Methods
    public LoadGameMenu(MenuManager Input_MenuManager, bool InputIsSubMenu) : base(Input_MenuManager)
    {
        IsSubMenu = InputIsSubMenu;
        FileNames = Directory.GetFiles(SavesFolder).ToList();
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        string Assets = Path.Combine(MenuFolder, "LoadingMap");
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        MainRectangle = new Box(new Rectangle(175, 184, 675, 400), Color.FromNonPremultiplied(0, 0, 0, 175), Content, Menus.GraphicDevice);
        Title = new Box(new Rectangle(200, 210, 270, 65), Color.FromNonPremultiplied(150, 15, 15, 200), Content, Menus.GraphicDevice);
        TitleFont = Content.Load<SpriteFont>("DefaultFont");
        SavesBox = new Box(new Rectangle(500, 210, 310, 350), Color.FromNonPremultiplied(0, 0, 0, 0), Content, Menus.GraphicDevice);
        Pixel = new Texture2D(Menus.GraphicDevice, 1, 1);
        FileHeight = 35;
        FileButtons = new List<Button>();
        ActiveFileSkip = 0;
        SkipFile(0);
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the menu
    {
        if (IsSubMenu != true) //If the loadgame menu hasn't been set as a sub menu
        {
            ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        }
        MainRectangle.Draw(ActiveSpriteBatch);
        Title.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.DrawString(TitleFont, "Load Map", new Vector2(220, 220), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
        base.Draw(ActiveSpriteBatch);
        SavesBox.Draw(ActiveSpriteBatch);
    }
    
    private void SkipFile(int Amount)
    {

        ActiveFileSkip += Amount;
        int PreMenuButtonCount = MenuButtons.Count;
        Color ActiveButtonColour = Color.FromNonPremultiplied(150, 150, 120, 200);
        Color ActiveColour;
        List<Color> ButtonColours = new List<Color> { Color.FromNonPremultiplied(110, 25, 0, 150), Color.FromNonPremultiplied(100, 35, 5, 150) };
        for (int Count = 0; Count < Math.Floor(SavesBox.Bounds.Height / (float)FileHeight) - 1; Count++) //Adding the buttons that can be seen to MenuButtons
        {
            string ActiveFile;
            if (Count < FileNames.Count) //If there aren't enough saves to display
            {
                ActiveFile = FileNames.ElementAt(Count + ActiveFileSkip);
            }
            else
            {
                ActiveFile = "null";
            }
            ActiveColour = ButtonColours.ElementAt((MenuButtons.Count - PreMenuButtonCount) % ButtonColours.Count);
            BaseButtonDrawer ButtonDrawer = new FileSelecterDrawer(ActiveFile, TitleFont, ActiveColour, ActiveButtonColour, Pixel);
            Rectangle FileBounds = new Rectangle(SavesBox.Bounds.X + BoxSmallSize, SavesBox.Bounds.Y + BoxSmallSize + (FileHeight * Count), SavesBox.Bounds.Width - BoxSmallSize, FileHeight);
            Button ActiveButton = new Button(ActiveFile, "", FileBounds, ButtonDrawer, null);
            MenuButtons.Add(ActiveButton);
        }
        
    }
}
