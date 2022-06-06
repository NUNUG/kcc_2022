using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GorillaBas.GameCode;
using GorillaBas.MonoGameHelpers;
using Microsoft.Xna.Framework.Audio;

namespace GorillaBas
{
	public partial class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private GameSettings GameSettings;
		private GameFunctions GameFunctions;
		private LoadedContent LoadedContent;
		private BuildingPainter BuildingPainter;
		private SpriteFont Font;
		private Pixel Pixel;

		private GorillaData Player1;
		private GorillaData Player2;
		private (GorillaData CurrentPlayer, GorillaData NextPlayer) Players;

		private float Gravity = 9.8f; // meters per second squared.
		private ExplosionData Explosion;
		private BananaData Banana;
		private List<(int X, int Y)> previousBananas;

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Pixel = new Pixel(GraphicsDevice);

			Font = Content.Load<SpriteFont>("fonts/Monoisome-Regular");
			LoadedContent = new LoadedContent();
			LoadedContent.ExplosionImage = Content.Load<Texture2D>("sprites/explosion");
			LoadedContent.GorillaImage = Content.Load<Texture2D>("sprites/gorilla");
			LoadedContent.BananaImage = Content.Load<Texture2D>("sprites/bigbanana");
			LoadedContent.GuideArrow = Content.Load<Texture2D>("sprites/guidearrow");
			LoadedContent.Gorillas = GameFunctions.CreateGorillas(GameSettings);
			LoadedContent.ExplosionSound = Content.Load<SoundEffect>("sounds/explosion");
			LoadedContent.FireSound = Content.Load<SoundEffect>("sounds/fire");
			LoadedContent.GorillaSound = Content.Load<SoundEffect>("sounds/gorilla");
			Player1 = LoadedContent.Gorillas.LeftGorilla;
			Player2 = LoadedContent.Gorillas.RightGorilla;
			Players = (Player1, Player2);

			NewPlayfield();
		}

		protected override void Update(GameTime gameTime)
		{
			// Close if ESC is pressed.
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
