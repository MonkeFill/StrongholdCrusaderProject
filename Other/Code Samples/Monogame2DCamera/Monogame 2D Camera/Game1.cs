using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_2D_Camera
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private float RotationAmount = MathHelper.ToRadians(90);
        private int gridSize = 50;          // Larger grid to support more layers
        private int cellSize = 35;
        private Point gridCenter;
        private Camera ViewCamera;
        private Texture2D squareTexture;
        private Dictionary<int, Color> layerColors;
        private float KeyDebounce = 250;
        private float CameraMovementAmount = 20;
        private int PreviousScrollWheelValue;
        private float ZoomSensitivity = 0.1f;
        private float MinZoom = 0.2f;
        private float MaxZoom = 5f;
        private Dictionary<Keys, float> KeyPressTime = new Dictionary<Keys, float>();

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Get display size
            DisplayMode display = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            int screenWidth = display.Width;
            int screenHeight = display.Height;
        // Set window size to full screen resolution (windowed)
        _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            gridCenter = new Point(gridSize / 2, gridSize / 2);
            layerColors = new Dictionary<int, Color>();
            ViewCamera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
            PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 1x1 white pixel texture
            squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            squareTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState ActiveMouse = Mouse.GetState();
            int NewScrollWheelValue = ActiveMouse.ScrollWheelValue - PreviousScrollWheelValue;
            if (NewScrollWheelValue != 0)
            {
                float ZoomChange = (NewScrollWheelValue / 120f) * ZoomSensitivity;
                ViewCamera.Zoom += ZoomChange;

                ViewCamera.Zoom = MathHelper.Clamp(ViewCamera.Zoom, MinZoom, MaxZoom);
            }
            PreviousScrollWheelValue = ActiveMouse.ScrollWheelValue;
            Vector2 Direction = Vector2.Zero;
            CheckDeboundedKeys(gameTime);
            // Exit on Escape key press
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (CheckIfKeyAvailable(Keys.E, gameTime))
            {
                ViewCamera.Rotation += RotationAmount;
            }
            else if (CheckIfKeyAvailable(Keys.Q, gameTime))
            {
                ViewCamera.Rotation -= RotationAmount;
            }
            else if (CheckIfKeyAvailable(Keys.W, gameTime))
            {
                Direction += new Vector2(0, -1);
            }
            else if (CheckIfKeyAvailable(Keys.S, gameTime))
            {
                Direction += new Vector2(0, 1);
            }
            else if (CheckIfKeyAvailable(Keys.A, gameTime))
            {
                Direction += new Vector2(-1, 0);
            }
            else if (CheckIfKeyAvailable(Keys.D, gameTime))
            {
                Direction += new Vector2(1, 0);
            }
            if (Direction != Vector2.Zero)
            {
                Direction.Normalize();
                Matrix rotation = Matrix.CreateRotationZ(-ViewCamera.Rotation);
                Direction = Vector2.Transform(Direction, rotation);
                ViewCamera.Position += Direction * CameraMovementAmount;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: ViewCamera.GetViewMatrix());

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    int layer = GetChebyshevDistance(x, y, gridCenter.X, gridCenter.Y);

                    if (!layerColors.ContainsKey(layer))
                        layerColors[layer] = GetColorFromHSV((layer * 30) % 360, 0.8f, 0.9f); // Generates vibrant colors

                    Color color = layerColors[layer];
                    Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize - 1, cellSize - 1);
                    _spriteBatch.Draw(squareTexture, rect, color);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private int GetChebyshevDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }

        private Color GetColorFromHSV(double hue, double saturation, double value)
        {
            // Converts HSV to RGB (returns a MonoGame Color)
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            return hi switch
            {
                0 => new Color(v, t, p),
                1 => new Color(q, v, p),
                2 => new Color(p, v, t),
                3 => new Color(p, q, v),
                4 => new Color(t, p, v),
                _ => new Color(v, p, q),
            };
        }
        private bool CheckIfKeyAvailable(Keys CheckKey, GameTime TimeGame)
        {
            KeyboardState ActiveKeyboard = Keyboard.GetState();
            if(ActiveKeyboard.IsKeyDown(CheckKey))
            {
                if (!KeyPressTime.ContainsKey(CheckKey))
                {
                    KeyPressTime.Add(CheckKey, (float)TimeGame.TotalGameTime.TotalMilliseconds);
                    return true;
                }
            }
            return false;
        }
        private void CheckDeboundedKeys(GameTime TimeGame)
        {
            
            for (int Count = 0; Count < KeyPressTime.Count; Count++)
            {
                double ActiveDebound = KeyPressTime.ElementAt(Count).Value;
                if (ActiveDebound + KeyDebounce < TimeGame.TotalGameTime.TotalMilliseconds)
                {
                    KeyPressTime.Remove(KeyPressTime.ElementAt(Count).Key);
                }
            }
        }
    }
}
