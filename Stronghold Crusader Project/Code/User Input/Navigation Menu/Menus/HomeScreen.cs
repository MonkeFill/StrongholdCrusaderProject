using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class HomeScreen : BaseMenu
{
    Texture2D Background;
    public HomeScreen(MenuManager Input_MenuManager, ContentManager Content) : base(Input_MenuManager, Content)
    {
        Manager = Input_MenuManager;
        
        string Assets = Path.Combine(MenuFolder, "HomeScreen");
        string ButtonAssets = Path.Combine(ButtonsFolder, "Backgrounds");
        float FontScale = 1f;
        Color TextColour = Color.White;
        int ButtonOffSetX = 260;
        int ButtonOffSetY = 275;
        int ButtonHeightDifference = 75;
        
        List<string> ButtonNames = new List<string>{"Start Game", "Load Game", "Save Game"};
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        Texture2D Axe = Content.Load<Texture2D>(Path.Combine(Assets, "Selected"));
        BaseButtonDrawer TempDrawer;
        Button TempButton;
        
        for (int Count = 1; Count <= 3; Count++)
        {
            Texture2D ButtonBackground = Content.Load<Texture2D>(Path.Combine(ButtonAssets, $"Button{Count}"));
            Texture2D ButtonBackgroundHover = Content.Load<Texture2D>(Path.Combine(ButtonAssets, $"Button{Count}{HoverAdd}"));
            TempDrawer = new SelectionDrawer(ButtonBackground, ButtonBackgroundHover, Axe, Font, FontScale, TextColour, ButtonNames[Count - 1]);
            int TempY = ButtonOffSetY + (ButtonHeightDifference * (Count - 1));
            TempButton = new Button(ButtonNames[Count - 1], "", new Rectangle(ButtonOffSetX, TempY, ButtonBackground.Width, ButtonBackground.Height), TempDrawer, null);
            MenuButtons.Add(TempButton);
        }
        Texture2D ExitButton = Content.Load<Texture2D>(Path.Combine(Assets, "ExitButton"));
        TempDrawer = new IconDrawer(ExitButton, Content.Load<Texture2D>(Path.Combine(Assets, $"ExitButton{HoverAdd}")));
        TempButton = new Button("Exit", "", new Rectangle(145, 590, ExitButton.Width, ExitButton.Height), TempDrawer, null);
        MenuButtons.Add(TempButton);

    }

    public override void Draw(SpriteBatch ActiveSpriteBatch)
    {
        ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        base.Draw(ActiveSpriteBatch);
    }
}

