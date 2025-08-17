namespace Stronghold_Crusader_Project.Code.Mapping
{
    public class Borders
    {
        Texture2D BorderTexture;
        SpriteBatch ActiveSpriteBatch;

        public Borders(ContentManager Content)
        {
            BorderTexture = Content.Load<Texture2D>(BorderPath);
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
                        OffSetY = MapHeightSize;
                        DrawHorizontal(OffSetX, OffSetY);
                        break;
                    case 2: //Left
                        OffSetX = -BorderWidth + (TileWidth / 2);
                        OffSetY = -(BorderHeight - (TileHeight / 2));
                        DrawVertical(OffSetX, OffSetY);
                        break;
                    case 3: //Right
                        OffSetX = MapWidthSize + (TileWidth / 2);
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
            for (int Count = 0; Count < (MapHeightSize / BorderHeight) + 2; Count++)
            {
                Console.WriteLine(MapHeightSize / BorderHeight);
                DrawBorder(PositionX, PositionY);
                PositionY += BorderHeight;
            }
        }

        private void DrawHorizontal(int OffSetX, int OffSetY)
        {
            int PositionX = OffSetX;
            int PositionY = OffSetY;
            for (int Count = 0; Count < MapWidthSize / BorderWidth; Count++)
            {
                DrawBorder(PositionX, PositionY);
                PositionX += BorderWidth;
            }
        }
        private void DrawBorder(int PositionX, int PositionY)
        {
            Vector2 Position = new Vector2(PositionX, PositionY);
            Vector2 TileCentre = new Vector2(TileWidth / 2f, TileHeight / 2f);
            ActiveSpriteBatch.Draw(BorderTexture, Position,null, Color.White, 0f, TileCentre, 0.12f, SpriteEffects.None, 0f);
        }
    }
}
