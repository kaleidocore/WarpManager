using Godot;
using KaleidoWarp;

public partial class Scene2 : Node2D
{
	static PackedScene MainScene { get; } = GD.Load<PackedScene>("res://Examples/Scene1/Scene1.tscn");

	float _timer;
	Label TimeLabel => GetNode<Label>("%TimeLabel");
	Button BackButton => GetNode<Button>("%BackButton");

	public override void _Ready()
	{
		base._Ready();

		BackButton.Pressed += () =>
		{
			WarpManager.Instance.WarpToPacked(MainScene, ColorFade.Cover(.5f), ColorFade.Uncover(.5f));
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_timer += (float)delta;
		TimeLabel.Text = _timer.ToString("F2");
	}
}
