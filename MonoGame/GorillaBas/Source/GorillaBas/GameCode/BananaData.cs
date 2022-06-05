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

		public (float X, float Y) TrajectoryFrom(float angleInDegrees, float velocity)
		{
			// SOH CAH TOA
			// Sin(a) = O/H
			// Cos(a) = A/H
			// Tan(a) = O/A

			float hypotenuse = 1.0f;

			// cos(a) = x / h : x = h * cos(a) 
			// sin(a) = y / h : y = h * sin(a).

			var angleInRadians = 2 * Math.PI * ((angleInDegrees + 180) % 360)/ 360.0f;
			var x = (float)Math.Cos(angleInRadians) * hypotenuse;
			var y = (float)Math.Sin(angleInRadians) * hypotenuse;

			return (x * velocity * 0.01f, y * velocity * 0.01f * -1);
		}

		public BananaData((float X, float Y) position, int size, float angleInDegrees, int directionModifier, float velocity, float gravity)
		{
			Size = size;
			Position = position;
			UpdateArea();
			Gravity = gravity;
			Angle = angleInDegrees;
			Velocity = velocity;
			Trajectory = TrajectoryFrom(angleInDegrees, -1 * velocity);
			Trajectory = (Trajectory.X * directionModifier, Trajectory.Y);	// Reverse the direction for player 2.
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
