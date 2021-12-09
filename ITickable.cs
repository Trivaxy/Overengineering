using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Assets;
using Overengineering.Graphics;
using System.Collections.Generic;

namespace Overengineering
{
	public interface ITickable
	{
		void Update(GameTime gameTime);
	}
}
