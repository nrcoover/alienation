using Godot;
using System;

public partial class HomingMissile : HitBox
{
	[Export] private float _rotationSpeed = 1.2f;
	[Export] private float _speed = 60.0f;
	[Export] private int _score = 5;

	private Player _playerRef;

	public override void _Ready()
	{
		base._Ready();

		_playerRef = GetTree().GetFirstNodeInGroup(Defs.GROUP_PLAYER) as Player;

		if(_playerRef == null)
		{
			QueueFree();
		}
	}

	public override void _Process(double delta)
	{
		Turn((float)delta);
	}

	private void Turn(float delta)
	{
		var directionToPlayer = GlobalPosition.DirectionTo(_playerRef.GlobalPosition);
		var angleToPlayer = Transform.X.AngleTo(directionToPlayer);
		var amountWeCanRotate = _rotationSpeed * delta;
		var angleWeWillTurn = Math.Min(Mathf.Abs(angleToPlayer), amountWeCanRotate);

		Rotate(angleWeWillTurn * Math.Sign(angleToPlayer));
		Position += Transform.X * _speed * delta;
	}

	private void BlowUp()
	{
		SignalManager.EmitOnCreateExplosion(GlobalPosition, (int)Defs.ExplosionType.Boom);
		ScoreManager.IncrementScore(_score);
		SetProcess(false);
		QueueFree();
	}

	protected override void OnAreaEntered(Area2D area)
	{
		BlowUp();
	}
}
