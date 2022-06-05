using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GorillaBas.GameCode
{
	public class Pixel
	{
		public Texture2D Value { get; private set; }

		private GraphicsDevice GraphicsDevice;
		private Color color;

		public Color Color
		{
			get => color;
			set
			{
				if (color != value)
				{
					color = value;
					Value = PixelFromColor(GraphicsDevice, value);
				}
			}
		}

		public Pixel(GraphicsDevice graphicsDevice) : this(graphicsDevice, Color.Black)
		{
		}

		public Pixel(GraphicsDevice graphicsDevice, Color color)
		{
			this.GraphicsDevice = graphicsDevice;
			Color = color;
		}

		public Texture2D OfColor(Color pixelColor)
		{
			return PixelFromColor(GraphicsDevice, pixelColor);
		}

		private static Texture2D PixelFromColor(GraphicsDevice graphicsDevice, Color color)
		{
			var result = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
			result.SetData<Color>(new[] { color });
			return result;
		}
	}
}
