namespace Stronghold_Crusader_Project.Code.Other;

public class Camera2D
{
    private Vector2 Position {get; set;} //Position of camera
    private float Zoom {get; set;} //Camera zoom
    private float Rotation {get; set;} //Rotation of camera
    private Viewport WindowFrame {get; set;} //Frame of the window 
    private int PreviousScrollWheelValue {get; set;}
    private float MinZoom = 0.2f;
    private float MaxZoom = 5f;
    private float ZoomSensitivity = 0.1f;
    private Vector2 Direction;
    private float MovementAmount = 20;
    private float RotationAmount = MathHelper.ToRadians(90);

    public Camera2D(Viewport viewport) //Initalise of a new camera
    {
        Position = Vector2.Zero; //default values set
        Zoom = 1f;
        Rotation = 0f;
        WindowFrame = viewport;
        PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Getting the current scroll wheel value to save it
    }

    public Matrix GetViewMatrix() //Get how the camera should look and be transformed onto the game
    {
        Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
        Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1f);
        Matrix ScreenCentre = Matrix.CreateTranslation(WindowFrame.Width / 2f, WindowFrame.Height / 2f, 0f);
        Matrix NewViewMatrix = NewTranslation * NewRotation * NewScale * ScreenCentre;
        return NewViewMatrix;
    }

    public void UpdateCamera(MouseState ActiveMouse, KeyboardState ActiveKeyboard) //Updating the camera if a key is pressed or the mouse is scrolled
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
