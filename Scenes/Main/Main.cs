using Godot;
using System;

public partial class Main : Control
{
	[Export] private UiButton _playButton;
	[Export] private UiButton _quitButton;

	public override void _Ready()
	{        
		GetTree().Paused = false;
		SubscribeToSignals();
	}

	private void SubscribeToSignals() {
		_playButton.Pressed += () => { GameManager.LoadGameScene(); };
		_quitButton.Pressed += () => { GetTree().Quit(); };
	}
}
