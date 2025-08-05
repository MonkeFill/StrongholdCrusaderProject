using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Other;

public static class Camera2D
{
    
    static readonly int MapTotalHeight = GlobalConfig.MapTotalHeight;
    static readonly int MapTotalWidth = GlobalConfig.MapTotalWidth;
    static readonly int BorderHeight = GlobalConfig.BorderHeight;
    static readonly int BorderWidth = GlobalConfig.BorderWidth;
    static float MaxMapHeight => GlobalConfig.MaxMapHeight;
    static float MaxMapWidth => GlobalConfig.MaxMapWidth;
    static bool  MapVertical => GlobalConfig.MapVertical;
    static readonly float MaxZoom = GlobalConfig.MaxZoom;
    static readonly float ZoomSensitivity = GlobalConfig.ZoomSensitivity;
    static readonly float MovementAmount = GlobalConfig.MovementAmount;
    static readonly float MovementSpeed = GlobalConfig.MovementSpeed;
    static readonly float RotationAmount = GlobalConfig.RotationAmount;
    
    static Vector2 Position = Vector2.Zero; //Position of camera
    private static Vector2 TargetPosition = Vector2.Zero;
    private static float Zoom; //Camera zoom
    public static float Rotation; //Rotation of camera
    static Viewport WindowFrame;  //Frame of the window 
    static int PreviousScrollWheelValue;
    private static float HalfScreenHeight => WindowFrame.Height / 2f / Zoom;
    private static float HalfScreenWidth => WindowFrame.Width / 2f / Zoom;
    private static float MinZoom;
    private static Vector2 ScreenCentre =>  new Vector2(WindowFrame.Width / 2f, WindowFrame.Height / 2f);

    public enum CameraAction
    {
        Move,
        Zoom,
        Rotate,
        None
    }

    public static void Initialize(Viewport viewport) //Initialise of a new camera
    {
        //default values set
        Rotation = 0f;
        WindowFrame = viewport;
        PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Getting the current scroll wheel value to save it
        MinZoom = GetMinimumZoom();
        Zoom = MinZoom;
        //Zoom = MaxZoom - MinZoom;
        Position = new Vector2(MaxMapWidth / 2f, MaxMapHeight / 2f);
    }

