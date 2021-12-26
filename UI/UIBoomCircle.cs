using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Utilities;
using System;

namespace Overengineering.UI
{
	public class UIBoomCircle : UIElement
	{
		public float Scale;
		public float MaxScale;
		public int TimeNeeded;
		public Action OnBoom;
		private int time;

		public UIBoomCircle(float scale, float maxScale, int time, Action onBoom = null)
		{
			Scale = scale;
			MaxScale = maxScale;
			TimeNeeded = time;
			OnBoom = onBoom;
		}

		public override void OnUpdate(GameTime _)
		{
			time++;

			if (time >= TimeNeeded)
			{
				OnBoom?.Invoke();
				Active = false;
			}
		}

		public override void OnDraw(SpriteBatch sb)
		{
			Texture2D texture = Assets<Texture2D>.Get("Textures/Circle").GetValue();
			float scale = Utils.EaseInQuint(Scale, MaxScale, time / (float)TimeNeeded);
			sb.Draw(texture, Position, null, Color.White, 0f, texture.Center(), scale, SpriteEffects.None, 0f);
		}
	}
}
