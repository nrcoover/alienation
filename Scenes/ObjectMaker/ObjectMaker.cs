using Godot;
using System;

public partial class ObjectMaker : Node2D
{
    
    public override void _Ready() 
    { 
        SignalManager.Instance.OnCreateExplosion += OnCreateExplosion;
        SignalManager.Instance.OnCreateHomingMissile += OnCreateHomingMissile;
        SignalManager.Instance.OnCreatePowerUp += OnCreatePowerUp;
        SignalManager.Instance.OnCreateRandomPowerUp += OnCreateRandomPowerUp;
        SignalManager.Instance.OnCreateBullet += OnCreateBullet;
    }

    private void OnCreateBullet(Vector2 startPos, Vector2 direction, float speed, int type)
    {
        throw new NotImplementedException();
    }


    private void OnCreateRandomPowerUp(Vector2 startPos)
    {
        throw new NotImplementedException();
    }


    private void OnCreatePowerUp(Vector2 startPos, int puType)
    {
        throw new NotImplementedException();
    }


    private void OnCreateHomingMissile(Vector2 startPos)
    {
        throw new NotImplementedException();
    }


    private void OnCreateExplosion(Vector2 startPos, int explosionType)
    {
        throw new NotImplementedException();
    }


    public override void _ExitTree()
    {
        SignalManager.Instance.OnCreateExplosion -= OnCreateExplosion;
        SignalManager.Instance.OnCreateHomingMissile -= OnCreateHomingMissile;
        SignalManager.Instance.OnCreatePowerUp -= OnCreatePowerUp;
        SignalManager.Instance.OnCreateBullet -= OnCreateBullet;
        SignalManager.Instance.OnCreateRandomPowerUp -= OnCreateRandomPowerUp;
    }

    private void AddObject(Node2D node, Vector2 globalPosition)
    {
        node.GlobalPosition = globalPosition;
        AddChild(node);
    }

    
}
