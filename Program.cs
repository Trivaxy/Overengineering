using Microsoft.Xna.Framework;
using System;

namespace Overengineering
{
	public class Program : Game
	{
		static void Main(string[] args)
		{
			using (Program program = new Program())
			{
				new GraphicsDeviceManager(program);
				program.Run();
			}
		}
	}
}
