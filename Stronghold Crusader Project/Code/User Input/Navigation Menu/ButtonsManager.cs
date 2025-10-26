namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class ButtonsManager //Manages button logic and creation
{
    //Variables
    List<Button> ButtonsCreated = new List<Button>();

    //Methods
    public void CreateButton(Button ButtonToCreate) //Initalising button class
    {
        if (GetButtonFromName(ButtonToCreate.Name) == null)
        {
            ButtonsCreated.Add(ButtonToCreate);
            LogEvent($"Button  {ButtonToCreate.Name} created", LogType.Info);
        }
        else
        {
            LogEvent($"Button name {ButtonToCreate.Name} already exists", LogType.Error);
        }
    }

    public void UpdateButtons(MouseState ActiveMouse) //Updating button logic
    {
        foreach (Button ActiveButton in ButtonsCreated) //Looping through all buttons
        {
            ActiveButton.Hover = false;
            if (ActiveButton.Update(ActiveMouse)) //If it has a change
            {
                string TempCategory = ActiveButton.Category;
                if (TempCategory != null)
                {
                    foreach (Button SecondActiveButton in ButtonsCreated) //Looping through all buttons in the same category
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

    public void DrawButtons(SpriteBatch ActiveSpriteBatch) //Drawing every button
    {
        foreach (Button ActiveButton in ButtonsCreated)
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

    public void ResetButton(string ButtonName) //Resetting a button
    {
        Button ActiveButton = GetButtonFromName(ButtonName);
        if (ActiveButton != null)
        {
            ActiveButton.ResetButton();
        }
        else
        {
            LogEvent($"Can't find button {ButtonName}", LogType.Warning);
        }
    }

    public void HideButtonsInCategory(string CategoryName) //Hidding all buttons in a category
    {
        foreach (Button ActiveButton in ButtonsCreated)
        {
            if (ActiveButton.Category == CategoryName)
            {
                ActiveButton.Visible = false;
            }
        }
    }

    public Button GetButtonFromName(string ButtonName) //Getting the button from the list
    {
        foreach (Button ActiveButton in ButtonsCreated)
        {
            if (ActiveButton.Name == ButtonName)
            {
                return ActiveButton;
            }
        }
        return null;
    }
}
