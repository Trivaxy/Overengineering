using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.Scenes;
using System.Collections.Generic;
using System.Linq;

namespace Overengineering
{
    public static class LayerHost
    {
        private static Dictionary<string, Layer> Layers = new Dictionary<string, Layer>();

        private static void Order() => Layers.OrderBy(n => -n.Value.Priority);

        public static void RegisterLayer(Layer layer, string Name)
        {
            Layers.Add(Name, layer);
            Order();
        }

        public static void DrawLayers(Scene scene, SpriteBatch sb)
        {
            foreach (IDrawable entity in scene.Drawables)
            {
                if (entity is Triangles)
                    Layers[entity.Layer ?? "Default"].AppendPrimitiveCall(entity.Draw);
                else
                    Layers[entity.Layer ?? "Default"].AppendCall(entity.Draw);
            }

            foreach (Layer layer in Layers.Values)
                layer.Draw(sb);
        }
    }
}
