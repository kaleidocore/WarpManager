namespace KaleidoWarp;

/// <summary>
/// Controls how the transition image is fitted to the screen during the transition.
/// </summary>
public enum ImageFit
{
	/// <summary>
	/// No scaling is applied to the transition image. The image will be drawn centered at its original size, which may result in it being cropped or not filling the entire screen depending on its dimensions and the screen size.
	/// </summary>
	None,

	/// <summary>
	/// The transition image will be scaled to fit within the screen while maintaining its aspect ratio. This may result in letterboxing or pillarboxing if the aspect ratios do not match.
	/// </summary>
	Contain,

	/// <summary>
	/// The transition image will be scaled to cover the entire screen while maintaining its aspect ratio. This may result in some parts of the image being cropped if the aspect ratios do not match.
	/// </summary>
	Cover,

	/// <summary>
	/// The transition image will be stretched to fill the entire screen, ignoring its aspect ratio. This may result in distortion if the aspect ratios do not match.
	/// </summary>
	Stretch
}
