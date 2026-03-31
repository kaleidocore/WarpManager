using Godot;
using KaleidoWarp;

public partial class Loader : CanvasLayer
{
	static PackedScene TargetScene { get; } = GD.Load<PackedScene>("res://Examples/Scene2/Scene2.tscn");
	bool _loaded;

	ProgressBar ProgressBar => GetNode<ProgressBar>("%ProgressBar");
	AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer>("%AnimationPlayer");

	[Export]
	public float Progress { get; set; }

	public override void _Ready()
	{
		base._Ready();

		CreateTween().TweenProperty(this, nameof(Progress), 100f, 2f); // Simulate loading progress
		AnimationPlayer.Play("Enter");
		AnimationPlayer.AnimationFinished += anim =>
		{
			if (anim == "Exit")
				WarpManager.Instance.WarpToPacked(TargetScene, null, Voronoi.Uncover().Angle(270).Ease(Tween.EaseType.Out));
		};
	}

	public override void _Process(double delta)
	{
		ProgressBar.Value = Progress;

		if (Progress >= 100 && !_loaded)
		{
			_loaded = true;
			AnimationPlayer.Play("Exit");
		}
	}
}
