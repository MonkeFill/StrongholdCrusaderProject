using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;
using System;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu //Menu for loading maps
{
    //Class Variables
    FileSelectionButtons FileButtonsManager;
    private Texture2D BackgroundTexture;
    private Texture2D Pixel;
    private Box MainRectangle;
    private Box TitleBox;
    private SpriteFont TitleFont;
    private string ActiveFile;
    private string Title = "Load Map";
    private int ActiveFileSkip = 0;
    private float TitleFontScale;
    private Vector2 TitlePosition;

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
        Pixel = new Texture2D(Menus.GraphicDevice, 1, 1);
        TitleFont = Content.Load<SpriteFont>("DefaultFont");
        TitleFontScale = 35f / TitleFont.LineSpacing;
        TitleBox = new Box(new Rectangle(200, 210, 270, 65), Color.FromNonPremultiplied(150, 15, 15, 200), Content, Menus.GraphicDevice);
        Vector2 TextPosition = TitleFont.MeasureString(Title) * TitleFontScale;
        TitlePosition = new Vector2(TitleBox.Bounds.X + ((TitleBox.Bounds.Width - TextPosition.X) / 2f), TitleBox.Bounds.Y + ((TitleBox.Bounds.Height - TextPosition.Y) / 2f));
        FileButtonsManager = new FileSelectionButtons(SavesFolder, "", new Rectangle(500, 210, 310, 350), Content, Pixel, Menus.GraphicDevice);
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the menu
    {
        ReplaceButtons();
        if (IsSubMenu != true) //If the loadgame menu hasn't been set as a sub menu
        {
            ActiveSpriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        }
        MainRectangle.Draw(ActiveSpriteBatch);
        TitleBox.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.DrawString(TitleFont, Title, TitlePosition, Color.White, 0f, Vector2.Zero, TitleFontScale, SpriteEffects.None, 0f);
        base.Draw(ActiveSpriteBatch);
        FileButtonsManager.Draw(ActiveSpriteBatch);
    }

    private void ReplaceButtons()
    {
        List<Button> NewButtons = FileButtonsManager.Update();
        for(int Count = 0; Count < NewButtons.Count; Count++)
        {
            int ActivePosition = MenuButtons.Count + Count;
            if(MenuButtons.Count <= ActivePosition)
            {
                MenuButtons.Add(NewButtons[Count]);
            }
            else
            {
                MenuButtons[ActivePosition] = NewButtons[Count];
            }
        }
    }
}
