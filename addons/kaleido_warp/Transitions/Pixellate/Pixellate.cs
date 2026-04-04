using Godot;
namespace KaleidoWarp;
#nullable enable

public partial class Pixellate : Transition, ITransitionFactory<Pixellate>
{
	const string ShaderPath = "pixellate.gdshader";

	/// <summary>
	/// The amount (ratio) of pixelation to apply during the transition. Higher values will result in larger, more pronounced pixels, while lower values will create a finer pixelation effect.
	/// </summary>
	[Export]
	public float Amount { get; set; } = 100f;

	/// <summary>
	/// The origin point for the pixelation effect, specified as a normalized vector where (0, 0) represents the top-left corner of the screen and (1, 1) represents the bottom-right corner.
	/// </summary>
	[Export]
	public Vector2 Origin { get; set; } = new(0.5f, 0.5f);

	public override void _Ready()
	{
		base._Ready();
		LoadShader(ShaderPath);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		var material = (ShaderMaterial)Material;
		material.SetShaderParameter("progress", Reverse ? 1f - Progress : Progress);
		material.SetShaderParameter("image", ImageTexture ?? TransparentPixel);
		material.SetShaderParameter("image_fit", (int)ImageFitMode);
		material.SetShaderParameter("amount", Amount);
		material.SetShaderParameter("origin", Origin);
	}

	public static Pixellate Cover(float duration = 2f)
	{
		return new Pixellate()
		{
			Duration = duration,
			Reverse = false,
			Ease = Tween.EaseType.InOut,
		};
	}

	public static Pixellate Uncover(float duration = 2f)
	{
		return new Pixellate()
		{
			Duration = duration,
			Reverse = true,
			Ease = Tween.EaseType.Out,
		};
	}
}
