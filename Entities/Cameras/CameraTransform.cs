using Microsoft.Xna.Framework;

namespace Overengineering
{
    public enum FrustrumType
    {
        Regular,
        FOV,
        OffCenter
    }

    public class CameraTransform : Entity
    {
        public Vector3 Direction;
        public float FieldOfView;
        public float NearPlane;
        public float FarPlane;

        public FrustrumType ProjectionType;

        public CameraTransform(Vector3 direction, float fieldOfView = MathHelper.PiOver2, FrustrumType frustrum = FrustrumType.FOV, float nearPlane = .1f, float farPlane = 5000f, Vector3? scale = null)
        {
            Direction = direction;
            FieldOfView = fieldOfView;
            NearPlane = nearPlane;
            FarPlane = farPlane;
            Transform.Scale = scale ?? Vector3.One;

            ProjectionType = frustrum;
        }

        public Matrix TransformationMatrix { get; set; }

        public Vector3 Target => Transform.Position + Direction;

        public Matrix ViewMatrix => Matrix.CreateLookAt(Transform.Position, Target, Vector3.Up);

        public Matrix WorldMatrix => Matrix.CreateScale(Transform.Scale);

        public Matrix ProjectionMatrix
        {
            get
            {
                switch (ProjectionType)
                {
                    case FrustrumType.Regular:
                        return Matrix.CreatePerspective(1, (float)Renderer.MaxResolution.Y / Renderer.MaxResolution.X, NearPlane, FarPlane);

                    case FrustrumType.FOV:
                        return Matrix.CreatePerspectiveFieldOfView(FieldOfView, Renderer.BackBufferSize.X / (float)Renderer.BackBufferSize.Y, NearPlane, FarPlane);
                    //TODO: do this one :P
                    case FrustrumType.OffCenter:
                        return Matrix.CreatePerspectiveOffCenter(0,0,0,0, NearPlane, FarPlane);
                }

                return Matrix.Identity;
            }
        }
           

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
        TransformConfiguration();
        OnUpdateTransform(gameTime);
    }

    public Vector3 GetScreenScale()
    {
        float scaleX = 1;
        float scaleY = 1;

        return new Vector3(scaleX * Transform.Scale.X, scaleY * Transform.Scale.Y, 1.0f);
    }
}
}
