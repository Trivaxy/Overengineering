using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overengineering.Scenes
{
	public static class SceneHolder
	{
		public static Scene CurrentScene { get; private set; }

		private static SceneTransition sceneTransition;

		public static void Update(GameTime time)
		{
			CurrentScene.Update(time);

			if (sceneTransition != null)
				UpdateTransition(time);

			GameInput.Instance.Update();
		}

		public static void StartScene(Scene scene)
		{
			CurrentScene?.OnDeactivate();
			CurrentScene = scene;
			CurrentScene.OnActivate();
		}

		public static void StartTransition(Scene targetScene, SceneTransition transition)
		{
			sceneTransition = transition;
			sceneTransition.TargetScene = targetScene;
		}

		public static void UpdateTransition(GameTime time)
		{
			sceneTransition.Update(time);

			if (--sceneTransition.TimeLeft == sceneTransition.TransitionPoint)
				StartScene(sceneTransition.TargetScene);

			if (sceneTransition.TimeLeft == 0)
				sceneTransition = null;
		}

		public static void DrawTransition(SpriteBatch sb) => sceneTransition?.Draw(sb);

		public static void AddEntity(Entity entity)
		{
			if (entity is ITickable tickable)
				CurrentScene.Tickables.Add(tickable);

			if (entity is IDrawable drawable)
				CurrentScene.Drawables.Add(drawable);
		}
	}
}
