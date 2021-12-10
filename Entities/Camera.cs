using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Overengineering
{
    public class CameraTransform : Entity
    {
        public Vector3 Direction;

        public Matrix TransformationMatrix { get; set; }

        public Vector3 Target => Transform.Position + Direction;

        protected virtual void OnUpdateTransform(GameTime gameTime) { }

        protected virtual void TransformConfiguration()
        {
            TransformationMatrix =
            Matrix.CreateTranslation(-Transform.Position) *
            Matrix.CreateRotationZ(Transform.Rotation.Z) *
            Matrix.CreateScale(GetScreenScale());
        }

        public void Update(GameTime gameTime)
        {
            OnUpdateTransform(gameTime);
            TransformConfiguration();
        }

        public Vector3 GetScreenScale()
        {
            float scaleX = 1;
            float scaleY = 1;

            return new Vector3(scaleX * Transform.Scale.X, scaleY * Transform.Scale.Y, 1.0f);
        }
    }
}
