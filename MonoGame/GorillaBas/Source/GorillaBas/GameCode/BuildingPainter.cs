using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GorillaBas.GameCode
{
	public class BuildingPainter
	{
		private readonly GameSettings gameSettings;
		private readonly LoadedContent loadedContent;
		private readonly GraphicsDevice graphics;
		private readonly SpriteBatch spriteBatch;
		private Pixel pixel;

		public BuildingPainter(GameSettings gameSettings, LoadedContent loadedContent, GraphicsDevice graphics, SpriteBatch spriteBatch)
		{
			this.gameSettings = gameSettings;
			this.loadedContent = loadedContent;
			this.graphics = graphics;
			this.spriteBatch = spriteBatch;
			pixel = new Pixel(graphics);
		}

		public void Draw()
		{
			var buildings = loadedContent.Buildings;
			buildings.ForEach(b => DrawBuilding(b));
		}

		private void DrawBuilding(Building b)
		{
			// Draw building.
			pixel.Color = b.BuildingColor;
			spriteBatch.Draw(pixel.Value, b.Area, Color.White);

			// Draw windows.
			DrawWindows(b);

		}

		private void DrawWindows(Building b)
		{
			pixel.Color = b.WindowColor;

			var buildingCenter = b.Width / 2;
			var leftCenter = buildingCenter / 2 + b.Area.Left;
			var rightCenter = buildingCenter / 2 + buildingCenter + b.Area.Left;
			var windowSize = buildingCenter / 2;
			var leftWindowLeft = leftCenter - windowSize / 2;
			var rightWindowLeft = rightCenter - windowSize / 2;
			var windowGap = buildingCenter / 2;


			var startY = b.Area.Top;
			var endY = b.Area.Bottom;
			var y = startY + windowGap;

			while (y < endY)
			{
				Rectangle windowRectLeft = new Rectangle(leftWindowLeft, y, windowSize, windowSize);
				spriteBatch.Draw(pixel.Value, windowRectLeft, Color.White);

				Rectangle windowRectRight = new Rectangle(rightWindowLeft, y, windowSize, windowSize);
				spriteBatch.Draw(pixel.Value, windowRectRight, Color.White);

				y += windowSize + windowGap;
			}
		}
	}
}
