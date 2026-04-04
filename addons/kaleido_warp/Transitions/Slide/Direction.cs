namespace KaleidoWarp;

/// <summary>
/// Configures the direction of the slide transition effect, determining the direction from which the new scene will slide in or out during the transition.
/// </summary>
public enum Direction
{
	/// <summary>
	/// Slides the scene from or towards the left edge of the screen.
	/// </summary>
	Left,

	/// <summary>
	/// Slides the scene from or towards the right edge of the screen.
	/// </summary>
	Right,

	/// <summary>
	///	Slides the scene from or towards the top edge of the screen.
	///
	Up,

	/// <summary>
	/// Slides the scene from or towards the bottom edge of the screen.
	/// </summary>
	Down
}