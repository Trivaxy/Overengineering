using Microsoft.Xna.Framework;

namespace Overengineering
{
    public class CameraTransform : Entity
    {
        public Vector3 Direction;
        public float FieldOfView;
        public float NearPlane;
        public float FarPlane;

        public CameraTransform(Vector3 direction, float fieldOfView = MathHelper.PiOver4, float nearPlane = 1f, float farPlane = 5000f)
		{
            Direction = direction;
            FieldOfView = fieldOfView;
            NearPlane = nearPlane;
            FarPlane = farPlane;
		}

        public Matrix TransformationMatrix { get; set; }

        public Vector3 Target => Transform.Position + Direction;

        public Matrix ViewMatrix => Matrix.CreateLookAt(Transform.Position, Target, Vector3.Up);

        public Matrix ProjectionMatrix => Matrix.CreatePerspectiveFieldOfView(FieldOfView, Renderer.Device.Viewport.AspectRatio, NearPlane, FarPlane);


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
