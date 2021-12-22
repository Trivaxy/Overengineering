using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Overengineering.UI;
using System;

namespace Overengineering
{
    public class OrthoPerspectiveCamera : CameraTransform
    {
        public Vector3 Offset;

        public OrthoPerspectiveCamera(Vector3 cameraDirection, float fieldOfView = MathHelper.PiOver4, 
            FrustrumType frustrum = FrustrumType.FOV, float nearPlane = 1f, float farPlane = 5000f)

            : base(cameraDirection, fieldOfView, frustrum, nearPlane, farPlane) { }


        protected override void OnUpdateTransform(GameTime gameTime)
        {
            Transform.Position.Z = -(Renderer.PresentationParameters.BackBufferHeight * 0.5f) / (float)Math.Tan(FieldOfView * 0.5f);
            Transform.Position.X = -Renderer.PresentationParameters.BackBufferWidth / 2f;
            Transform.Position.Y = -Renderer.PresentationParameters.BackBufferHeight / 2f;

            Transform.Position += Offset;

            float XRatio = Renderer.BackBufferSize.X / (float)Renderer.ViewportSize.X;
            float YRatio = Renderer.BackBufferSize.Y / (float)Renderer.ViewportSize.Y;

            Transform.Scale = new Vector3(XRatio, YRatio, 1);
        }
    }
}
