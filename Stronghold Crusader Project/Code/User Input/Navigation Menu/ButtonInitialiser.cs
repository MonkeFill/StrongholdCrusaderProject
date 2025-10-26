using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NavigationMenu
{
    public static class ButtonInitialiser
    {
        public static void Initalise(ButtonsManager ButtonManager, ContentManager Content, Texture2D Pixel, SpriteFont ActiveSpriteFont)
        {
            //Testing buttons for icon buttons
            //ButtonManager.CreateButton(new IconButton("TestButton", new Vector2(10, 10), 50, 50, Content.Load<Texture2D>("IconTest"), Input_Visible: true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveIcon: Content.Load<Texture2D>("ActiveTest"), Input_HoverIcon: Content.Load<Texture2D>("HoverTest"), Input_Category: "TestCategory"));
            ButtonManager.CreateButton(new IconButton("TestButton2", new Vector2(75, 10), 50, 50, Content.Load<Texture2D>("IconTest"), Input_Visible: true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveIcon: Content.Load<Texture2D>("ActiveTest"), Input_HoverIcon: Content.Load<Texture2D>("HoverTest"), Input_Category: "TestCategory"));
            //ButtonManager.CreateButton(new IconButton("TestButton3", new Vector2(10, 75), 50, 50, Content.Load<Texture2D>("IconTest"), Input_Visible: true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveIcon: Content.Load<Texture2D>("ActiveTest"), Input_HoverIcon: Content.Load<Texture2D>("HoverTest")));
            //ButtonManager.CreateButton(new IconButton("TestButton4", new Vector2(10, 150), 50, 50, Content.Load<Texture2D>("IconTest"), Input_Visible: true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveIcon: Content.Load<Texture2D>("ActiveTest"), Input_HoverIcon: Content.Load<Texture2D>("HoverTest"), Input_Category: "TestCategory2"));
            //ButtonManager.CreateButton(new IconButton("TestButton5", new Vector2(75, 150), 50, 50, Content.Load<Texture2D>("IconTest"), Input_Visible: true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveIcon: Content.Load<Texture2D>("ActiveTest"), Input_HoverIcon: Content.Load<Texture2D>("HoverTest"), Input_Category: "TestCategory2"));
            
            
            ButtonManager.CreateButton(new TitleButton("TestButton 1", new Vector2(10, 10), 50, 50, "Button 1", 30, Color.White, ActiveSpriteFont, true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveColour: Color.Red, Input_Category: "TestCategory", Input_HoverColour: Color.Blue));
            //ButtonManager.CreateButton(new TitleButton("TestButton 2", new Vector2(75, 10), 50, 50, "Button 2", 30, Color.White, ActiveSpriteFont, true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveColour: Color.Red, Input_Category: "Test1", Input_HoverColour: Color.Blue));
            //ButtonManager.CreateButton(new TitleButton("TestButton 3", new Vector2(10, 75), 50, 50, "Button 3", 30, Color.White, ActiveSpriteFont, true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveColour: Color.Red, Input_Category: "Test2", Input_HoverColour: Color.Blue));
            //ButtonManager.CreateButton(new TitleButton("TestButton 4", new Vector2(10, 150), 50, 50, "Button 4", 30, Color.White, ActiveSpriteFont, true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveColour: Color.Red, Input_Category: "Test3", Input_HoverColour: Color.Blue));
            //ButtonManager.CreateButton(new TitleButton("TestButton 5", new Vector2(75, 150), 50, 50, "Button 5", 30, Color.White, ActiveSpriteFont, true, Input_Stroke: 5, Input_StrokeColour: Color.GreenYellow, Input_StrokePixel: Pixel, Input_Background: Content.Load<Texture2D>("BackgroundTest"), Input_ActiveColour: Color.Red, Input_Category: "Test3", Input_HoverColour: Color.Blue));

        }

    }
}
