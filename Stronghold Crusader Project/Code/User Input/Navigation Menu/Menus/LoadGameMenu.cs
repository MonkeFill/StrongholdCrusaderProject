namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu //Menu for loading maps
{
    //Class Variables
    private List<string> FileNames = Directory.GetFiles(SavesFolder).ToList();
    private Texture2D BackgroundTexture;
    private Box MainRectangle;
    private Box TitleBox;
    private Box SavesBox;
    private SpriteFont TitleFont;
    private string ActiveFile;
    private string Title = "Load Map";
    private int ActiveFileSkip = 0;
    private int FileHeight = 35;
    private float TitleFontScale;
    private float FileSelectorFontScale;
    private Vector2 TitlePosition;
    private Texture2D Pixel;

    //Class Methods
    public LoadGameMenu(MenuManager Input_MenuManager, bool InputIsSubMenu) : base(Input_MenuManager)
    {
        IsSubMenu = InputIsSubMenu;
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        KeybindsManager = new KeyManager("LoadGameMenu");
        string Assets = Path.Combine(MenuFolder, "LoadingMap");
        
        BackgroundTexture = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        MainRectangle = new Box(new Rectangle(175, 184, 675, 400), Color.FromNonPremultiplied(0, 0, 0, 175), Content, Menus.GraphicDevice);
        SavesBox = new Box(new Rectangle(500, 210, 310, 350), Color.FromNonPremultiplied(0, 0, 0, 0), Content, Menus.GraphicDevice);
        Pixel = new Texture2D(Menus.GraphicDevice, 1, 1);
        TitleFont = Content.Load<SpriteFont>("DefaultFont");
        TitleFontScale = 25f / TitleFont.LineSpacing;
        TitleBox = new Box(new Rectangle(200, 210, 270, 65), Color.FromNonPremultiplied(150, 15, 15, 200), Content, Menus.GraphicDevice);
        Vector2 TextPosition = TitleFont.MeasureString(Title) * TitleFontScale;
        TitlePosition = new Vector2(TitleBox.Bounds.X + ((TitleBox.Bounds.Width - TextPosition.X) / 2f), TitleBox.Bounds.Y + ((TitleBox.Bounds.Height - TextPosition.Y) / 2f));
        FileSelectorFontScale = 20f / TitleFont.LineSpacing;
        SkipFile(0);
        //All other buttons below here
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the menu
    {
        if (IsSubMenu != true) //If the loadgame menu hasn't been set as a sub menu
        {
            ActiveSpriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        }
        MainRectangle.Draw(ActiveSpriteBatch);
        TitleBox.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.DrawString(TitleFont, Title, TitlePosition, Color.White, 0f, Vector2.Zero, TitleFontScale, SpriteEffects.None, 0f);
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
            BaseButtonDrawer ButtonDrawer = new FileSelecterDrawer(ActiveFile, TitleFont, ActiveColour, ActiveButtonColour, Pixel, FileSelectorFontScale);
            Rectangle FileBounds = new Rectangle(SavesBox.Bounds.X + BoxSmallSize, SavesBox.Bounds.Y + BoxSmallSize + (FileHeight * Count), SavesBox.Bounds.Width - BoxSmallSize, FileHeight);
            Button ActiveButton = new Button(ActiveFile, "", FileBounds, ButtonDrawer, null);
            if (MenuButtons.Count == Count)
            {
                MenuButtons.Add(ActiveButton);
            }
            else
            {
                MenuButtons[Count] = ActiveButton;
            }
        }
        
    }
}
