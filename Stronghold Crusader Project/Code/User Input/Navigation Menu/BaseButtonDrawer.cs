namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public interface BaseButtonDrawer //Using interface as it must have a draw method
{
    void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton);
}

