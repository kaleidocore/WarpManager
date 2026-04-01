namespace KaleidoWarp;

/// <summary>
/// A convenience interface for transition classes to provide static factory methods for creating cover and uncover transitions. This allows for a consistent API across different transition types, enabling users to easily create transitions with default settings and further customize them as needed.
/// </summary>
/// <typeparam name="T">The type of the transition class implementing this interface.</typeparam>
public interface ITransitionFactory<T>
	where T : Transition
{
	/// <summary>
	/// Creates a new <typeparamref name="T"/> instance representing an exit/outro transition.
	/// </summary>
	/// <param name="duration">The duration, in seconds, for which the cover operation should last.
	/// second.</param>
	/// <returns>An instance of type <typeparamref name="T"/> representing the configured cover operation.</returns>
	static abstract T Cover(float duration);

	/// <summary>
	/// Creates a new <typeparamref name="T"/> instance representing an entry/intro transition.
	/// </summary>
	/// <param name="duration">The duration, in seconds, for which the uncover operation should last.</param>
	/// <returns>An instance of type <typeparamref name="T"/> representing the configured uncover operation.</returns>
	static abstract T Uncover(float duration);
}
