using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Overengineering.Graphics
{
    public class ModelComponent
    {
        public Vector3 Position;
        public Vector3 Rotation;

        public Model currentModel { get; private set; }

        public GraphicsDevice GraphicsDevice { get; set; }

        public Effect Effect { get; set; }

        public float Scale { get; set; }

        public Action<Effect> ShaderParameters;

        public ModelComponent(Model currentModelInput)
        {
            Model model = currentModelInput;
            currentModel = model;
            GraphicsDevice = Renderer.GraphicsDeviceManager.GraphicsDevice;
            Scale = 1f;
            Effect = null;
        }

        public void Update() { }

        public void Unload()
        {
            //Mechanic.GetMechanic<ModelHost>().modelComponents.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;

            Matrix world =
                      Matrix.CreateRotationX(Rotation.X)
                    * Matrix.CreateRotationY(Rotation.Y)
                    * Matrix.CreateRotationZ(Rotation.Z)
                    * Matrix.CreateScale(Scale)
                    * Matrix.CreateWorld(new Vector3(Position.X, -Position.Y, Position.Z), Vector3.UnitZ, Vector3.Up)
                    * Matrix.CreateTranslation(new Vector3(-width / 2, height / 2, 0)); //Move the models position

            // Compute camera matrices.
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 100), Vector3.Zero, Vector3.UnitY);

            //Create the 3D projection for this model
            Matrix projection = Matrix.CreateOrthographic(width, height, 0f, 1000f);

            Model model = currentModel;

            foreach (ModelMesh mesh in model.Meshes)
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
                            effect.SpecularPower = 100;
                            effect.AmbientLightColor = Color.DeepSkyBlue.ToVector3();
                            effect.SpecularColor = Color.White.ToVector3();
                        }
                    }
                }
                mesh.Draw();
            }
        }
    }
}