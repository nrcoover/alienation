using Godot;
using System;

public partial class GameOver : Control
{
	[Export] private Label _scoreLabel;
	[Export] private Timer _timer;

	public override void _Ready()
	{
		Hide();
		SetProcess(false);

		SubscribeToSignals();
	}
	
	public override void _ExitTree()
	{
		UnsubscribeFromSignals();
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			GameManager.LoadMainScene();
		}
	}

	private void SubscribeToSignals()
	{
		SignalManager.Instance.OnPlayerDied += OnPlayerDied;
		_timer.Timeout += OnTimerTimeout;
	}

	private void UnsubscribeFromSignals()
	{
		SignalManager.Instance.OnPlayerDied -= OnPlayerDied;
	}

	private void OnTimerTimeout()
	{
		SetProcess(true);
	}


	private void OnPlayerDied()
	{        
		GetTree().Paused = true;
		_timer.Start();
		_scoreLabel.Text = $"Score: {ScoreManager.GetScore()} (Best: {ScoreManager.GetHighScore()})";
		Show();
	}
}
