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
    [Export] private Shield _shield;

    private Vector2 _upperLeft;
    private Vector2 _lowerRight;

	public override void _Ready()
	{
        AreaEntered += OnAreaEntered;
        SetLimits();
	}

    public override void _Process(double delta)
	{
        UpdatePosition(delta);

        if (Input.IsActionJustPressed("shoot"))
        {
            Shoot();
        }
	}

    private Vector2 GetInput()
    {
        Vector2 directionVector = new Vector2(
            Input.GetAxis("left", "right"),
            Input.GetAxis("up", "down")
        );

        HandleMovementAnimation(directionVector);

        GD.Print($"v.x: {directionVector.X}, v.y: {directionVector.Y}, v.Length(): {directionVector.Length()}, v.Normalized(): {directionVector.Normalized()}");

        return directionVector.Normalized();
    }

    private void SetLimits()
    {
        var viewport = GetViewportRect();

        _lowerRight = new Vector2(viewport.Size.X - MARGIN, viewport.Size.Y - MARGIN);

        _upperLeft = new Vector2(MARGIN, MARGIN);
    }

    private void HandleMovementAnimation(Vector2 direction) {

        var noMovement = 0;

        if (direction.X == noMovement) {
            _animationPlayer.Play("fly");
            return;
        }

        if (direction.X > noMovement) {
            _sprite2D.FlipH = true;

        } else if (direction.X < noMovement) {
            _sprite2D.FlipH = false;
        }

        _animationPlayer.Play("turn");
    }

    private void UpdatePosition(double delta) {
        var input = GetInput();

        Vector2 desiredPosition = GlobalPosition + input * (float)delta * _speed;

        GlobalPosition = desiredPosition.Clamp(_upperLeft, _lowerRight);
    }

    private void Shoot()
    {
        SignalManager.EmitOnCreateBullet(GlobalPosition, _bulletDirection, _bulletSpeed, (int)Defs.BulletType.Player);
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is PowerUp) {
            var powerUp = area as PowerUp;

            if (powerUp.GetPowerUpType() == Defs.PowerUpType.Health)
            {
                SignalManager.EmitOnPlayerHealthBonus(_healthBoost);
                GD.Print($"Player::Health boost! {_healthBoost}");
            }
            else if (powerUp.GetPowerUpType() == Defs.PowerUpType.Shield)
            {
                _shield.EnableShield();
            }
        }
        else if(area is HitBox)
        {
            var hitBox = area as HitBox;
            SignalManager.EmitOnPlayerHit(hitBox.GetDamage());
            GD.Print($"Player::Taking Damage! {hitBox.GetDamage()}");
        }
    }
}
