using Microsoft.Xna.Framework;
using Overengineering.UI;
using Overengineering.Utilities;

namespace Overengineering.Scenes
{
	public class MenuScene : Scene
	{
		private UIBoomCircle BoomCircle;
		private UIMovingQuad PlayButton;
		private UIMovingQuad OptionButton;
		private UIMovingQuad QuitButton;

		public override void OnActivate()
		{
			BoomCircle = new UIBoomCircle(0f, 0.5f, 60)
			{
				Position = new Vector2(140, 235),
				OnBoom = () =>
				{
					//PlayButton.MoveTo(new Vector3(410, 118, 0), new Vector3(612, 56, 0), new Vector3(624, 156, 0), new Vector3(409, 147, 0), 30, Utils.EaseOutQuint);
					PlayButton.MoveTo(BoomCircle.Position.WithZ(00), BoomCircle.Position.WithZ(0), BoomCircle.Position.WithZ(0), BoomCircle.Position.WithZ(0), 30, Utils.EaseOutQuint);
				}
			};

			Vector3 circleCenter = BoomCircle.Position.WithZ(0);
			PlayButton = new UIMovingQuad(circleCenter, circleCenter, circleCenter, circleCenter, Color.Red);
			OptionButton = new UIMovingQuad(circleCenter, circleCenter, circleCenter, circleCenter, Color.Green);
			QuitButton = new UIMovingQuad(circleCenter, circleCenter, circleCenter, circleCenter, Color.Aqua);

			Tickables.Add(BoomCircle);
			Drawables.Add(BoomCircle);
			Tickables.Add(PlayButton);
			Drawables.Add(PlayButton);

			OnScreenLogger logger = new OnScreenLogger();
			Tickables.Add(logger);
			Drawables.Add(logger);
		}

		public override void Update(GameTime time)
		{
			Logger.NewText(PlayButton.BoundingBox);
		}
	}
}
