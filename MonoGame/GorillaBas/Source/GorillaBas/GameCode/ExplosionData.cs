using System;
using GorillaBas.MonoGameHelpers;
using Microsoft.Xna.Framework;

namespace GorillaBas.GameCode
{
	public class ExplosionData
	{
		public bool Active { get; private set; }
		public (int X, int Y) ImpactOffset => impactOffsets[currentImpactOffsetIndex];
		public float Rotation { get; set; }

		private Cooldown cooldown;
		private (int X, int Y)[] impactOffsets;
		private int currentImpactOffsetIndex;
		private Random rnd;

		public ExplosionData()
		{
			rnd = new Random();
			cooldown = new Cooldown();
			int milliseconds = 100;
			cooldown.TotalTime = new TimeSpan(0, 0, 0, 0, milliseconds);

			impactOffsets = new[]
			{
				(-5, -5), (5, 5), (-5, 5), (5, -5), (0, 0),
				(-5, -5), (5, 5), (-5, 5), (5, -5), (0, 0),
				//(-5, -5), (5, 5), (-5, 5), (5, -5), (0, 0),
				//(-5, -5), (5, 5), (-5, 5), (5, -5), (0, 0),
			};
			Reset();
		}

		public void Reset()
		{
			Active = false;
			currentImpactOffsetIndex = 0;
		}

		public void Activate(GameTime gameTime)
		{
			Active = true;
			cooldown.Reset(gameTime);
		}

		public void Update(GameTime gameTime)
		{
			if (cooldown.Expired(gameTime))
			{
				currentImpactOffsetIndex++;
				Rotation = rnd.Next(314 * 2) / 100.0f;
				cooldown.Reset(gameTime);
				if (currentImpactOffsetIndex > impactOffsets.Length - 1)
					Reset();
			}
		}
	}
}
