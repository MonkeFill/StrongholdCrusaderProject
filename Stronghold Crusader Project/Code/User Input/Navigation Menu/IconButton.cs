using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NavigationMenu
{
    public class IconButton : Button
    {
        private Texture2D Icon;
        private Texture2D HoverIcon;
        private Texture2D ActiveIcon;

        public IconButton( //Creating the button with optional parameters for details on the button
            string Input_Name,
            Vector2 Input_Position,
            int Input_Height,
            int Input_Width,
            Texture2D Input_Icon,
            bool Input_Visible = false,
            string Input_Category = null,
            Texture2D Input_Background = null,
            int Input_Stroke = 0,
            Color? Input_StrokeColour = null,
            Texture2D Input_StrokePixel = null,
            Texture2D Input_HoverIcon = null,
            Texture2D Input_ActiveIcon = null
            ) : base(Input_Name,Input_Position,Input_Height,Input_Width, Input_Visible, Input_Category, Input_Background, Input_Stroke ,Input_StrokeColour, Input_StrokePixel) //Adding variables to button
            {
            //Adding variables to the additonal parameters
                Icon = Input_Icon;
                HoverIcon = Input_HoverIcon;
                ActiveIcon = Input_ActiveIcon;
            }

        public override void Draw(SpriteBatch ActiveSpriteBatch)
        {
            if (Visible == true) //Only draw if the button is visible
            {
                base.Draw(ActiveSpriteBatch);
                Texture2D TextureForIcon = Icon;
                if (Hover == true)
                {
                    TextureForIcon = HoverIcon;
                }
                if (Active == true)
                {
                    TextureForIcon = ActiveIcon;
                }
                float MiddleX = Position.X + (Width / 2);
                float MiddleY = Position.Y + (Height / 2);
                Vector2 PositionToDraw = new Vector2(MiddleX - (TextureForIcon.Width / 2), MiddleY - (TextureForIcon.Height / 2));
                ActiveSpriteBatch.Draw(TextureForIcon, PositionToDraw, Color.White);
            }
        }
    }
}
