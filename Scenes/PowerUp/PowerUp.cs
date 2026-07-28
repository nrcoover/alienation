using Godot;
using System;
using System.Collections.Generic;

public partial class PowerUp : HitBox
{
	private static readonly Dictionary<Defs.PowerUpType, Texture2D> PowerUpTextures = new Dictionary<Defs.PowerUpType, Texture2D>
	{
		{ Defs.PowerUpType.Health, GD.Load<Texture2D>("res://assets/misc/powerupGreen_bolt.png") },
		{ Defs.PowerUpType.Shield, GD.Load<Texture2D>("res://assets/misc/shield_gold.png") }
	};

	[Export] float _speed = 100;
	[Export] private Sprite2D _sprite;
	[Export] private AudioStreamPlayer2D _sound;

	private Defs.PowerUpType _powerUpType = Defs.PowerUpType.Health;
    
	public override void _Ready()
	{
		base._Ready();
		_sprite.Texture = PowerUpTextures[_powerUpType];
		SoundManager.PlayPowerUpDeploySound(_sound);
	}

	public override void _Process(double delta)
	{
		Position += new Vector2(0, _speed * (float)delta);
	}

	protected override void OnAreaEntered(Area2D area)
	{   
		GD.Print("PowerUp collected");  
		QueueFree();
	}

	public void Setup(Defs.PowerUpType type)
	{
		_powerUpType = type;
	}
}