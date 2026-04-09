using System;
using Godot;

namespace KaleidoWarp.Examples;

/// <summary>
/// This class demonstrates how to create some custom dissolve patterns for use with the <see cref="Dissolve"/> transition. You can use it as a reference for generating your own patterns or as a utility to create new textures.
/// </summary>
internal class PatternGenerator
{
	public static Image WipeH()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		for (int x = 0; x < size; x++)
			img.FillRect(new Rect2I(x, 0, 1, size), new Color(x / 255f, 0, 0));

		return img;
	}

	public static Image WipeV()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		for (int y = 0; y < size; y++)
			img.FillRect(new Rect2I(0, y, size, 1), new Color(y / 255f, 0, 0));

		return img;
	}

	public static Image CurtainsH()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		var col = 0;
		for (int x = 0; x < size / 2; x++)
		{
			img.FillRect(new Rect2I(x, 0, 1, size), new Color(col / 255f, 0, 0));
			col += 2;
		}

		for (int x = size / 2; x < size; x++)
		{
			img.FillRect(new Rect2I(x, 0, 1, size), new Color((size - 1 - x) / (size / 2f - 1f), 0, 0));
			col -= 2;
		}

		return img;
	}

	public static Image CurtainsV()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		var col = 0;
		for (int y = 0; y < size / 2; y++)
		{
			img.FillRect(new Rect2I(0, y, size, 1), new Color(col / 255f, 0, 0));
			col += 2;
		}

		for (int y = size / 2; y < size; y++)
		{
			img.FillRect(new Rect2I(0, y, size, 1), new Color((size - 1 - y) / (size / 2f - 1f), 0, 0));
			col -= 2;
		}

		return img;
	}

	public static Image BlindsH(int count = 4)
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		var val = 0;
		for (int x = 0; x < size; x++)
		{
			img.FillRect(new Rect2I(x, 0, 1, size), new Color((val % 256) / 255f, 0, 0));
			val += count;
		}

		return img;
	}

	public static Image BlindsV(int count = 4)
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		var val = 0;
		for (int y = 0; y < size; y++)
		{
			img.FillRect(new Rect2I(0, y, size, 1), new Color((val % 256) / 255f, 0, 0));
			val += count;
		}

		return img;
	}

	public static Image Circle()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);
		float cornerDist = Mathf.Sqrt(2f) / 2f; // distance from center to corner in normalized space

		for (int y = 0; y < size; y++)
		{
			float dy = y / (size - 1f) - 0.5f;
			for (int x = 0; x < size; x++)
			{
				float dx = x / (size - 1f) - 0.5f;
				float dist = Mathf.Sqrt(dx * dx + dy * dy) / cornerDist; // 0 at center, 1 at corners
				float value = 1f - dist; // white center, black corners
				img.SetPixel(x, y, new Color(Mathf.Clamp(value, 0f, 1f), 0, 0));
			}
		}

		return img;
	}

	public static Image Square()
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);

		for (int y = 0; y < size; y++)
		{
			float dy = Mathf.Abs(y / (size - 1f) - 0.5f) * 2f; // 0 at center, 1 at edge
			for (int x = 0; x < size; x++)
			{
				float dx = Mathf.Abs(x / (size - 1f) - 0.5f) * 2f;
				float value = 1f - Mathf.Max(dx, dy); // white center, black borders
				img.SetPixel(x, y, new Color(value, 0, 0));
			}
		}

		return img;
	}

	public static Image Clock(float startAngle = 0f, int count = 1)
	{
		const int size = 256;
		var img = Image.CreateEmpty(size, size, false, Image.Format.L8);
		float offset = -startAngle * Mathf.Pi / 180f;
		for (int y = 0; y < size; y++)
		{
			float dy = y / (size - 1f) - 0.5f;
			for (int x = 0; x < size; x++)
			{
				float dx = x / (size - 1f) - 0.5f;
				float angle = Mathf.Atan2(dy, dx);
				float value = (angle + Mathf.Pi / 2f + offset) / (Mathf.Pi * 2f);
				value -= Mathf.Floor(value);
				value = (value * count) % 1f;
				img.SetPixel(x, y, new Color(value, 0, 0));
			}
		}
		return img;
	}

	public static Image PixelNoise(int width, int height, int pixelSize)
	{
		int pixelsX = width / pixelSize;
		int pixelsY = height / pixelSize;
		int pixelCount = pixelsX * pixelsY;

		// Fill with perfectly uniform values then shuffle
		var values = new float[pixelCount];
		for (int i = 0; i < pixelCount; i++)
			values[i] = i / (pixelCount - 1f);

		var rng = new Random();
		for (int i = pixelCount - 1; i > 0; i--)
		{
			int j = rng.Next(i + 1);
			(values[i], values[j]) = (values[j], values[i]);
		}

		var img = Image.CreateEmpty(width, height, false, Image.Format.L8);

		for (int by = 0; by < pixelsY; by++)
		{
			for (int bx = 0; bx < pixelsX; bx++)
			{
				float value = values[by * pixelsX + bx];
				var color = new Color(value, 0, 0);

				img.FillRect(new Rect2I(bx * pixelSize, by * pixelSize, pixelSize, pixelSize), color);
			}
		}

		return img;
	}
}