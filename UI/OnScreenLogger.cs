using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Overengineering.Graphics.Meshes;
using Overengineering.Utilities;
using System;
using System.Collections.Generic;

namespace Overengineering.UI
{
    public static class Logger
    {
        private static readonly int LogCount = 100;
        internal static List<string> Logs = new List<string>();

        public static int TimeWithoutLog = 0;
        public static void NewText(string LogMessage)
        {
            TimeWithoutLog = 0;
            Logs.Insert(0, LogMessage);
            if (Logs.Count > LogCount)
            {
                Logs.RemoveAt(LogCount);
            }
        }

        public static void NewText(object LogMessage)
        {
            string? LM = LogMessage.ToString();
            if (LM != null)
            {
                TimeWithoutLog = 0;
                Logs.Insert(0, LM);
                if (Logs.Count > LogCount)
                {
                    Logs.RemoveAt(LogCount);
                }
            }
        }
    }
    public class OnScreenLogger : IDrawable
    {
        public float LogAlpha;
        public string Layer => "UI";

        protected void Update()
        {
            Logger.TimeWithoutLog++;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) Logger.TimeWithoutLog = 0;

            if (Logger.TimeWithoutLog > 280) LogAlpha = LogAlpha *= 0.98f;
            else LogAlpha += (1 - LogAlpha) / 16f;
        }
        public void Draw(SpriteBatch sb)
        {
            int MaxOnscreenLogs = 10;
            var logger = Logger.Logs;

            Vector2 ASS = Renderer.ViewportSize.ToVector2();
            int Count = (int)MathHelper.Min(logger.Count, MaxOnscreenLogs);

            for (int i = 0; i < Count; i++)
            {
                float alpha = 1 - i / (float)MaxOnscreenLogs;
                //Utils.DrawTextToLeft(logger[i], Color.Yellow * alpha * LogAlpha, new Vector2(30, ASS.Y - 30 - 20 * i), sb);
            }
        }
    }
}