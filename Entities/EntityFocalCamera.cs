using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Overengineering.Scenes;
using System;
using System.Diagnostics;

namespace Overengineering
{
    public class EntityFocalCamera : CameraTransform
    {
        public Entity FocalEntity { get; init; }

        public Vector2 Angle;
        public float MouseSensitivity = 0.5f;
        public float InternalWalkSpeed = 10;

        public EntityFocalCamera(Entity entity) => FocalEntity = entity;

        protected override void OnUpdateTransform(GameTime gameTime)
        {
            KeyboardState k = Keyboard.GetState();

            Angle.X += GameInput.Instance.DeltaMousePosition.X * MouseSensitivity * gameTime.DeltaTime();
            Angle.Y += GameInput.Instance.DeltaMousePosition.Y * MouseSensitivity * gameTime.DeltaTime();

            Vector2 directionX = new Vector2((float)Math.Sin(Angle.X), (float)Math.Cos(Angle.X));
            Vector2 directionY = new Vector2((float)Math.Sin(Angle.Y), (float)Math.Cos(Angle.Y));

            Direction = new Vector3(directionX.X, directionY.X, directionX.Y * directionY.Y);

            if (FocalEntity != null)
                Transform.Position = FocalEntity.Transform.Position;
            else
            {
                if (k.IsKeyDown(Keys.W)) Transform.Position += new Vector3(Direction.X, 0, Direction.Z) * InternalWalkSpeed;
                if (k.IsKeyDown(Keys.S)) Transform.Position -= new Vector3(Direction.X, 0, Direction.Z) * InternalWalkSpeed;

                if (k.IsKeyDown(Keys.D)) Transform.Position += new Vector3(-Direction.Z, 0, Direction.X) * InternalWalkSpeed;
                if (k.IsKeyDown(Keys.A)) Transform.Position -= new Vector3(-Direction.Z, 0, Direction.X) * InternalWalkSpeed;
            }
        }
    }
}
