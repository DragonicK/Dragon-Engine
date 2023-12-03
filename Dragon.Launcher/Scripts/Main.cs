using Godot;

namespace Dragon.Launcher;

public partial class Main : PanelContainer {
	private bool isClicked = false;
	private Vector2 startPosition;

	public override void _Ready() {	}
 
	public override void _Process(double delta) {
		if (isClicked) {
			var position = GetGlobalMousePosition() + DisplayServer.WindowGetPosition() - startPosition;

			DisplayServer.WindowSetPosition(new Vector2I((int)position.X, (int)position.Y));	
		}
	}

	private void OnButtonClosePressed() {
		GetTree().Quit();
	}

	private void OnButtonMinimizePressed() {
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Minimized);
	}

	private void OnHeaderGuiInput(InputEvent @event) {
		if (@event is InputEventMouseButton) {
			var e = @event as InputEventMouseButton;

			if (e.IsPressed()) {
				if (e.ButtonIndex == MouseButton.Left) {
					isClicked = true;

					startPosition = GetLocalMousePosition();
				}
			}
			else {
				isClicked = false;
			}
		}
	}
}
