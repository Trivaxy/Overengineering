using Microsoft.Xna.Framework;
using System;

namespace Overengineering
{
    public static class Time
    {
        public static float DeltaTime(this GameTime gt) => (float)gt.ElapsedGameTime.TotalSeconds;

        public static float Delta60(this GameTime gt) => gt.DeltaTime() * 60;

        public static float SineTime(this GameTime gt, float period, float displacement = 0) => (float)Math.Sin(gt.TotalGameTime.TotalSeconds * period + displacement);

        public static int GameUpdateCount;
    }
}
