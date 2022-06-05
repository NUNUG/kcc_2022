using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GorillaBas.GameCode
{
	public class GorillaData
	{
		public string Name { get; set; }
		public Rectangle Area { get; set; }
		public float Angle { get; set; }
		public float Velocity { get; set; }
		public int Score { get; set; }

		public GorillaData()
		{
		}

		public GorillaData(string name, Rectangle area, float angle, float velocity)
		{
			Name = name;
			Area = area;
			Angle = angle;
			Velocity = velocity;
		}
	}
}
