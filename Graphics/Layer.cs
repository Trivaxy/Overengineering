﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using System;

namespace Overengineering
{
    public class Layer
    {
        private event Action<SpriteBatch> DrawCalls;
        private event Action<SpriteBatch> PrimitiveCalls;

        public Effect LayerEffect { get; init; }

        public CameraTransform Camera { get; init; }

        public float Priority { get; init; }

        public Layer(float priority, CameraTransform camera, Effect effect = null)
        {
            LayerEffect = effect;
            Priority = priority;
            Camera = camera;
        }

        public void AppendCall(Action<SpriteBatch> call) => DrawCalls += call;

        public void AppendPrimitiveCall(Action<SpriteBatch> call) => PrimitiveCalls += call;

        public void Draw(SpriteBatch sb)
        {
            Camera.Update(Time.Current);

            //FNA has shitty overloads smh
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, LayerEffect, Camera?.TransformationMatrix ?? Matrix.Identity);

            LayerEffect?.CurrentTechnique.Passes[0]?.Apply();

            DrawCalls?.Invoke(sb);

            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            BasicEffect basicEffect = Assets<Effect>.Get("BasicEffect").GetValue() as BasicEffect;

            basicEffect.View = Camera.ViewMatrix;
            basicEffect.Projection = Camera.ProjectionMatrix;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                PrimitiveCalls?.Invoke(sb);
            }

            sb.End();

            DrawCalls = null;
            PrimitiveCalls = null;
        }
    }
}