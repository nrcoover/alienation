using Godot;
using System;

public partial class EnemyBase : PathFollow2D
{ 
	[Export] private Timer _laserTimer;
	[Export] private AudioStreamPlayer2D _sound;
	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private HealthBar _healthBar;
	[Export] private HitBox _hitBox;
				
	[Export] private bool _shoots { get; set; } = false;
	[Export] private bool _aimsAtPlayer { get; set; } = false;
	[Export] private Defs.BulletType _bulletType { get; set; } = Defs.BulletType.Enemy;
	[Export] private float _bulletSpeed { get; set; } = 120.0f;
	[Export] private Vector2 _bulletDirection { get; set; } = Vector2.Down;
	[Export] private float _bulletWaitTime { get; set; } = 2.0f;
	[Export] private float _bulletWaitTimeVar { get; set; } = 0.05f;
	[Export] private float _speed = 50.0f;

	private Player _playerRef;

	public override void _Ready()
	{
		_playerRef = GetTree().GetFirstNodeInGroup(Defs.GROUP_PLAYER) as Player;
		
		if (_playerRef == null)
		{
			QueueFree();
			return;
		}

		SubscribeToSignals();
		SpaceUtils.PlayRandomAnimation(_animatedSprite2D);
		StartShootTimer();
	}

	public override void _Process(double delta)
	{
		ProgressOnPath(delta);
	}

	private void SubscribeToSignals() {
		_laserTimer.Timeout += LaserTimerTimeout;
		_healthBar.OnDied += OnHealthBarDepleted;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
	}

	private void ProgressOnPath(double delta)
	{
		Progress += _speed * (float)delta;
		var progressLimit = 0.99f;

		if (ProgressRatio > progressLimit)
		{
			QueueFree();
		}
	}

	private void StartShootTimer()
	{
		SpaceUtils.SetAndStartTimer(_laserTimer, _bulletWaitTime, _bulletWaitTimeVar);
	}

	private void UpdateBulletDirection()
	{
		if (!_aimsAtPlayer || !IsInstanceValid(_playerRef))
		{
			return;
		}

		_bulletDirection = GlobalPosition.DirectionTo(_playerRef.GlobalPosition);
	}

	private void Shoot()
	{
		if (!_shoots)
		{
			return;
		}
	
		UpdateBulletDirection();
		SignalManager.EmitOnCreateBullet(GlobalPosition, _bulletDirection, _bulletSpeed, (int)_bulletType);
		SoundManager.PlayLaserRandom(_sound);
		StartShootTimer();
	}

	private void LaserTimerTimeout()
	{
		Shoot();
	}

	private void OnHealthBarDepleted() 
	{
		QueueFree();
	}

	private void OnHitBoxAreaEntered(Area2D area)
	{
		if(area is BaseBullet)
		{
			_healthBar.TakeDamage((area as BaseBullet).GetDamage());
		}
	}
}

