#if TOOLS
using Godot;

namespace KaleidoWarp;

[Tool]
public partial class WarpPlugin : EditorPlugin
{
	public override void _EnterTree() => AddAutoloadSingleton("WarpManager", "WarpManager/WarpManager.tscn");
	public override void _ExitTree() => RemoveAutoloadSingleton("WarpManager");
}
#endif
