using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.Scenes;
using System.Collections.Generic;
using System.Linq;

namespace Overengineering
{
    public static class LayerHost
    {
        private static Dictionary<string, Layer> layers = new Dictionary<string, Layer>();

        private static void Order() => layers.OrderBy(n => -n.Value.Priority);

        public static void RegisterLayer(Layer layer, string name)
        {
            layers.Add(name, layer);
            Order();
        }

        public static void DrawLayers(Scene scene, SpriteBatch sb)
        {
            foreach (IDrawable entity in scene.Drawables)
            {
                if (entity is Triangles)
                    layers[entity.Layer ?? "Default"].AppendPrimitiveCall(entity.Draw);
                else
                    layers[entity.Layer ?? "Default"].AppendCall(entity.Draw);
            }

            foreach (Layer layer in layers.Values)
                layer.Draw(sb);
        }

        public static Layer GetLayer(string layerName) => layers[layerName ?? "Default"];
    }
}