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
    public class Button
    {
        //Class Variables
        public string Name;
        protected Vector2 Position;
        protected int Height;
        protected int Width;
        public bool Visible;
        public string Category;
        private Texture2D BackgroundTexture;
        private int Stroke;
        private Color StrokeColour;
        private Texture2D StrokePixel;

        public bool Active;
        public bool Hover;

        //Methods
        public Button(
            string Input_Name, 
            Vector2 Input_Position,
            int Input_Height,
            int Input_Width,
            bool Input_Visible = false,
            string Input_Category = null,
            Texture2D Input_Background = null,
            int Input_Stroke = 0,
            Color? Input_StrokeColour = null,
            Texture2D Input_StrokePixel = null
            ) //Creating the button with optional parameters for details on the button
        {
            Name = Input_Name;
            Position = Input_Position;
            Height = Input_Height;
            Width = Input_Width;
            Visible = Input_Visible;
            Category = Input_Category;
            BackgroundTexture = Input_Background;
            Stroke = Input_Stroke;
            StrokePixel = Input_StrokePixel;
            if (Input_StrokeColour == null)
            {
                Input_StrokeColour = Color.Black; //Default stroke colour
            }
            else
            {
                StrokeColour = (Color)Input_StrokeColour;
                StrokePixel.SetData(new[] { StrokeColour }); //Colouring the pixel
            }

        }

        public virtual void Draw(SpriteBatch ActiveSpriteBatch)
        {
            if (BackgroundTexture != null) //If there is a background, draw it
            {
                int BackgroundStartX = (BackgroundTexture.Width / 2) - Width; //Getting the start points of the texture to make the button only show the middle bit if its too big
                int BackgroundStartY = (BackgroundTexture.Height / 2) - Height;
                Rectangle BackgroundBound = new Rectangle(BackgroundStartX, BackgroundStartY, Width, Height);
                ActiveSpriteBatch.Draw(BackgroundTexture, Position, BackgroundBound, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            if (Stroke != 0) //If there is a stroke, add it
            {
                Rectangle StrokeRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height); //Default rectangle outline
                                                                                                            //Drawing four separate boxes for the stroke sides
                ActiveSpriteBatch.Draw(StrokePixel, new Rectangle(StrokeRectangle.X - Stroke, StrokeRectangle.Y - Stroke, StrokeRectangle.Width + (Stroke * 2), Stroke), StrokeColour); //Top
                ActiveSpriteBatch.Draw(StrokePixel, new Rectangle(StrokeRectangle.X - Stroke, StrokeRectangle.Y + StrokeRectangle.Height, StrokeRectangle.Width + (Stroke * 2), Stroke), StrokeColour); //Bottom
                ActiveSpriteBatch.Draw(StrokePixel, new Rectangle(StrokeRectangle.X - Stroke, StrokeRectangle.Y - Stroke, Stroke, StrokeRectangle.Height + (Stroke * 2)), StrokeColour); //Left
                ActiveSpriteBatch.Draw(StrokePixel, new Rectangle(StrokeRectangle.X + StrokeRectangle.Width, StrokeRectangle.Y - Stroke, Stroke, StrokeRectangle.Height + (Stroke * 2)), StrokeColour); //Right
            }

        }

        public bool Update(MouseState ActiveMouse) //Will return true if active becomes true
        {
            if (Visible == true) //Button is visible
            {
                if (Active == false) //Not an active button
                {
                    if (ActiveMouse.X >= Position.X && ActiveMouse.X <= Position.X + Width) //inside of the X bounds of the button
                    {
                        if (ActiveMouse.Y >= Position.Y && ActiveMouse.Y <= Position.Y + Height) //inside of the Y bounds of the button
                        {
                            if (ActiveMouse.LeftButton == ButtonState.Pressed) //If the button is pressed
                            {
                                Active = true;
                                return true;
                            }
                            Hover = true;
                        }
                    }
                    return false;
                }
                return true;
            }
            return false;
        }
        public void ResetButton()
        {
            Active = false;
            Hover = false;
            Visible = true;
        }
    }
}
