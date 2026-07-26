using Godot;
using System;

public partial class BaseBullet : HitBox
{
	[Export] private VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;

	private Vector2 _direction = Vector2.Up;
	private float _speed = 100.0f;

	public override void _Ready()
	{
		base._Ready();
		SubscribeToSignals();
	}

	public override void _Process(double delta)
	{
		Position += _direction * _speed * (float)delta;
	}

	private void SubscribeToSignals()
	{
		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
	}

	public void Setup(Vector2 direction, float speed)
	{
		_direction = direction;
		_speed = speed;
	}

	public void BlowUp()
	{
		SetProcess(false);
		QueueFree();
	}

	protected override void OnAreaEntered(Area2D area)
	{
		BlowUp();
	}

	private void OnScreenExited()
	{
		QueueFree();
	}
}
