using Godot;
using System;

public partial class SignalManager : Node
{
    [Signal] public delegate void OnPlayerHitEventHandler(int v);
    [Signal] public delegate void OnPlayerHealthBonusEventHandler(int v);
    [Signal] public delegate void OnPlayerDiedEventHandler();
    [Signal] public delegate void OnScoreUpdatedEventHandler(int v);

    [Signal] public delegate void OnCreateHomingMissileEventHandler(Vector2 startPos);
    [Signal] public delegate void OnCreateExplosionEventHandler(Vector2 startPos, int explosionType);
    [Signal] public delegate void OnCreatePowerUpEventHandler(Vector2 startPos, int puType);
    [Signal] public delegate void OnCreateRandomPowerUpEventHandler(Vector2 startPos);
    [Signal] public delegate void OnCreateBulletEventHandler(Vector2 startPos, Vector2 direction, float speed, int type);
    
    public static SignalManager Instance;

    public override void _EnterTree()
    {
        Instance = this;
    }

    public static void EmitOnPlayerHit(int v)
    {
        Instance.EmitSignal(SignalName.OnPlayerHit, v);
    }

    public static void EmitOnPlayerHealthBonus(int v)
    {
        Instance.EmitSignal(SignalName.OnPlayerHealthBonus, v);
    }

    public static void EmitOnPlayerDied()
    {
        Instance.EmitSignal(SignalName.OnPlayerDied);
    }

    public static void EmitOnScoreUpdated(int v)
    {
        Instance.EmitSignal(SignalName.OnScoreUpdated, v);
    }

    public static void EmitOnCreateExplosion(Vector2 startPos, int explosionType)
    {
        Instance.EmitSignal(SignalName.OnCreateExplosion, startPos, explosionType);
    }

    public static void EmitOnCreatePowerUp(Vector2 startPos, int puType)
    {
        Instance.EmitSignal(SignalName.OnCreatePowerUp, startPos, puType);
    }

    public static void EmitOnCreateRandomPowerUp(Vector2 startPos)
    {
        Instance.EmitSignal(SignalName.OnCreateRandomPowerUp, startPos);
    }

    public static void EmitOnCreateBullet(Vector2 startPos, Vector2 direction, float speed, int type)
    {
        Instance.EmitSignal(SignalName.OnCreateBullet, startPos, direction, speed, type);
    }

    public static void EmitOnCreateHomingMissile(Vector2 startPos)
    {
        Instance.EmitSignal(SignalName.OnCreateHomingMissile, startPos);
    }
}
