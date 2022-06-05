using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GorillaBas.GameCode
{
	public class BananaData
	{
		public (float X, float Y) Position { get; set; }
		public Rectangle Area { get; set; }
		public (float X, float Y) Trajectory { get; set; }
		public float Gravity { get; set; }
		public float Angle { get; }
		public float Velocity { get; }
		public int Size { get; set; }
		public float Rotation { get; set; }


		public (float X, float Y) TrajectoryFrom(float angle, float velocity)
		{
			// SOH CAH TOA
			// Sin(a) = O/H
			// Cos(a) = A/H
			// Tan(a) = O/A

			var xdistance = 1.0f;
			var ratio = Math.Tan(angle);
			float ydistance = Convert.ToSingle(ratio / xdistance);  // ydistance is technically the same as ratio because xdistance is 1.  Just showing my work.

			//float scale = 1000;
			//return (xdistance * scale, ydistance * scale);
			//return (xdistance * velocity, ydistance * velocity);
			return (xdistance, -1 * ydistance);
		}

		//public BananaData((float X, float Y) position, int size, (float X, float Y) trajectory, float gravity)
		//{
		//	Position = position;
		//	Area = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size, size);
		//	Gravity = gravity;
		//	Trajectory = trajectory;
		//}

		public BananaData((float X, float Y) position, int size, float angle, float velocity, float gravity)
		{
			Size = size;
			Position = position;
			UpdateArea();
			Gravity = gravity;
			Angle = angle;
			Velocity = velocity;
			//Trajectory = trajectory;
			Trajectory = TrajectoryFrom(angle, -1 * velocity);
		}


		public void ApplyGravity()
		{
			// Gravity applies a downward force.
			// That means we apply it to the Y (vertical) part of the trajectory.
			// Trajectory and gravity are both based on meters per second.
			// So we can just subtract gravity from the Y velocity.
			(var x, var y) = Trajectory;
			y = y + Gravity * 0.001f;
			Trajectory = (x, y);
			Position = (Position.X + Trajectory.X, Position.Y + Trajectory.Y);
			UpdateArea();

			// This is a convenient place to change the rotation.
			Rotation = Rotation + (Single)((2 * Math.PI) / 360.0f * 25.0f);	// (twenty five degrees)
		}
		private void UpdateArea()
		{
			Area = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Size, Size);
		}
	}
}
