using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMenu
{
    public class ButtonsManager
    {
        List<Button> ButtonsCreated = new List<Button>();

        public void CreateButton(Button ButtonToCreate)
        {
            if (GetButtonFromName(ButtonToCreate.Name) == null)
            {
                ButtonsCreated.Add(ButtonToCreate);
                //LogEvent($"Button  {ButtonToCreate.Name} created", ErrorType.Info);
            }
            else
            {
                //LogEvent($"Button name {ButtonToCreate.Name} already exists", ErrorType.Error);
            }
        }

        public void UpdateButtons(MouseState ActiveMouse)
        {
            foreach (Button ActiveButton in ButtonsCreated)
            {
                ActiveButton.Hover = false;
                if (ActiveButton.Update(ActiveMouse))
                {
                    string TempCategory = ActiveButton.Category;
                    if (TempCategory != null)
                    {
                        foreach (Button SecondActiveButton in ButtonsCreated)
                        {
                            if (SecondActiveButton != ActiveButton)
                            {
                                if (SecondActiveButton.Category == TempCategory)
                                {
                                    SecondActiveButton.Active = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DrawButtons(SpriteBatch ActiveSpriteBatch)
        {
            foreach (Button ActiveButton in ButtonsCreated)
            {
                ActiveButton.Draw(ActiveSpriteBatch);
            }
        }

        public void ResetButton(string ButtonName)
        {
            Button ActiveButton = GetButtonFromName(ButtonName);
            if (ActiveButton != null)
            {
                ActiveButton.ResetButton();
            }
            else
            {
                //LogEvent($"Can't find button {ButtonName}", ErrorType.Warning);
            }
        }
        
        public void HideButtonsInCategory(string CategoryName)
        {
            foreach (Button ActiveButton in ButtonsCreated)
            {
                if (ActiveButton.Category == CategoryName)
                {
                    ActiveButton.Visible = false;
                }
            }
        }

        public Button GetButtonFromName(string ButtonName)
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
}
