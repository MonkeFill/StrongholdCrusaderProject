using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Other;

public static class Camera2D
{
    static Vector2 Position = Vector2.Zero; //Position of camera
    static float Zoom = 1f; //Camera zoom
    public static float Rotation = 0f; //Rotation of camera
    static Viewport WindowFrame;  //Frame of the window 
    static int PreviousScrollWheelValue = 0;
    private static float MinZoom = 0.2f;
    private static float MaxZoom = 5f;
    private static float ZoomSensitivity = 0.1f;
    private static Vector2 Direction;
    private static float MovementAmount = 20;
    private static float RotationAmount = MathHelper.ToRadians(90);

    public static void Initialize(Viewport viewport) //Initialise of a new camera
    {
        Position = Vector2.Zero; //default values set
        Zoom = 1f;
        Rotation = 0f;
        WindowFrame = viewport;
        PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Getting the current scroll wheel value to save it
    }

    public static Matrix GetViewMatrix() //Get how the camera should look and be transformed onto the game
    {
        Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
        Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1f);
        Matrix ScreenCentre = Matrix.CreateTranslation(WindowFrame.Width / 2f, WindowFrame.Height / 2f, 0f);
        Matrix NewViewMatrix = NewTranslation * NewRotation * NewScale * ScreenCentre;
        return NewViewMatrix;
    }

    public static void UpdateCamera(MouseState ActiveMouse, KeyboardState ActiveKeyboard) //Updating the camera if a key is pressed or the mouse is scrolled
    {
        Direction = Vector2.Zero;
        int NewScrollWheelValue = Mouse.GetState().ScrollWheelValue - PreviousScrollWheelValue;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            Direction += new Vector2(0, -1); //Move up
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            Direction += new Vector2(0, 1); //Move Down
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Direction += new Vector2(-1, 0); //Move Left
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Direction += new Vector2(1, 0); //Move Right
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {
            Rotation -= RotationAmount; //Rotating the camera left
        }
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {
            Rotation += RotationAmount; //Rotating the camera right
        }
        if (NewScrollWheelValue != PreviousScrollWheelValue)
        {
            Zoom  += (NewScrollWheelValue / 120f) * ZoomSensitivity; //Create the new zoom
            Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom); //Making sure it doesn't zoom too much
        }
        PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Setting the new scroll wheel value
        if (Direction != Vector2.Zero) //Setting the new direction
        {
            Direction.Normalize();
            Direction = Vector2.Transform(Direction, Matrix.CreateRotationZ(-Rotation));
            Position += Direction * MovementAmount;
        }
    }
}
