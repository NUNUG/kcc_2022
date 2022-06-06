using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorillaBas.GameCode
{
	public class GameSettings
	{
		public bool Debug => false;
		public bool FullScreen => false;
		public (int Width, int Height) ScreenSize => (800, 600);
		public (int MinWidth, int MaxWidth, int MinHeight, int MaxHeight) BuildingLimits => (55, 75, 200, 400);
		public int InitialAngle => 45;
		public float InitialVelocity => 200;
		public float Gravity => 9.8f;
		public int GorillaSize => 64;
		public int BananaSize => 24;
		public int MaxScore => 10;
		public int GuideArrowSize => 15;
	}
}
