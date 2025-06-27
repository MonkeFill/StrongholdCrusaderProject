using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_2D_Camera
{
    internal class Camera
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }
        private Viewport WindowFrame { get; set; }

        public Camera(Viewport windowframe)
        {
            Position = Vector2.Zero;
            Zoom = 1f;
            Rotation = 0f;
            WindowFrame = windowframe;
        }

        public Matrix GetViewMatrix()
        {
            Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
            Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
            Matrix Scale = Matrix.CreateScale(Zoom, Zoom, 1f);
            Matrix ScreenCentre = Matrix.CreateTranslation(WindowFrame.Width / 2f, WindowFrame.Height / 2f, 0f);
            Matrix NewViewMatrix = NewTranslation * NewRotation * Scale * ScreenCentre;
            return NewViewMatrix;
        }
    }
}
