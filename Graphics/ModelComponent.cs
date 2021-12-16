using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Overengineering.Graphics
{
    public class ModelComponent : Entity, IDrawable, ITickable
    {
        public Model Model { get; private set; }

        public GraphicsDevice GraphicsDevice { get; set; }

        public Effect Effect { get; set; }

        public string Layer { get; set; }

        public Action<Effect> ShaderParameters;

        public ModelComponent(Model currentModelInput)
        {
            Model model = currentModelInput;
            Model = model;
            GraphicsDevice = Renderer.GraphicsDeviceManager.GraphicsDevice;
            Effect = null;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            Matrix world =
                      Matrix.CreateRotationX(Transform.Rotation.X)
                    * Matrix.CreateRotationY(Transform.Rotation.Y)
                    * Matrix.CreateRotationZ(Transform.Rotation.Z)
                    * Matrix.CreateScale(Transform.Scale)
                    * Matrix.CreateWorld(Transform.Position, Vector3.Forward, Vector3.Up); //Move the models position

            // Get camera matrices.
            CameraTransform camera = LayerHost.GetLayer(Layer).Camera;

            Matrix view = camera.ViewMatrix;
            Matrix projection = camera.ProjectionMatrix;

            Renderer.GraphicsDeviceManager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (ModelMesh mesh in Model.Meshes)
            {
                if (Effect != null)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = Effect;
                        Effect.Parameters["World"].SetValue(mesh.ParentBone.Transform * world);
                        Effect.Parameters["View"].SetValue(view);
                        Effect.Parameters["Projection"].SetValue(projection);
                        ShaderParameters?.Invoke(Effect);
                    }
                }
                else
                {
                    foreach (Effect effects in mesh.Effects)
                    {
                        if (effects is BasicEffect effect)
                        {
                            effect.World = mesh.ParentBone.Transform * world;
                            effect.View = view;
                            effect.Projection = projection;
                            effect.EnableDefaultLighting();
                            effect.FogEnabled = true;
                            effect.FogEnd = 4000f;
                            effect.FogStart = 2000f;
                            effect.PreferPerPixelLighting = true;
                            effect.FogColor = new Vector3(0);
                        }
                    }
                }
                mesh.Draw();
            }
        }
    }
}