using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GorillaBas.GameCode
{
	public class LoadedContent
	{
		public List<Building> Buildings { get; set; }
		public Texture2D GorillaImage { get; set; }
		public Texture2D BananaImage { get; set; }
		public Texture2D ExplosionImage { get; set; }
		public Texture2D GuideArrow { get; set; }
		public (GorillaData LeftGorilla, GorillaData RightGorilla) Gorillas { get; set; }
		public SoundEffect ExplosionSound { get; set; }
		public SoundEffect FireSound { get; set; }
		public SoundEffect GorillaSound { get; set; }


		public LoadedContent()
		{
			Buildings = new List<Building>();
		}

	}
}
