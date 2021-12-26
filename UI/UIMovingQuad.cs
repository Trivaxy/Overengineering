using Microsoft.Xna.Framework;
using Overengineering.Utilities;
using System;

namespace Overengineering.UI
{
	public class UIMovingQuad : UIQuad
	{
		public Vector3 TargetTopLeft;
		public Vector3 TargetTopRight;
		public Vector3 TargetBottomRight;
		public Vector3 TargetBottomLeft;
		public Vector3 OriginalTopLeft;
		public Vector3 OriginalTopRight;
		public Vector3 OriginalBottomRight;
		public Vector3 OriginalBottomLeft;
		private bool isMoving;
		private int moveTime;
		private int timer;
		private Func<float, float> easingFunction;

		public UIMovingQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color) : base(v1, v2, v3, v4, color)
		{
		}

		public void MoveTo(Vector3 topLeft, Vector3 topRight, Vector3 bottomRight, Vector3 bottomLeft, int duration, Func<float, float> easing)
		{
			TargetTopLeft = topLeft;
			TargetTopRight = topRight;
			TargetBottomRight = bottomRight;
			TargetBottomLeft = bottomLeft;
			OriginalTopLeft = Mesh.TopLeft.Position;
			OriginalTopRight = Mesh.TopRight.Position;
			OriginalBottomRight = Mesh.BottomRight.Position;
			OriginalBottomLeft = Mesh.BottomLeft.Position;
			moveTime = duration;
			isMoving = true;
			easingFunction = easing;
			timer = 0;
		}

		public override void OnUpdate(GameTime time)
		{
			base.OnUpdate(time);

			if (isMoving)
			{
				float t = easingFunction(timer++ / (float)moveTime);

				Mesh.TopLeft.Position = Utils.Interpolate(OriginalTopLeft, TargetTopLeft, t);
				Mesh.TopRight.Position = Utils.Interpolate(OriginalTopRight, TargetTopRight, t);
				Mesh.BottomRight.Position = Utils.Interpolate(OriginalBottomRight, TargetBottomRight, t);
				Mesh.BottomLeft.Position = Utils.Interpolate(OriginalBottomLeft, TargetBottomLeft, t);
			}

			if (timer >= moveTime)
				isMoving = false;
		}
	}
}
