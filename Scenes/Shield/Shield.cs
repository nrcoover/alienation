using Godot;
using System;

public partial class Shield : Area2D
{
	[Export] private int _startHealth = 5;

	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AudioStreamPlayer2D _sound;
	[Export] private CollisionShape2D _collisionShape2D;
	[Export] private Timer _timer;

	private int _health;
	private bool _isDestroyed => _health <= 0;

	public override void _Ready()
	{
		SubscribeToSignals();

		DisableShield();
	}

	private void SubscribeToSignals() {
		_timer.Timeout += OnTimerTimeout;
		AreaEntered += OnAreaEntered;
	}

	public void EnableShield()
	{
		_animationPlayer.Play("RESET");
		Show();
		SetHealth(_startHealth);
		_collisionShape2D.CallDeferred(CollisionShape2D.MethodName.SetDisabled, false);
		_timer.Start();
		SoundManager.PlayPowerUpSound(_sound, Defs.PowerUpType.Shield);
	}

	private void DisableShield()
	{
		Hide();
		_collisionShape2D.CallDeferred(CollisionShape2D.MethodName.SetDisabled, true);
		_timer.Stop();
	}

	private void Hit()
	{
		_animationPlayer.Play("hit");
		DecrementHealth();
		
		if (_isDestroyed) {
			DisableShield();
		}
	}

	private void OnAreaEntered(Area2D area)
	{
		Hit();
	}

	private void OnTimerTimeout()
	{
		DisableShield();
	}

	private void DecrementHealth()
	{
		var hitPoint = 1;
		SetHealth(_health -= hitPoint);
	}

	private void SetHealth(int value)
	{
		_health = value;
	}
}
