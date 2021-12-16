using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Overengineering.UI;
using System;

namespace Overengineering
{
    public class EntityFocalCamera : CameraTransform
    {
        public Entity FocalEntity { get; init; }

        public Vector2 Angle;
        public float MouseSensitivity = 0.5f;
        public float InternalWalkSpeed = 10;

        public EntityFocalCamera(Entity entity, Vector3 cameraDirection, float fieldOfView = MathHelper.PiOver4, float nearPlane = 1f, float farPlane = 5000f)
            : base(cameraDirection, fieldOfView, nearPlane, farPlane)
            => FocalEntity = entity;


        protected override void OnUpdateTransform(GameTime gameTime)
        {
            KeyboardState k = Keyboard.GetState();

            Vector2 directionX = new Vector2((float)Math.Sin(Angle.X), (float)Math.Cos(Angle.X));
            Vector2 directionY = new Vector2((float)Math.Sin(Angle.Y), (float)Math.Cos(Angle.Y));

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Angle.X += GameInput.Instance.DeltaMousePosition.X * MouseSensitivity * gameTime.DeltaTime();
                Angle.Y += GameInput.Instance.DeltaMousePosition.Y * MouseSensitivity * gameTime.DeltaTime();
            }

            Direction = Vector3.Normalize(new Vector3(directionX.X, directionY.X, directionX.Y * directionY.Y));

            if (FocalEntity != null)
                Transform.Position = FocalEntity.Transform.Position;
            else
            {
                if (k.IsKeyDown(Keys.W)) Transform.Position += new Vector3(Direction.X, 0, Direction.Z) * InternalWalkSpeed;
                if (k.IsKeyDown(Keys.S)) Transform.Position -= new Vector3(Direction.X, 0, Direction.Z) * InternalWalkSpeed;

                if (k.IsKeyDown(Keys.D)) Transform.Position += new Vector3(-Direction.Z, 0, Direction.X) * InternalWalkSpeed;
                if (k.IsKeyDown(Keys.A)) Transform.Position -= new Vector3(-Direction.Z, 0, Direction.X) * InternalWalkSpeed;
            }

            Logger.NewText(Direction);
        }
    }
}
