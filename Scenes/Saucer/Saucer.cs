using Godot;
using System;

public partial class Saucer : PathFollow2D
{
	const string PLAYBACK_PARAM = "parameters/playback";

	[Export] private AnimationTree _animationTree;
	[Export] private HealthBar _healthBar;
	[Export] private Node2D _booms;
	[Export] private HitBox _hitBox;
	[Export] private Timer _shootTimer;
	[Export] private AudioStreamPlayer2D _sound;

	[Export] private float _speed = 20.0f;
	[Export] private float _boomDelay = 0.1f;
	[Export] private int _score = 150;
	[Export] private float _waitTime = 16.0f;
	[Export] private float _waitTimeVar = 4.0f;


	private bool _shooting = false;
	private AnimationNodeStateMachinePlayback _stateMachinePlayback;


	public override void _Ready()
	{
		_stateMachinePlayback = (AnimationNodeStateMachinePlayback)_animationTree.Get(PLAYBACK_PARAM);
		_healthBar.OnDied += OnHealthBarDied;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
		_shootTimer.Timeout += Shoot;
		ResetTimer();
	}

	public override void _Process(double delta)
	{
		if (!_shooting)
		{
			Progress += _speed * (float)delta;
		}
	}

	private void ResetTimer()
	{
		SpaceUtils.SetAndStartTimer(_shootTimer, _waitTime, _waitTimeVar);
	}

	private async void MakeBooms()
	{
		foreach (Node2D b in _booms.GetChildren())
		{
			SignalManager.EmitOnCreateExplosion(b.GlobalPosition, (int)Defs.ExplosionType.Boom);
			await ToSignal(GetTree().CreateTimer(_boomDelay), "timeout");
		}
	}

	private void StopShooting()
	{
		_shooting = false;
		ResetTimer();
	}

	private void Shoot()
	{
		_shooting = true;
		_stateMachinePlayback.Travel("shoot");
		_sound.Play();
	}

	private void FireMissile()
	{
		SignalManager.EmitOnCreateHomingMissile(GlobalPosition);
	}

	private void OnHealthBarDied()
	{
		_hitBox.Deactivate();
		_healthBar.Hide();
		_healthBar.OnDied -= OnHealthBarDied;
		_shootTimer.Stop();
		SetProcess(false);
		ScoreManager.IncrementScore(_score);

		_stateMachinePlayback.Travel("die");
		MakeBooms();
	}

	private void OnHitBoxAreaEntered(Area2D area)
	{
		_healthBar.TakeDamage((area as HitBox).GetDamage());
	}
}
