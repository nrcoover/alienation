using Godot;
using System;

public partial class ObjectMaker : Node2D
{
    private PackedScene _playerBulletScene = GD.Load<PackedScene>("res://Scenes/Bullets/PlayerBullet.tscn");
    private PackedScene _powerUpScene = GD.Load<PackedScene>("res://Scenes/PowerUp/PowerUp.tscn");
    private PackedScene _explosion = GD.Load<PackedScene>("res://Scenes/Explosion/Explosion.tscn");
    private PackedScene _enemyBulletScene = GD.Load<PackedScene>("res://Scenes/Bullets/EnemyBullet.tscn");
    private PackedScene _enemyBombScene = GD.Load<PackedScene>("res://Scenes/Bullets/EnemyBomb.tscn");
    private PackedScene _homingMissileScene = GD.Load<PackedScene>("res://Scenes/HomingMissile/HomingMissile.tscn");
    
    public override void _Ready() 
    { 
        SubscribeToSignals();
    }

    public override void _ExitTree()
    {
        UnsubscribeFromSignals();
    }

    private void SubscribeToSignals()
    {
        SignalManager.Instance.OnCreateExplosion += OnCreateExplosion;
        SignalManager.Instance.OnCreateHomingMissile += OnCreateHomingMissile;
        SignalManager.Instance.OnCreatePowerUp += OnCreatePowerUp;
        SignalManager.Instance.OnCreateRandomPowerUp += OnCreateRandomPowerUp;
        SignalManager.Instance.OnCreateBullet += OnCreateBullet;
    }

    private void UnsubscribeFromSignals()
    {
        SignalManager.Instance.OnCreateExplosion -= OnCreateExplosion;
        SignalManager.Instance.OnCreateHomingMissile -= OnCreateHomingMissile;
        SignalManager.Instance.OnCreatePowerUp -= OnCreatePowerUp;
        SignalManager.Instance.OnCreateBullet -= OnCreateBullet;
        SignalManager.Instance.OnCreateRandomPowerUp -= OnCreateRandomPowerUp;
    }

    private PackedScene GetBulletScene(int type)
    {
        Defs.BulletType bulletType = (Defs.BulletType)type;

        switch (bulletType)
        {
            case Defs.BulletType.Enemy:
                return _enemyBulletScene;
            case Defs.BulletType.Player:
                return _playerBulletScene;
            case Defs.BulletType.EnemyBomb:
                return _enemyBombScene;
            default:
                return _playerBulletScene;
        }
    }

    private void AddObject(Node2D node, Vector2 globalPosition)
    {
        node.GlobalPosition = globalPosition;
        AddChild(node);
    }

    private void OnCreateBullet(Vector2 startPos, Vector2 direction, float speed, int type)
    {
        var newScene = GetBulletScene(type).Instantiate<BaseBullet>();
        newScene.Setup(direction, speed);
        CallDeferred(MethodName.AddObject, newScene, startPos);
    }

    private void OnCreateRandomPowerUp(Vector2 startPos)
    {
        Defs.PowerUpType powerUpType = SpaceUtils.GetRandomEnumValue<Defs.PowerUpType>();
        OnCreatePowerUp(startPos, (int)powerUpType);
    }

    private void OnCreatePowerUp(Vector2 startPos, int puType)
    {
        var newScene = _powerUpScene.Instantiate<PowerUp>();
        newScene.Setup((Defs.PowerUpType)puType);
        CallDeferred(MethodName.AddObject, newScene, startPos);      
    }

    private void OnCreateHomingMissile(Vector2 startPos)
    {
        var newScene = _homingMissileScene.Instantiate<HomingMissile>();
        CallDeferred(MethodName.AddObject, newScene, startPos);
    }

    private void OnCreateExplosion(Vector2 startPos, int explosionType)
    {
        var newScene = _explosion.Instantiate<Explosion>();
        newScene.Setup((Defs.ExplosionType)explosionType);
        CallDeferred(MethodName.AddObject, newScene, startPos);
    }
}
