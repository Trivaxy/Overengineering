using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overengineering.UI
{
	public delegate void MouseHover(Vector2 mousePos);

	public delegate void Click(Vector2 mousePos);

	public delegate void RightClick(Vector2 mousePos);

	public delegate void MouseHold(Vector2 mousePos);

	public delegate void MouseRightHold(Vector2 mousePos);

	public abstract class UIElement : IDrawable, ITickable
	{
		public Vector2 Position;
		public Vector2 Size;
		public event MouseHover OnMouseHover;
		public event Click OnClick;
		public event RightClick OnRightClick;
		public event MouseHold OnMouseHold;
		public event MouseRightHold OnMouseRightHold;

		public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

		public virtual string Layer => "UI";

		public abstract void Draw(SpriteBatch sb);

		public virtual void OnUpdate(GameTime time) { }

		public void Update(GameTime time)
		{
			OnUpdate(time);

			Vector2 mousePos = GameInput.Instance.MousePosition;

			if (BoundingBox.Contains((int)mousePos.X, (int)mousePos.Y))
			{
				OnMouseHover.Invoke(mousePos);

				if (GameInput.Instance.JustClickingLeft)
					OnClick.Invoke(mousePos);

				if (GameInput.Instance.JustClickingRight)
					OnRightClick.Invoke(mousePos);

				if (GameInput.Instance.IsClicking)
					OnMouseHold.Invoke(mousePos);

				if (GameInput.Instance.IsRightClicking)
					OnMouseRightHold.Invoke(mousePos);
			}
		}
	}
}