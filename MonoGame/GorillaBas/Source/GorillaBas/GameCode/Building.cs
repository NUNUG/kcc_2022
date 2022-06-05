using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GorillaBas.GameCode
{
	public class Building
	{
		public int Height { get; set; }
		public int Width { get; set; }
		public Color BuildingColor { get; set; }
		public Rectangle Area { get; set; }
		public Color WindowColor { get; set; }

		public static Building Create(GameSettings gameSettings, int leftOffset)
		{
			var buildingLimits = gameSettings.BuildingLimits;
			(var minBuildingWidth, var maxBuildingWidth, var minBuildingHeight, var maxBuildingHeight) = buildingLimits;


			Random rnd = new Random();
			int width = rnd.Next(minBuildingWidth, maxBuildingWidth);
			int height = rnd.Next(minBuildingHeight, maxBuildingHeight);
			
			var availableColors = new[]
			{
				Color.Gray,
				Color.Blue,
				Color.Beige,
				Color.Maroon
			};

			int colorIndex = rnd.Next(0, availableColors.Length);
			Color buildingColor = availableColors[colorIndex];

			var windowColorOptions = new[] { Color.Black, Color.Yellow, Color.Orange };
			var windowColorIndex = rnd.Next(windowColorOptions.Length);
			var windowColor = windowColorOptions[windowColorIndex];

			var result = new Building(gameSettings, width, height, buildingColor, leftOffset, windowColor);
			return result;
		}

		public Building(GameSettings gameSettings, int width, int height, Color buildingColor, int leftOffset, Color windowColor)
		{
			Width = width;
			Height = height;
			BuildingColor = buildingColor;
			Area = new Rectangle(leftOffset, gameSettings.ScreenSize.Height - height, Width, Height);
			WindowColor = windowColor;
		}
	}
}
