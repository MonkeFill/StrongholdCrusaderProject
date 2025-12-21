namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public abstract class BaseFileMenu : BaseMenu
{
    //Class Variables
    private FileSelectionButtons FileButtonsManager;
    private Texture2D BackgroundTexture;
    private Texture2D Pixel;
    private Box MainRectangle;
    private Box TitleBox;
    private SpriteFont TitleFont;
    public string ActiveFileName;
    private string Title;
    private float TitleFontScale;
    private Vector2 TitlePosition;
    private Rectangle MiniMap;

    public BaseFileMenu(MenuManager Input_MenuManager, bool InputIsSubMenu, string Assets, string Input_Title) : base(Input_MenuManager)
    {
        IsSubMenu = InputIsSubMenu;
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        KeybindsManager = new KeyManager(Input_Title);

        Title = Input_Title;
        BackgroundTexture = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        MainRectangle = new Box(new Rectangle(175, 184, 675, 400), Color.FromNonPremultiplied(0, 0, 0, 175), Content, Menus.GraphicDevice);
        Pixel = new Texture2D(Menus.GraphicDevice, 1, 1);
        TitleFont = Content.Load<SpriteFont>("DefaultFont");
        TitleFontScale = 35f / TitleFont.LineSpacing;
        TitleBox = new Box(new Rectangle(200, 210, 270, 65), Color.FromNonPremultiplied(150, 15, 15, 200), Content, Menus.GraphicDevice);
        int TitleBoxMiddleX = TitleBox.Bounds.X + (TitleBox.Bounds.Width / 2);
        Vector2 TextPosition = TitleFont.MeasureString(Title) * TitleFontScale;
        TitlePosition = new Vector2(TitleBoxMiddleX - (TextPosition.X / 2f), TitleBox.Bounds.Y + ((TitleBox.Bounds.Height - TextPosition.Y) / 2f));
        FileButtonsManager = new FileSelectionButtons(SavesFolder, "", new Rectangle(500, 210, 310, 350), this, Pixel);
        int MiniMapSize = (int)(TitleBox.Bounds.Width * 0.75f);
        MiniMap = new Rectangle(TitleBoxMiddleX - (MiniMapSize / 2), TitleBox.Bounds.Y + TitleBox.Bounds.Height + 20, MiniMapSize, MiniMapSize);
        //Adding the load/save game button
        Box TempBox = FileButtonsManager.SavesBox;
        Vector2 ExtraButtonPosition = new Vector2(TitleBoxMiddleX, TempBox.Bounds.Y + TempBox.Bounds.Height);
        string ButtonText = "Save Game";
        Action ButtonAction = () => { };
        if (Title.Contains("Load"))
        {
            ButtonText = "Load Game";
            ButtonAction = () => MapImportHandler(ActiveFileName);
        }
        BasicTextButton NewTextButton = new BasicTextButton();
        MenuButtons.Add(NewTextButton.GetButton(Content, ExtraButtonPosition, ButtonAction, ButtonText, 1f, Color.White));
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the menu
    {
        FileButtonsManager.ReplaceButtons(MenuButtons);
        if (IsSubMenu != true) //If the loadgame menu hasn't been set as a sub menu
        {
            ActiveSpriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        }
        MainRectangle.Draw(ActiveSpriteBatch);
        TitleBox.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.DrawString(TitleFont, Title, TitlePosition, Color.White, 0f, Vector2.Zero, TitleFontScale, SpriteEffects.None, 0f);
        base.Draw(ActiveSpriteBatch);
        FileButtonsManager.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.Draw(Pixel, new Rectangle(MiniMap.X - 1, MiniMap.Y - 1, MiniMap.Width + 2, MiniMap.Height + 2), Color.White);
        DrawMiniMap(ActiveSpriteBatch, MiniMap, Pixel);
    }
}

