using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private HealthBar _healthBar;
	[Export] private AudioStreamPlayer2D _sound;
	[Export] private Label _scoreLabel;

	public override void _Ready()
	{
		SubscribeToSignals();

		ScoreManager.ResetScore();
	}

	public override void _ExitTree()
	{
		UnsubscribeFromSignals();
	}

	private void SubscribeToSignals()
	{
		SignalManager.Instance.OnPlayerHit += OnPlayerHit;
		SignalManager.Instance.OnPlayerHealthBonus += OnPlayerHealthBonus;
		SignalManager.Instance.OnScoreUpdated += OnScoreUpdated;

		_healthBar.OnDied += OnHealthBarDepleted;
	}

	private void UnsubscribeFromSignals()
	{
		SignalManager.Instance.OnPlayerHit -= OnPlayerHit;
		SignalManager.Instance.OnPlayerHealthBonus -= OnPlayerHealthBonus;
		SignalManager.Instance.OnScoreUpdated -= OnScoreUpdated;
	}

	private void OnPlayerHit(int v)
	{
		GD.Print($"GameUi::OnPlayerHit! {v}");
		_healthBar.TakeDamage(v);
	}

	private void OnPlayerHealthBonus(int v)
	{
		GD.Print($"GameUi::OnPlayerHealthBonus! {v}");
		_healthBar.IncrementValue(v);
		SoundManager.PlayPowerUpSound(_sound, Defs.PowerUpType.Health);
	}

	private void OnHealthBarDepleted()
	{
		SignalManager.EmitOnPlayerDied();
		GD.Print("GameUi::EmitOnPlayerDied()");
	}

	private void OnScoreUpdated(int v)
	{
		_scoreLabel.Text = v.ToString("D6");
	}
}
