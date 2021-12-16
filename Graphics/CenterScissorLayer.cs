using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Overengineering
{
    public class CenterScisorLayer : Layer
    {
        public CenterScisorLayer(float priority, CameraTransform camera, Effect effect = null, Rectangle scissor = default, Rectangle destination = default)
            : base(priority, camera, effect, scissor, destination) { }

        public override void OnDraw()
        {
            Point MR = Renderer.MaxResolution;
            Point BBS = Renderer.BackBufferSize;

            ScissorSource = new Rectangle(MR.X / 2 - BBS.X / 2, MR.Y / 2 - BBS.Y / 2, BBS.X, BBS.Y);
            Destination = new Rectangle(0, 0, BBS.X, BBS.Y);
        }
    }
}