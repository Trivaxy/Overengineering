using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.UI;
using FontStashSharp;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Overengineering.Maths;

namespace Overengineering.Scenes
{
    public class TestScene : Scene
    {
        public static ModelComponent Tree { get; set; }
        public static ModelComponent Monkey { get; set; }
        public static SoundEffect MonkeySong { get; set; }

        public static QuadMesh Floor;
        public static OnScreenLogger ScreenLogger;

        private readonly int FloorSize = 10000;

        public override void RegisterSystems()
        {
            AddSystem<KinematicEntitySystem>();
        }

        public override void OnActivate()
        {
            Tree = new ModelComponent(Assets<Model>.Get("Models/Tree"));
            Monkey = new ModelComponent(Assets<Model>.Get("Models/Monkey"), true);
            MonkeySong = Assets<SoundEffect>.Get("Sounds/monke");

            Monkey.Transform.Position.Y = 100;

            Floor = new QuadMesh(
                new Vector3(-FloorSize, 0, -FloorSize),
                new Vector3(-FloorSize, 0, FloorSize),
                new Vector3(FloorSize, 0, FloorSize),
                new Vector3(FloorSize, 0, -FloorSize),
                Color.AliceBlue, default,
                Assets<Texture2D>.Get("Textures/Floor"));

            ScreenLogger = new OnScreenLogger();

            SceneHolder.AddEntity(Monkey);
            SceneHolder.AddEntity(Tree);

            SceneHolder.AddEntity(new KinematicQuad(
                new Maths.Polygon[] { new Maths.Polygon(
                    new Vector3[] {
                        new Vector3(0, 0, 0),
                        new Vector3(50, 0, 0),
                        new Vector3(50, 50, 0),
                        new Vector3(0, 50, 0) }) }));

            SceneHolder.AddEntity(new KinematicQuad(
                 new Maths.Polygon[] { new Maths.Polygon(
                    new Vector3[] {
                        new Vector3(0, 0, 0),
                        new Vector3(60, 0, 0),
                        new Vector3(60, 50, 0),
                        new Vector3(0, 50, 0) }) }));

            Drawables.Add(Floor);
            Drawables.Add(ScreenLogger);
            Tickables.Add(ScreenLogger);

            Tree.Transform.Scale = new Vector3(1);
            Monkey.Transform.Scale = new Vector3(1);

            Tree.Transform.Position = new Vector3(0);

            Renderer.DefaultCamera.Transform.Position.Y += 100;
        }

        public override void Update(GameTime time)
        {
            if(Rand.random.Next(100) == 0)MonkeySong.Play();
            Monkey.Transform.Rotation.Y = Time.SineTime(Time.Current, 1f);
        }
    }

    public class TestText : IDrawable
    {
        public FontSystem Font;
        public string Text;
        public int FontSize;
        public Vector2 Position;
        public Color Color;

        public TestText(FontSystem font, string text, int fontSize, Vector2 position, Color color)
        {
            Font = font;
            Text = text;
            FontSize = fontSize;
            Position = position;
            Color = color;
        }

        public string Layer => "UI";

        public void Draw(SpriteBatch sb)
        {
            Font.GetFont(FontSize).DrawText(sb, Text, Position, Color);
        }
    }
}