using Godot;
using System;

public partial class Level : Node2D
{
	public override void _Ready()
	{
		GetTree().Paused = false;
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("test"))
		{
			// SignalManager.EmitOnCreatePowerUp(new Vector2(100, 100), (int)Defs.PowerUpType.Shield);
			// SignalManager.EmitOnCreatePowerUp(new Vector2(100, 100), (int)Defs.PowerUpType.Health);
			SignalManager.EmitOnCreateRandomPowerUp(new Vector2(100, 100));
		}

		if(Input.IsActionJustPressed("quit"))
		{
			GameManager.LoadMainScene();
		}
	}
}
