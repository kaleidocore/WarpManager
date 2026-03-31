using Godot;
using KaleidoWarp;

public partial class Scene1 : Node2D
{
	static PackedScene TargetScene { get; } = GD.Load<PackedScene>("res://Examples/Scene2/Scene2.tscn");
	static PackedScene LoaderScene { get; } = GD.Load<PackedScene>("res://Examples/Loader/Loader.tscn");
	static Color OverlayColor { get; set; } = Colors.Black;
	static bool UseImage { get; set; } = false;

	float _timer;
	Label TimeLabel => GetNode<Label>("%TimeLabel");
	Button FadeButton => GetNode<Button>("%FadeButton");
	Button DissolveButton => GetNode<Button>("%DissolveButton");
	Button VoronoiButton => GetNode<Button>("%VoronoiButton");
	Button SuperMarioButton => GetNode<Button>("%SuperMarioButton");
	Button LoaderButton => GetNode<Button>("%LoaderButton");
	ColorPickerButton ColorPicker => GetNode<ColorPickerButton>("%ColorPicker");
	CheckButton UseImageButton => GetNode<CheckButton>("%UseImage");
	Texture2D OverlayImage => UseImage ? GetNode<Sprite2D>("%OverlayImage").Texture : Transition.TransparentPixel;

	public override void _Ready()
	{
		base._Ready();

		ColorPicker.Color = OverlayColor;
		ColorPicker.ColorChanged += (c) => OverlayColor = c;
		UseImageButton.ButtonPressed = UseImage;
		UseImageButton.Toggled += (v) => UseImage = v;

		FadeButton.Pressed += () => WarpManager.Instance.WarpToPacked(TargetScene, ColorFade.Cover().Color(OverlayColor).Image(OverlayImage), ColorFade.Uncover().Color(OverlayColor).Image(OverlayImage));
		DissolveButton.Pressed += () => WarpManager.Instance.WarpToPacked(TargetScene, Dissolve.Cover().Color(OverlayColor).Image(OverlayImage), Dissolve.Uncover().Color(OverlayColor).Image(OverlayImage));
		VoronoiButton.Pressed += () => WarpManager.Instance.WarpToPacked(TargetScene, Voronoi.Cover().Color(OverlayColor).Image(OverlayImage), Voronoi.Uncover().Color(OverlayColor).Image(OverlayImage));
		SuperMarioButton.Pressed += () => WarpManager.Instance.WarpToPacked(TargetScene, SuperMario.Cover().Color(OverlayColor).Image(OverlayImage), SuperMario.Uncover().Color(OverlayColor).Image(OverlayImage));
		LoaderButton.Pressed += () => WarpManager.Instance.WarpToPacked(LoaderScene, Dissolve.Cover(1f).Color(Colors.Black).Pattern(p => p.TileReveal), null);
	}

	public override void _Process(double delta)
	{
		_timer += (float)delta;
		TimeLabel.Text = _timer.ToString("F2");
	}
}
