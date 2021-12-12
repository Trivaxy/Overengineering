using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using System;

namespace Overengineering.Graphics.Meshes
{
    // OpenGL is a specification developed by the Khronos Group in 1992 specifying a flexible and efficient graphics API used to interface with a computer's graphics processo
    public class Triangles : IDrawable
    {
        protected VertexPositionColorTexture[] vertices;
        protected short[] indices;
        private int vertexPointer;
        private int indexPointer;
        private bool finished;
        private string layer;
        private Texture2D texture;

        private Effect effect;

        public string Layer => layer;

        public Triangles(int vertexCount, int indexCount, string layer, Texture2D texture = null, Effect effect = null)
        {
            vertices = new VertexPositionColorTexture[vertexCount];
            indices = new short[indexCount];
            this.layer = layer;
            this.effect = effect;
            this.texture = texture;
        }

        public void AddVertex(Vector3 position, Color color, Vector2 uv)
        {
            ResetIfFinished();

            vertices[vertexPointer++] = new VertexPositionColorTexture(position, color, uv);

            if (vertexPointer == vertices.Length + 1)
                Array.Resize(ref vertices, vertices.Length * 2);
        }

        public void AddIndex(short index)
        {
            ResetIfFinished();

            indices[indexPointer++] = index;

            if (indexPointer == indices.Length + 1)
                Array.Resize(ref indices, indices.Length * 2);
        }

        public void Finish() => finished = true;

        public void Reset()
        {
            Array.Clear(vertices, 0, vertices.Length);
            Array.Clear(indices, 0, indices.Length);
            vertexPointer = 0;
            indexPointer = 0;
        }

        private void ResetIfFinished()
        {
            if (finished)
            {
                finished = false;
                Reset();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            BasicEffect basicEffect = Assets<Effect>.Get("BasicEffect").GetValue() as BasicEffect;

            basicEffect.TextureEnabled = texture != null;
            basicEffect.Texture = texture;
            basicEffect.VertexColorEnabled = true;

            VertexBuffer vertexBuffer = new VertexBuffer(sb.GraphicsDevice, typeof(VertexPositionColorTexture), vertexPointer, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            IndexBuffer indexBuffer = new IndexBuffer(sb.GraphicsDevice, typeof(short), indexPointer, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);

            sb.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            sb.GraphicsDevice.Indices = indexBuffer;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                sb.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexPointer, 0, indexPointer / 3);
            }
        }
    }
}