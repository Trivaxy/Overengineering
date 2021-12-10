using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;

namespace Overengineering.Scenes
{
	public class TestScene : Scene
	{
        public static ModelComponent Tree { get; set; }

        public override void OnActivate()
        {
            Tree = new ModelComponent(Assets<Model>.Get("Models/Tree"));

            SceneHolder.AddEntity(Tree);

            Tree.Transform.Scale = new Vector3(1);
            Tree.Transform.Position = new Vector3(0);
        }
    }
}
