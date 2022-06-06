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
				int width = LoadedContent.ExplosionImage.Width;
				int height = LoadedContent.ExplosionImage.Height;

				var destRect = new Rectangle(
						Banana.Area.Left + this.Explosion.ImpactOffset.X,
						Banana.Area.Top + this.Explosion.ImpactOffset.Y,
						LoadedContent.ExplosionImage.Width,
						LoadedContent.ExplosionImage.Height);

				if (GameSettings.Debug)
					spriteBatch.Draw(Pixel.OfColor(Color.DarkRed), destRect, Color.White);

				Rectangle sourceRect = LoadedContent.ExplosionImage.Bounds;
				float rotation = Explosion.Rotation;
				Vector2 origin = new Vector2(width / 2, height / 2);

				spriteBatch.Draw(
					LoadedContent.ExplosionImage,
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

		private void DrawBanana()
		{
			if (GameSettings.Debug)
			{
				foreach ((int X, int Y) previousBanana in previousBananas)
				{
					spriteBatch.Draw(LoadedContent.BananaImage, new Rectangle(previousBanana.X, previousBanana.Y, Banana.Area.Width, Banana.Area.Height), Color.White);
				}
			}
			float rotation = Banana.Rotation;
			spriteBatch.Draw(LoadedContent.BananaImage,
				Banana.Area,
				LoadedContent.BananaImage.Bounds,
				Color.White,
				rotation,
				new Vector2(LoadedContent.BananaImage.Width / 2, LoadedContent.BananaImage.Height / 2),
				SpriteEffects.None,
				0);
		}

		private void NextTurn()
		{
			// Just swap the position of the players in the Players property.
			(var currentPlayer, var nextPlayer) = Players;
			Players = (nextPlayer, currentPlayer);
		}
	}
}