    public static Matrix GetViewMatrix() //Get how the camera should look and be transformed onto the game
    {
        Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
        Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1f);
        Matrix ScreenCentreMatrix = Matrix.CreateTranslation(ScreenCentre.X, ScreenCentre.Y, 0f);
        Matrix NewViewMatrix = NewTranslation * NewScale * NewRotation * ScreenCentreMatrix;
        return NewViewMatrix;
    }

    public static void UpdateCamera(GameTime InputGameTime, CameraAction InputAction, Vector2 PositionChange , float RotationChange, float ZoomChange) //Updating the camera
    {
        float DeltaTime = (float)InputGameTime.ElapsedGameTime.TotalSeconds;
        if (InputAction != CameraAction.None)
        {
            MouseState ActiveMouseState = Mouse.GetState();
            Vector2 MouseCentre =  -new Vector2(ActiveMouseState.X, ActiveMouseState.Y);
            Vector2 WorldBeforeChange = CameraScreenToWorld(MouseCentre); //Getting how the world it is before 
            switch (InputAction)
            {
                case CameraAction.Move: //Move the camera to a new position
                    PositionChange.Normalize(); //Move same speed horizontally, vertically and diagonally
                    PositionChange = Vector2.Transform(PositionChange, Matrix.CreateRotationZ(-Rotation)); //Create the new position change based on the rotation
                    TargetPosition += PositionChange * MovementAmount * DeltaTime; //New position for where it should go
                    break;
                case CameraAction.Zoom: //Zooming into where the mouse is 
                    Zoom += (ZoomChange * ZoomSensitivity); //Adding the zoom
                    break;
                case CameraAction.Rotate:
                    Rotation += RotationAmount * RotationChange; //Adding rotations
                    break;
            }
            Vector2 WorldAfterChange = CameraScreenToWorld(MouseCentre);
            Vector2 WorldOffSet = WorldBeforeChange - WorldAfterChange;
            Position += WorldOffSet;
        }
        Position = Vector2.Lerp(Position, TargetPosition, MovementSpeed * DeltaTime); //Move from position to target position slowly
        ClampCamera();
        Console.WriteLine($"Zoom - {Zoom}");
       }
    private static Vector2 CameraScreenToWorld(Vector2 ScreenPosition) //Converting screen position to actual world position
    {
        Matrix InvertedMatrix = Matrix.Invert(GetViewMatrix());
        return Vector2.Transform(ScreenPosition, InvertedMatrix);
    }

    private static void ClampCamera() //A method to make sure that the camera doesn't go outside the bounds
    {
        //Camera Zoom Clamping
        Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom); //Making sure it isn't too high or low
        //Camera Rotation Clamping
        Rotation = Rotation % MathHelper.TwoPi; //Making it loop through 360 degrees
        if (Rotation < 0) //If it goes negative whilst turning left
        {
            Rotation += MathHelper.TwoPi; //Adding 360 to keep it within the loop
        }
        
        //Camera Movement Clamping
        float MaxPositionX;
        float MaxPositionY;
        float MinPositionX;
        float MinPositionY;
        if (MapVertical) //Two separate clamps weather the map is vertical or not since it will use different variables as it inverts the coordinates
        {
            MaxPositionY = MaxMapWidth - HalfScreenWidth; //Max width the camera can go to
            MaxPositionX = MaxMapHeight - HalfScreenHeight; //Max height the camera can go to
            MinPositionY = HalfScreenWidth; //min width the camera can go to
            MinPositionX = HalfScreenHeight; //min height the camera can go to
            
            if (MaxMapWidth <= HalfScreenWidth * 2) //If the map isn't as wide as the monitor
            {
                MinPositionY = MaxPositionY = MaxMapWidth / 2; //set the min and max to the width of the monitor
            }
            if (MaxMapHeight <= HalfScreenHeight * 2) //if the map isn't as tall as the monitor
            {
                MinPositionX = MaxPositionX = MaxMapHeight / 2; //set the min and max to the height of the monitor
            }
        }
        else
        {
            MaxPositionX = MaxMapWidth - HalfScreenWidth; //Max width the camera can go to
            MaxPositionY = MaxMapHeight - HalfScreenHeight; //Max height the camera can go to
            MinPositionX = HalfScreenWidth; //min width the camera can go to
            MinPositionY = HalfScreenHeight; //min height the camera can go to
            
            if (MaxMapWidth <= HalfScreenWidth * 2) //If the map isn't as wide as the monitor
            {
                MinPositionX = MaxPositionX = MaxMapWidth / 2; //set the min and max to the width of the monitor
            }
            if (MaxMapHeight <= HalfScreenHeight * 2) //if the map isn't as tall as the monitor
            {
                MinPositionY = MaxPositionY = MaxMapHeight / 2; //set the min and max to the height of the monitor
            }
        }
        Position.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        Position.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
        TargetPosition.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        TargetPosition.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
    }

    private static float GetMinimumZoom()
    {
        float ScreenWidth = WindowFrame.Width;
        float ScreenHeight = WindowFrame.Height;
        
        float ZoomNormal = Math.Max(ScreenWidth / MaxMapWidth, ScreenHeight / MaxMapHeight); //Which is bigger when normally rotated
        float ZoomRotated = Math.Max(ScreenWidth / MaxMapHeight, ScreenHeight / MaxMapWidth); //Which is bigger when rotated once
        return Math.Max(ZoomNormal, ZoomRotated);
    }

    public static Matrix GetViewMatrixWithoutRotation()
    {
        Matrix NewTranslation = Matrix.CreateTranslation(new Vector3(-Position, 0));
        Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1);
        Matrix NewScreenCentreTranslation = Matrix.CreateTranslation(new Vector3(ScreenCentre, 0));
        Matrix InverseRotation = Matrix.CreateRotationZ(-Rotation);
        return NewTranslation * NewRotation * NewScale * NewScreenCentreTranslation * InverseRotation;
    }
}
