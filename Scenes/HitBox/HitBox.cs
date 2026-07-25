
using Godot;
using System;

[GlobalClass]
public partial class HitBox : Area2D
{
	[Export] protected int _damage = 10;
	[Export] private CollisionShape2D collisionShape2D;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	public int GetDamage()
	{
		return _damage;
	}

	public void Deactivate()
	{
		collisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	protected virtual void OnAreaEntered(Area2D area)
	{
	}
}
