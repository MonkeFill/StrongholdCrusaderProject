using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Mapping
{
    public class Borders //Class to handle drawing borders around the map
    {
        //Class Variables
        Texture2D ActiveTexture;
        SpriteBatch ActiveSpriteBatch;
        Dictionary<string, Texture2D> BorderTextures = new  Dictionary<string, Texture2D>();
    
        //Methods
        public Borders(ContentManager Content) //Setting up borders class
        {
            LoadBorderTextures(Content); //Loading all the border textures
        }
        public void Draw(SpriteBatch ActiveSpriteBatchInput) //Method to draw all borders
        {
            ActiveSpriteBatch = ActiveSpriteBatchInput;
            int OffSetX = 0;
            int OffSetY = 0;

            for (int Count = 0; Count < 4; Count++)
            {
                switch (Count) //0. Top, 1. Bottom, 2. Left, 3. Right
                {
                    case 0: //Top
                        OffSetX = TileWidth / 2;
                        OffSetY = -(BorderHeight - (TileHeight / 2));
                        DrawHorizontal(OffSetX, OffSetY);
                        break;
                    case 1: //Bottom
                        OffSetX = TileWidth / 2;
                        OffSetY = MapHeightSize - (TileHeight / 2);
                        DrawHorizontal(OffSetX, OffSetY);
                        break;
                    case 2: //Left
                        OffSetX = -BorderWidth + (TileWidth / 2);
                        OffSetY = -(BorderHeight - (TileHeight / 2));
                        DrawVertical(OffSetX, OffSetY);
                        break;
                    case 3: //Right
                        OffSetX = MapWidthSize - (TileWidth / 2);
                        OffSetY = -(BorderHeight - (TileHeight / 2)); 
                        DrawVertical(OffSetX, OffSetY);
                        break;
                }
            }
        }

        private void DrawVertical(int OffSetX, int OffSetY) //Method to draw a vertical row of borders
        {
            int PositionX = OffSetX;
            int PositionY = OffSetY;
            
            int AmountOfBorders = (RealMapHeight / BorderHeight) + 2;
            for (int Count = 0; Count < AmountOfBorders; Count++)
            {
                if (Count == AmountOfBorders / 2) //Drawing a smaller border in the middle
                {
                    ActiveTexture = BorderTextures[TopSmallBorderTexture];
                    OffSetY = -(TileHeight / 2);
                }
                else
                {
                    ActiveTexture = BorderTextures[DefaultBorderTexture];
                    OffSetY = 0;
                }
                DrawBorder(PositionX, PositionY);
                PositionY += BorderHeight + OffSetY;
            }
        }

        private void DrawHorizontal(int OffSetX, int OffSetY) //Method to draw a horizontal column of borders
        {
            int PositionX = OffSetX;
            int PositionY = OffSetY;
            int AmountOfBorders = (RealMapWidth / BorderWidth);
            
            for (int Count = 0; Count < AmountOfBorders; Count++)
            {
                if (Count == AmountOfBorders / 2) //Drawing a smaller border in the middle
                {
                    ActiveTexture = BorderTextures[SideSmallBorderTexture];
                    OffSetX = -(TileWidth / 2);
                }
                else
                {
                    ActiveTexture = BorderTextures[DefaultBorderTexture];
                    OffSetX = 0;
                }
                DrawBorder(PositionX, PositionY);
                PositionX += BorderWidth + OffSetX;
            }
        }
        private void DrawBorder(int PositionX, int PositionY) //Method to draw a single border
        {
            Vector2 Position = new Vector2(PositionX, PositionY);
            Vector2 TileCentre = new Vector2(TileWidth / 2f, TileHeight / 2f);
            ActiveSpriteBatch.Draw(ActiveTexture, Position,null, Color.White, CameraRotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        
        private void LoadBorderTextures(ContentManager Content) //Adding the 3 possible textures into the texturemap
        {
            LogEvent("Loading border textures", LogType.Info);
            BorderTextures.Add(DefaultBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, DefaultBorderTexture)));
            BorderTextures.Add(TopSmallBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, TopSmallBorderTexture)));
            BorderTextures.Add(SideSmallBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, SideSmallBorderTexture)));
        }
    }
}
