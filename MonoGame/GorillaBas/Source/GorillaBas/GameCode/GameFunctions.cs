using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GorillaBas.GameCode
{
	public class GameFunctions
	{
		private Random rnd = new Random();
		private KeyboardState prevKbdState;
		private KeyboardState kbdState;

		public GameFunctions()
		{
			prevKbdState = Keyboard.GetState();
			kbdState = prevKbdState;
		}

		public List<Building> GenerateBuildings(GameSettings gameSettings)
		{
			var result = new List<Building>();
			int left = 0;
			int lastWidth = 0;
			while (left + lastWidth < gameSettings.ScreenSize.Width)
			{
				Building b = Building.Create(gameSettings, left);
				result.Add(b);
				left += b.Width + 1;
			}
			return result;
		}

		public (GorillaData leftGorilla, GorillaData rightGorilla) CreateGorillas(GameSettings gameSettings, List<Building> buildings)
		{
			// Left gorilla belongs on the left half, right gorilla belongs on the right half.
			int middleBuilding = buildings.Count / 2;

			// Do not place gorillas on the first or last building on their half of the screen.
			// If there is an odd number, they may end up on the same building.
			// If they are the last, they may end up off-screen.
			int left1 = 2;
			int left2 = middleBuilding - 1;
			int right1 = middleBuilding + 1;
			int right2 = buildings.Count - 1;
			
			int leftBuildingIndex = rnd.Next(left1, left2 + 1) - 1;
			int rightBuildingIndex = rnd.Next(right1, right2 + 1) - 1;

			Building leftBuilding = buildings[leftBuildingIndex];
			Building rightBuilding = buildings[rightBuildingIndex];

			(int X, int Y) leftGorillaCenter = (leftBuilding.Area.Center.X, leftBuilding.Area.Center.Y);
			(int X, int Y) rightGorillaCenter = (rightBuilding.Area.Center.X, rightBuilding.Area.Center.Y);
			(int X, int Y) leftGorillaPosition = (leftGorillaCenter.X - gameSettings.GorillaSize / 2, leftBuilding.Area.Top - gameSettings.GorillaSize);
			(int X, int Y) rightGorillaPosition = (rightGorillaCenter.X - gameSettings.GorillaSize / 2, rightBuilding.Area.Top - gameSettings.GorillaSize);

			return (
				// Left Gorilla
				new GorillaData()
				{
					Name = "Player 1",
					Area = new Rectangle(leftGorillaPosition.X, leftGorillaPosition.Y, gameSettings.GorillaSize, gameSettings.GorillaSize),
					Angle = gameSettings.InitialAngle,
					DirectionModifier = 1,
					Velocity = gameSettings.InitialVelocity
				},
				// Right Gorilla
				new GorillaData()
				{
					Name = "Player 2",
					Area = new Rectangle(rightGorillaPosition.X, rightGorillaPosition.Y, gameSettings.GorillaSize, gameSettings.GorillaSize),
					Angle = gameSettings.InitialAngle, // Player two throws the banana in the opposite direction, so we make it a negative angle.
					DirectionModifier = -1,
					Velocity = gameSettings.InitialVelocity
				}
			);
		}

		private bool CheckForKeyPress(Keys keyToCheckFor)
		{
			return kbdState.IsKeyDown(keyToCheckFor) && !prevKbdState.IsKeyDown(keyToCheckFor);
		}

		public InputState ReadInput()
		{
			kbdState = Keyboard.GetState();
			var result = new InputState()
			{
				EscPressed = CheckForKeyPress(Keys.Escape),
				UpArrowPressed = CheckForKeyPress(Keys.Up),
				DownArrowPressed = CheckForKeyPress(Keys.Down),
				LeftArrowPressed = CheckForKeyPress(Keys.Left),
				RightArrowPressed = CheckForKeyPress(Keys.Right),
				SpacePressed = CheckForKeyPress(Keys.Space),

				UpArrowHeld = kbdState.IsKeyDown(Keys.Up),
				DownArrowHeld = kbdState.IsKeyDown(Keys.Down),
				LeftArrowHeld = kbdState.IsKeyDown(Keys.Left),
				RightArrowHeld = kbdState.IsKeyDown(Keys.Right),
			};
			prevKbdState = kbdState;
			return result;
		}

		//public bool DoRectanglesOverlap(Rectangle rectangle1, Rectangle rectangle2)
		//{
		//	// If either side of the second rectangle is between the sides of the first triangle, there is a horizontal overlap.
		//	bool xOverlap = (rectangle1.Left < rectangle2.Left && rectangle2.Left < rectangle1.Right)
		//		|| (rectangle1.Left < rectangle2.Right && rectangle2.Right < rectangle1.Right);

		//	// If either top or bottom of the second rectangle is between the top and bottom of the first triangle, there is a vertical overlap.
		//	bool yOverlap = (rectangle1.Top < rectangle2.Top && rectangle2.Top < rectangle1.Bottom)
		//		|| (rectangle1.Top < rectangle2.Bottom && rectangle2.Bottom < rectangle1.Bottom);

		//	// If there is both horizontal and vertical overlap, the rectangles are overlapped.
		//	return xOverlap && yOverlap;
		//}

		//public bool DoRectanglesOverlap(Rectangle rectangle1, Rectangle rectangle2)
		//{
		//	// If either side of the second rectangle is between the sides of the first triangle, there is a horizontal overlap.
		//	bool xOverlap = (rectangle1.Left < rectangle2.Left && rectangle2.Left < rectangle1.Right)
		//		|| (rectangle1.Left < rectangle2.Right && rectangle2.Right < rectangle1.Right)
		//		|| (rectangle2.Left < rectangle1.Left && rectangle1.Left < rectangle2.Right)
		//		|| (rectangle2.Left < rectangle1.Right && rectangle1.Right < rectangle2.Right);

		//	// If either top or bottom of the second rectangle is between the top and bottom of the first triangle, there is a vertical overlap.
		//	bool yOverlap = (rectangle1.Top < rectangle2.Top && rectangle2.Top < rectangle1.Bottom)
		//		|| (rectangle1.Top < rectangle2.Bottom && rectangle2.Bottom < rectangle1.Bottom)
		//		|| (rectangle2.Top < rectangle1.Top && rectangle1.Top < rectangle2.Bottom)
		//		|| (rectangle2.Top < rectangle1.Bottom && rectangle1.Bottom < rectangle2.Bottom);

		//	// If there is both horizontal and vertical overlap, the rectangles are overlapped.
		//	return xOverlap && yOverlap;
		//}

		public bool BananaCollidedWith(Rectangle bananaRectangle, Rectangle rectangle2)
		{
			//// If either side of the second rectangle is between the sides of the first triangle, there is a horizontal overlap.
			//bool xOverlap = (rectangle1.Left < rectangle2.Left && rectangle2.Left < rectangle1.Right)
			//	|| (rectangle1.Left < rectangle2.Right && rectangle2.Right < rectangle1.Right);

			//// If either top or bottom of the second rectangle is between the top and bottom of the first triangle, there is a vertical overlap.
			//bool yOverlap = (rectangle1.Top < rectangle2.Top && rectangle2.Top < rectangle1.Bottom)
			//	|| (rectangle1.Top < rectangle2.Bottom && rectangle2.Bottom < rectangle1.Bottom);

			//// If there is both horizontal and vertical overlap, the rectangles are overlapped.
			//return xOverlap && yOverlap;

			//// If either side of the second rectangle is between the sides of the first triangle, there is a horizontal overlap.
			//bool xOverlap = (rectangle1.Left < rectangle2.Left && rectangle2.Left < rectangle1.Right)
			//	|| (rectangle1.Left < rectangle2.Right && rectangle2.Right < rectangle1.Right)
			//	|| (rectangle2.Left < rectangle1.Left && rectangle1.Left < rectangle2.Right)
			//	|| (rectangle2.Left < rectangle1.Right && rectangle1.Right < rectangle2.Right);

			//// If either top or bottom of the second rectangle is between the top and bottom of the first triangle, there is a vertical overlap.
			//bool yOverlap = (rectangle1.Top < rectangle2.Top && rectangle2.Top < rectangle1.Bottom)
			//	|| (rectangle1.Top < rectangle2.Bottom && rectangle2.Bottom < rectangle1.Bottom)
			//	|| (rectangle2.Top < rectangle1.Top && rectangle1.Top < rectangle2.Bottom)
			//	|| (rectangle2.Top < rectangle1.Bottom && rectangle1.Bottom < rectangle2.Bottom);

			//// If there is both horizontal and vertical overlap, the rectangles are overlapped.
			//return xOverlap && yOverlap;

			bool xOverlap = (bananaRectangle.Left > rectangle2.Left && bananaRectangle.Left < rectangle2.Right);
			bool yOverlap = (bananaRectangle.Top > rectangle2.Top && bananaRectangle.Top < rectangle2.Bottom);

			return xOverlap && yOverlap;
		}
	}
}
