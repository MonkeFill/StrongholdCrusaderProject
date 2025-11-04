namespace Stronghold_Crusader_Project.Code.User_Input.User_Interface;

public abstract class BaseUI
{
    //Class Variables
    protected List<Button> UIButtons;
    protected bool Visible;
    
    public BaseUI()
    {
        Visible = false;
    }
    
    public void Update(MouseState ActiveMouse) //Updating the UI logic
    {
        if (Visible == true) //You can see the UI
        {
            foreach (Button ActiveButton in UIButtons) //Looping through all buttons to update them
            {
                ActiveButton.Hover = false;
                if (ActiveButton.Update(ActiveMouse)) //If it has a change
                {
                    string TempCategory = ActiveButton.Category;
                    if (TempCategory != null)
                    {
                        foreach (Button SecondActiveButton in UIButtons) //Looping through all buttons in the same category
                        {
                            if (SecondActiveButton != ActiveButton)
                            {
                                if (SecondActiveButton.Category == TempCategory)
                                {
                                    SecondActiveButton.Active = false; //Making the button unactive
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    public virtual void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the UI
    {
        foreach (Button ActiveButton in UIButtons) //Draw every button
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

    public void ResetUI() //Setting the UI to its default
    {
        foreach (Button ActiveButton in UIButtons)
        {
            ActiveButton.Visible = true;
            ActiveButton.Hover = false;
            ActiveButton.Active = false;
        }
    }
}

