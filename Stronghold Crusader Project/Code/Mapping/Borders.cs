using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Mapping
{
    public class Borders
    {
        //Class Variables
        Texture2D ActiveTexture;
        SpriteBatch ActiveSpriteBatch;
        Dictionary<string, Texture2D> BorderTextures = new  Dictionary<string, Texture2D>();
    
        //Methods
        public Borders(ContentManager Content)
        {
            LoadBorderTextures(Content);
        }
        public void Draw(SpriteBatch ActiveSpriteBatchInput)
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

        private void DrawVertical(int OffSetX, int OffSetY)
        {
            int PositionX = OffSetX;
            int PositionY = OffSetY;
            for (int Count = 0; Count < (RealMapHeight / BorderHeight) + 2; Count++)
            {
                if (Count == (RealMapHeight / BorderHeight) / 2) //Second first and second last border must be a small top texture
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

        private void DrawHorizontal(int OffSetX, int OffSetY)
        {
            int PositionX = OffSetX;
            int PositionY = OffSetY;
            
            for (int Count = 0; Count < (RealMapWidth / BorderWidth); Count++)
            {
                if (Count == (RealMapWidth / BorderWidth) / 2) //First and last border must be a small side texture
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
        private void DrawBorder(int PositionX, int PositionY)
        {
            Vector2 Position = new Vector2(PositionX, PositionY);
            Vector2 TileCentre = new Vector2(TileWidth / 2f, TileHeight / 2f);
            ActiveSpriteBatch.Draw(ActiveTexture, Position,null, Color.White, CameraRotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        
        private void LoadBorderTextures(ContentManager Content)
        {
            LogEvent("Loading border textures", LogType.Info);
            BorderTextures.Add(DefaultBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, DefaultBorderTexture)));
            BorderTextures.Add(TopSmallBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, TopSmallBorderTexture)));
            BorderTextures.Add(SideSmallBorderTexture, Content.Load<Texture2D>(Path.Combine(BorderPath, SideSmallBorderTexture)));
        }
    }
}
