using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace NavigationMenu
{
    public class TitleButton : Button
    {
        private string Text;
        private int TextSize;
        private Color TextColour;
        private SpriteFont TextFont;
        private Color HoverColour;
        private Color ActiveColour;
        private float TextSizeScale;
        private int BaseFontSize = 24;

        public TitleButton( //Creating the button with optional parameters for details on the button
            string Input_Name,
            Vector2 Input_Position,
            int Input_Height,
            int Input_Width,
            string Input_Text,
            int Input_TextSize,
            Color Input_TextColour,
            SpriteFont Input_TextFont,
            bool Input_Visible = false,
            string Input_Category = null,
            Texture2D Input_Background = null,
            int Input_Stroke = 0,
            Color? Input_StrokeColour = null,
            Texture2D Input_StrokePixel = null,
            Color? Input_HoverColour = null,
            Color? Input_ActiveColour = null
            ) : base(Input_Name, Input_Position, Input_Height, Input_Width, Input_Visible, Input_Category, Input_Background, Input_Stroke, Input_StrokeColour, Input_StrokePixel) //Adding variables to button
        {
            //Adding variables to the additonal parameters
            Text = Input_Text;
            TextSize = Input_TextSize;
            TextColour = Input_TextColour;
            TextFont = Input_TextFont;
            HoverColour = TextColour;
            ActiveColour = TextColour;
            if (Input_HoverColour != null)
            {
                HoverColour = (Color)Input_HoverColour;
            }

            if (Input_ActiveColour != null)
            {
                ActiveColour = (Color)Input_ActiveColour;
            }
            TextSizeScale = (float)TextSize / BaseFontSize;
        }

        public override void Draw(SpriteBatch ActiveSpriteBatch)
        {
            if (Visible == true)//Only draw if the button is visible
            {
                base.Draw(ActiveSpriteBatch);
                Color ActiveTextColor = TextColour;
                if (Hover == true)
                {
                    ActiveTextColor = HoverColour;
                }
                if (Active == true)
                {
                    ActiveTextColor = ActiveColour;
                }
                float MiddleX = Position.X + (Width / 2);
                float MiddleY = Position.Y + (Height / 2);
                float TextX = TextFont.MeasureString(Text).X * TextSizeScale;
                float TextY = TextFont.MeasureString(Text).Y * TextSizeScale;
                Vector2 PositionToDraw = new Vector2(MiddleX - (TextX / 2), MiddleY - (TextY / 2));
                ActiveSpriteBatch.DrawString(TextFont, Text, PositionToDraw, ActiveTextColor, 0f, Vector2.Zero, TextSizeScale, SpriteEffects.None, 0f);
            }
        }
    }
}
