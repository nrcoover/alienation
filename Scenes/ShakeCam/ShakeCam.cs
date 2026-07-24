using Godot;
using System;

public partial class ShakeCam : Camera2D
{
    [Export] private Timer _timer;
    private static readonly Vector2 ShakeRange = new Vector2(-5.0f, 5.0f);

    public override void _Ready()
    {
        SignalManager.Instance.OnPlayerHit += OnPlayerHit;
        _timer.Timeout += OnTimerTimeout;
        SetProcess(false);
    }

    public override void _ExitTree()
    {
        SignalManager.Instance.OnPlayerHit -= OnPlayerHit;
    }

    public override void _Process(double delta)
    {
        Offset = new Vector2(
             GetRandomShakeAmount(), GetRandomShakeAmount()
        );
    }

    private float GetRandomShakeAmount()
    {
        return (float)GD.RandRange(ShakeRange.X, ShakeRange.Y);
    }

    private void OnPlayerHit(int value)
    {
        SetProcess(true);        
        _timer.Start();
    }

    private void OnTimerTimeout()
    {
        SetProcess(false);
        Offset = Vector2.Zero;
    }
}
