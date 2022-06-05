using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GorillaBas.GameCode;
using GorillaBas.MonoGameHelpers;

namespace GorillaBas
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		//private (int Width, int Height) BaseScreenDimensions;
		//private int ScreenScale = 8;
		//private (int Width, int Height) ScreenDimensions;
		//private bool IsFullScreen = false;

		// Builders and content
		//private GameContent GameContent;
		//private GameStorage GameStorage;
		private GameSettings GameSettings;
		private GameFunctions GameFunctions;
		private LoadedContent LoadedContent;
		private BuildingPainter BuildingPainter;
		private SpriteFont Font;
		private Pixel Pixel;
		private Random rnd;

		private GorillaData Player1;
		private GorillaData Player2;
		private (GorillaData CurrentPlayer, GorillaData NextPlayer) Players;

		private float Gravity = 9.8f; // meters per second squared.
		private bool Firing = false;
		private ExplosionData Explosion;
		private BananaData Banana;
		private List<(int X, int Y)> previousBananas;

		private KeyboardState oldKbdState;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;

			GameSettings = new GameSettings();
			GameFunctions = new GameFunctions();
			Explosion = new ExplosionData();
			previousBananas = new List<(int X, int Y)>();
			rnd = new Random();

			Firing = false;
		}

		protected override void Initialize()
		{
			// Set the screen resolution.  I should be able to do this in the constructor, but had to move it to the Initialize() method due to a bug in Monogame 3.8.0.  It should be fixed in 3.8.1.
			graphics.PreferredBackBufferWidth = GameSettings.ScreenSize.Width;
			graphics.PreferredBackBufferHeight = GameSettings.ScreenSize.Height;
			graphics.IsFullScreen = GameSettings.FullScreen;
			graphics.ApplyChanges();

			oldKbdState = Keyboard.GetState();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Font = Content.Load<SpriteFont>("fonts/Monoisome-Regular");
			Pixel = new Pixel(GraphicsDevice);

			LoadedContent = new LoadedContent();
			LoadedContent.SplosionImage = Content.Load<Texture2D>("sprites/splosion");
			LoadedContent.GorillaImage = Content.Load<Texture2D>("sprites/gorilla");
			LoadedContent.BananaImage = Content.Load<Texture2D>("sprites/bigbanana");
			LoadedContent.Buildings = GameFunctions.GenerateBuildings(GameSettings);
			LoadedContent.Gorillas = GameFunctions.CreateGorillas(GameSettings, LoadedContent.Buildings);
			Player1 = LoadedContent.Gorillas.LeftGorilla;
			Player2 = LoadedContent.Gorillas.RightGorilla;
			Players = (Player1, Player2);

			BuildingPainter = new BuildingPainter(GameSettings, LoadedContent, GraphicsDevice, spriteBatch);
		}

		protected override void Update(GameTime gameTime)
		{
			// Close if ESC is pressed.
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (!Firing && !Explosion.Active)
			{
				var inputState = GameFunctions.ReadInput();

				if (inputState.LeftArrowHeld) Players.CurrentPlayer.Angle -= 1;
				if (inputState.RightArrowHeld) Players.CurrentPlayer.Angle += 1;
				if (inputState.UpArrowHeld) Players.CurrentPlayer.Velocity += 1;
				if (inputState.DownArrowHeld) Players.CurrentPlayer.Velocity -= 1;

				if (inputState.SpacePressed)
					FireBanana();
			}
			else if (Firing && !Explosion.Active)
			{
				Banana.ApplyGravity();

				// If the banana goes out of bounds, end the turn.  Don't let it fall for eternity!
				if (Banana.Area.Top > GameSettings.ScreenSize.Height)
				{
					Firing = false;
					NextTurn();
				}

				if (previousBananas.Count < 10000)
					previousBananas.Add(((int)Banana.Position.X, (int)Banana.Position.Y));

				CheckForImpact(gameTime);
			}
			if (Explosion.Active)
			{
				Explosion.Update(gameTime);
			}

			base.Update(gameTime);
		}


		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			BuildingPainter.Draw();
			DrawGorillas();

			if (Firing)
				DrawBanana();

			if (Explosion.Active)
			{
				DrawExplosion();
			}

			DrawText();
			DrawDebugText();
			spriteBatch.End();

			base.Draw(gameTime);
		}

		private void DrawGorillas()
		{
			if (GameSettings.Debug)
			{
				spriteBatch.Draw(Pixel.OfColor(Color.BlueViolet), LoadedContent.Gorillas.LeftGorilla.Area, Color.White);
				spriteBatch.Draw(Pixel.OfColor(Color.BlueViolet), LoadedContent.Gorillas.RightGorilla.Area, Color.White);
			}

			spriteBatch.Draw(LoadedContent.GorillaImage, LoadedContent.Gorillas.LeftGorilla.Area, Color.White);
			spriteBatch.Draw(LoadedContent.GorillaImage, LoadedContent.Gorillas.RightGorilla.Area, Color.White);
		}

		private void NextTurn()
		{
			// Just swap the position of the players in the Players property.
			(var currentPlayer, var nextPlayer) = Players;
			Players = (nextPlayer, currentPlayer);
		}

		private void FireBanana()
		{
			previousBananas.Clear();
			Firing = true;
			(int X, int y) position = (Players.CurrentPlayer.Area.Center.X, Players.CurrentPlayer.Area.Top);
			//(float X, float Y) trajectory = (1.0f, 1.0f);
			Banana = new BananaData(
				position,
				GameSettings.BananaSize,
				Players.CurrentPlayer.Angle,
				Players.CurrentPlayer.DirectionModifier,
				Players.CurrentPlayer.Velocity,
				Gravity
			);
		}

		private void CheckForImpact(GameTime gameTime)
		{
			// Did the banana touch the opposing gorilla?
			bool isOverlappedWithGorilla = GameFunctions.DoRectanglesOverlap(Banana.Area, Players.NextPlayer.Area);
			bool isOverlappedWithBuilding = LoadedContent.Buildings.Any(
				building => GameFunctions.DoRectanglesOverlap(Banana.Area, building.Area));

			bool isOverlapped = isOverlappedWithGorilla || isOverlappedWithBuilding;
			if (isOverlapped)
			{
				Firing = false;
				Explosion.Activate(gameTime);

				if (isOverlappedWithGorilla)
				{
					Players.CurrentPlayer.Score++;
					if (Players.CurrentPlayer.Score == GameSettings.MaxScore)
					{
						// GAME OVER!
					}
				}

				NextTurn();
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

		private void DrawText()
		{
			// Need to output these elements:
			// - Score
			// - Angle
			// - Velocity

			// Player1 text
			float rowHeight = 25.0f;
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

			if (Players.CurrentPlayer == Player1)
			{
				spriteBatch.DrawString(Font, $"{Player1.Name}", player1NameVector, Color.White);
				spriteBatch.DrawString(Font, $"Score: {Player1.Score}", player1ScoreVector, Color.White);
				spriteBatch.DrawString(Font, $"Angle: {Player1.Angle}", player1AngleVector, Color.White);
				spriteBatch.DrawString(Font, $"Velocity: {Player1.Velocity}", player1VelocityVector, Color.White);
			}
			else
			{
				spriteBatch.DrawString(Font, $"{Player2.Name}", player2NameVector, Color.White);
				spriteBatch.DrawString(Font, $"Score: {Player2.Score}", player2ScoreVector, Color.White);
				spriteBatch.DrawString(Font, $"Angle: {Player2.Angle}", player2AngleVector, Color.White);
				spriteBatch.DrawString(Font, $"Velocity: {Player2.Velocity}", player2VelocityVector, Color.White);
			}
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
				//float rotation = 0.0f;
				//float rotation = rnd.Next(314 * 2) / 100.0f; // Random number between 0 and 2pi radians, which is between 0 and 359 degrees.
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

