using Godot;

public partial class SaucerManager : Node2D
{
	private static readonly PackedScene SaucerScene = GD.Load<PackedScene>("res://Scenes/Saucer/Saucer.tscn");

	[Export] private float _waitTime = 15.0f;
	[Export] private float _waitVariation = 3.0f; 
	[Export] private Timer _timer;
	[Export] private Godot.Collections.Array<Path2D> _path2Ds = new();

	public override void _Ready()
	{
		_timer.Timeout += OnTimerTimeout;
		ResetTimer();
	}

	private void ResetTimer()
	{
		SpaceUtils.SetAndStartTimer(_timer, _waitTime, _waitVariation);
	}

	private void SpawnSaucer()
	{
		GD.Print("SpawnSaucer");
		Saucer saucer = SaucerScene.Instantiate<Saucer>();
		Path2D path = _path2Ds.PickRandom();
		path.AddChild(saucer);
		ResetTimer();
	}

	private void OnTimerTimeout()
	{
		SpawnSaucer();
	}
}
