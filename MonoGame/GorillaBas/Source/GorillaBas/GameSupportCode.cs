using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GorillaBas.GameCode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GorillaBas
{
	public partial class Game1
	{
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;

			GameSettings = new GameSettings();
			GameFunctions = new GameFunctions();
			previousBananas = new List<(int X, int Y)>();

			Firing = false;
		}

		private void NewPlayfield()
		{
			Explosion = new ExplosionData();
			LoadedContent.Buildings = GameFunctions.GenerateBuildings(GameSettings);
			GameFunctions.ResetGorillas(Player1, Player2, GameSettings, LoadedContent.Buildings);
			BuildingPainter = new BuildingPainter(GameSettings, LoadedContent, GraphicsDevice, spriteBatch);
		}


		protected override void Initialize()
		{
			// Set the screen resolution.
			graphics.PreferredBackBufferWidth = GameSettings.ScreenSize.Width;
			graphics.PreferredBackBufferHeight = GameSettings.ScreenSize.Height;
			graphics.IsFullScreen = GameSettings.FullScreen;
			graphics.ApplyChanges();

			base.Initialize();
		}

		private void DrawBananaGuide()
		{
			// If the banana is off the screen, draw an arrow on the top edge of the screen to show where it is.

			if (Banana.Area.Y < 0)
			{
				var image = LoadedContent.GuideArrow;
				(int Width, int Height) size = (GameSettings.GuideArrowSize, GameSettings.GuideArrowSize);
				var destRect = new Rectangle(Banana.Area.X - size.Width / 2, 0, size.Width, size.Height);
				spriteBatch.Draw(image, destRect, Color.White);
			}
		}

		private void DrawText()
		{
			float rowHeight = 25.0f;

			// Player1 text
			Vector2 player1TextVector = Vector2.Zero;
			Vector2 player1NameVector = new Vector2(player1TextVector.X, player1TextVector.Y + rowHeight * 1);
			Vector2 player1ScoreVector = new Vector2(player1TextVector.X, player1TextVector.Y + rowHeight * 2);
			Vector2 player1AngleVector = new Vector2(player1TextVector.X, player1TextVector.Y + rowHeight * 3);
			Vector2 player1VelocityVector = new Vector2(player1TextVector.X, player1TextVector.Y + rowHeight * 4);

			// Player2 text
			Vector2 player2TextVector = new Vector2(GameSettings.ScreenSize.Width / 2, 0);
			Vector2 player2NameVector = new Vector2(player2TextVector.X, player2TextVector.Y + rowHeight * 1);
			Vector2 player2ScoreVector = new Vector2(player2TextVector.X, player2TextVector.Y + rowHeight * 2);
			Vector2 player2AngleVector = new Vector2(player2TextVector.X, player2TextVector.Y + rowHeight * 3);
			Vector2 player2VelocityVector = new Vector2(player2TextVector.X, player2TextVector.Y + rowHeight * 4);

			// Draw the highlight
			Rectangle highlightRect;
			Color highlightColor = new Color(Color.Black, 0.25f);
			if (Players.CurrentPlayer == Player1)
			{
				highlightRect = new Rectangle((int)player1NameVector.X, (int)player1NameVector.Y - 5, (int)player2NameVector.X - 50, (int)rowHeight);
			}
			else
			{
				highlightRect = new Rectangle((int)player2NameVector.X, (int)player2NameVector.Y - 5, (int)GameSettings.ScreenSize.Width - (int)player2NameVector.X - 50, (int)rowHeight);
			}
			spriteBatch.Draw(Pixel.OfColor(highlightColor), highlightRect, Color.White);

			// Draw the texts.
			spriteBatch.DrawString(Font, $"{Player1.Name}", player1NameVector, Color.White);
			spriteBatch.DrawString(Font, $"Score: {Player1.Score}", player1ScoreVector, Color.White);
			spriteBatch.DrawString(Font, $"Angle: {Player1.Angle}", player1AngleVector, Color.White);
			spriteBatch.DrawString(Font, $"Velocity: {Player1.Velocity}", player1VelocityVector, Color.White);

			spriteBatch.DrawString(Font, $"{Player2.Name}", player2NameVector, Color.White);
			spriteBatch.DrawString(Font, $"Score: {Player2.Score}", player2ScoreVector, Color.White);
			spriteBatch.DrawString(Font, $"Angle: {Player2.Angle}", player2AngleVector, Color.White);
			spriteBatch.DrawString(Font, $"Velocity: {Player2.Velocity}", player2VelocityVector, Color.White);
		}

		private void DrawExplosion()
		{
			if (Explosion.Active)
			{
				int width = LoadedContent.SplosionImage.Width;
				int height = LoadedContent.SplosionImage.Height;

				var destRect = new Rectangle(
						Banana.Area.Left + this.Explosion.ImpactOffset.X,
						Banana.Area.Top + this.Explosion.ImpactOffset.Y,
						LoadedContent.SplosionImage.Width,
						LoadedContent.SplosionImage.Height);

				if (GameSettings.Debug)
					spriteBatch.Draw(Pixel.OfColor(Color.DarkRed), destRect, Color.White);

				Rectangle sourceRect = LoadedContent.SplosionImage.Bounds;
				float rotation = Explosion.Rotation;
				Vector2 origin = new Vector2(width / 2, height / 2);

				spriteBatch.Draw(
					LoadedContent.SplosionImage,
					destRect,
					sourceRect,
					Color.White,
					rotation,
					origin,
					SpriteEffects.None,
					0.0f);

				if (GameSettings.Debug)
				{
					spriteBatch.Draw(Pixel.OfColor(Color.DarkGreen), Banana.Area, Color.White);
				}
			}
		}

		private void DrawDebugText()
		{
			if (GameSettings.Debug)
			{
				if (Banana != null)
				{
					string debugText = $"({Banana.Area.X}, {Banana.Area.Y}, {Banana.Area.Width}, {Banana.Area.Height})";
					Vector2 upperLeft = new Vector2(0, GameSettings.ScreenSize.Height - 20);

					spriteBatch.Draw(Pixel.OfColor(Color.Black), new Rectangle(
						(int)upperLeft.X, (int)upperLeft.Y,
						GameSettings.ScreenSize.Width, 20), Color.White);

					spriteBatch.DrawString(Font, debugText, upperLeft, Color.White);
				}
			}
		}

	}
}
