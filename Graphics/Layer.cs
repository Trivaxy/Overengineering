using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Overengineering
{
    public class Layer
    {
        private event Action<SpriteBatch> DrawCalls;

        public Effect LayerEffect { get; init; }

        public CameraTransform Camera { get; init; }

        public float Priority { get; init; }

        public Layer(float priority = 0, Effect effect = null, CameraTransform camera = default)
        {
            LayerEffect = effect;
            Priority = priority;
            Camera = camera;
        }

        public void AppendCall(Action<SpriteBatch> call) => DrawCalls += call;

        public void Draw(SpriteBatch sb)
        {
            //FNA has shitty overloads smh
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, LayerEffect, Camera.TransformationMatrix);

            LayerEffect?.CurrentTechnique.Passes[0]?.Apply();

            DrawCalls?.Invoke(sb);

            sb.End();

            DrawCalls = null;
        }
    }
}
