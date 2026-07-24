using Godot;
using System;

public partial class Player : Area2D
{

    private const float MARGIN = 16.0f;

    [Export] private float _speed = 250.0f;
    [Export] private float _bulletSpeed = 250.0f;
    [Export] private Vector2 _bulletDirection = Vector2.Up;
    [Export] private int _healthBoost = 20;

    [Export] private Sprite2D _sprite2D;
    [Export] private AnimationPlayer _animationPlayer;

    private Vector2 _upperLeft;
    private Vector2 _lowerRight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AreaEntered += OnAreaEntered;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    private Vector2 GetInput()
    {
        Vector2 v = Vector2.Zero;
        return v.Normalized();
    }

    private void SetLimits()
    {
        var vp = GetViewportRect();
        _lowerRight = new Vector2(vp.Size.X - MARGIN, vp.Size.Y - MARGIN);
        _upperLeft = new Vector2(MARGIN, MARGIN);
    }

    private void OnAreaEntered(Area2D area)
    {
    }
}
