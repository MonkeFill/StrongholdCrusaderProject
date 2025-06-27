using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AStar_PathFinding;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    char[,] Grid = new char[,] { };
    char[,] OriginalGrid = new char[,]
    {
        {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ', ' ', ' ',' ', ' '},
        {' ',' ',' ','W',' ',' ',' ',' ','W',' ',' ', ' ', ' ','W', ' '},
        {' ',' ',' ','W',' ',' ',' ',' ','W',' ',' ', 'W', ' ',' ', ' '},
        {' ',' ',' ','W','W',' ',' ',' ','W',' ',' ', 'W', ' ',' ', ' '},
        {'S',' ',' ',' ',' ','W',' ',' ','W',' ',' ', ' ', ' ','W', ' '},
        {' ',' ',' ',' ',' ','W',' ',' ',' ',' ',' ', ' ', ' ','W', ' '},
        {' ',' ',' ',' ',' ','W',' ','W',' ',' ',' ', ' ', ' ','W', ' '},
        {' ',' ',' ',' ',' ','W',' ','W',' ',' ','W', ' ', ' ','W', ' '},
        {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','W', ' ', ' ',' ', ' '},
        {' ',' ',' ',' ',' ',' ',' ',' ',' ','W',' ', ' ', ' ',' ', ' '},
    };

    int SquareSize = 45;
    Texture2D Pixel;
    Color ActiveColor;
    int OutlineThickness = 1;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 30.0);
        IsFixedTimeStep = true;
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Mouse.SetPosition(50, 50);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        Grid = OriginalGrid.Clone() as char[,];
        List<Coordinate> CurrentPath = new List<Coordinate>();
        Grid = OriginalGrid.Clone() as char[,];

        MouseState MouseCursor  = Mouse.GetState();
        int MouseGridX = MouseCursor.X / SquareSize;
        int MouseGridY = MouseCursor.Y / SquareSize;

// Check bounds
        if (MouseGridX >= 0 && MouseGridX < 15 && MouseGridY >= 0 && MouseGridY < 15)
        {
            if (Grid[MouseGridY, MouseGridX] == ' ')
            {
                Grid[MouseGridY, MouseGridX] = 'E';
                CurrentPath = PathFind.FindPath(Grid, new Coordinate(0, 4, true), new Coordinate(MouseGridX, MouseGridY, true));
                foreach (Coordinate step in CurrentPath)
                {
                    if (Grid[step.Y, step.X] == ' ')
                        Grid[step.Y, step.X] = 'P';
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        MouseState MouseCursor  = Mouse.GetState();
        
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        // TODO: Add your drawing code here
        int ActiveColumnPosition = 0;
        int ActiveRowPosition = 0;
        char MouseOverGrid = Grid[MouseCursor.Y / SquareSize, MouseCursor.X / SquareSize];
        if (MouseOverGrid == ' ')
        {
            Grid[MouseCursor.Y / SquareSize, MouseCursor.X / SquareSize] = 'E';
        }
        
        List<Coordinate> Path = PathFind.FindPath(Grid, new Coordinate(0,4, true), new Coordinate(MouseCursor.X / SquareSize,MouseCursor.Y / SquareSize, true));
        for (int Count = 0; Count < Path.Count; Count++)
        {
            Grid[Path[Count].Y, Path[Count].X] = 'P';
        }
        
        for (int Row = 0; Row < 10; Row++)
        {
            ActiveRowPosition = SquareSize * Row;
            for (int Column = 0; Column < 15; Column++)
            {
                ActiveColumnPosition = SquareSize * Column;
                switch (Grid[Row, Column])
                {
                    case 'W':
                        ActiveColor = Color.Black;
                        break;
                    case 'P':
                        ActiveColor = Color.Blue;
                        break;
                    case ' ':
                        ActiveColor = Color.Purple;
                        break;
                    case 'S':
                        ActiveColor = Color.Green;
                        break;
                    case 'E' :
                        ActiveColor = Color.Red;
                        break;
                }
                Console.WriteLine(Grid[Row, Column].ToString());
                _spriteBatch.Draw(Pixel, new Rectangle(ActiveColumnPosition, ActiveRowPosition, SquareSize, SquareSize), ActiveColor);
                _spriteBatch.Draw(Pixel, new Rectangle(ActiveColumnPosition, ActiveRowPosition, SquareSize, OutlineThickness), Color.Black);
                _spriteBatch.Draw(Pixel, new Rectangle(ActiveColumnPosition, ActiveRowPosition + SquareSize - OutlineThickness, SquareSize, OutlineThickness), Color.Black);
                _spriteBatch.Draw(Pixel, new Rectangle(ActiveColumnPosition, ActiveRowPosition, OutlineThickness, SquareSize), Color.Black);
                _spriteBatch.Draw(Pixel, new Rectangle(ActiveColumnPosition + SquareSize - OutlineThickness, ActiveRowPosition, OutlineThickness, SquareSize), Color.Black);
            }
        }
        _spriteBatch.End();
        
        //Console.WriteLine(MouseCursor.Position);
        base.Draw(gameTime);
    }
}
