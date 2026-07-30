using Godot;
using System;

public partial class HealthBar : TextureProgressBar
{
	[Signal] public delegate void OnDiedEventHandler();

	[Export] public int LevelLow { get; set; } = 30;
	[Export] public int LevelMed { get; set; } = 65;
	[Export] public int StartHealth { get; set; } = 100;
	[Export] public int MaxHealth { get; set; } = 100;

	private static readonly Color COLOR_DANGER = new Color("#cc0000");
	private static readonly Color COLOR_MIDDLE = new Color("#ff9900");
	private static readonly Color COLOR_GOOD = new Color("#33cc33");

	public override void _Ready()
	{
		MaxValue = MaxHealth;
		Value = StartHealth;
		SetColor();
	}

	private void SetColor()
	{
		if (Value < LevelLow)
		{
			TintProgress = COLOR_DANGER;
		}
		else if (Value < LevelMed)
		{
			TintProgress = COLOR_MIDDLE;
		}
		else
		{
			TintProgress = COLOR_GOOD;
		}
	}

	public void IncrementValue(int v)
	{
		Value += v;

		if (Value <= 0)
		{
			EmitSignal(SignalName.OnDied);
		}
		SetColor();
	}

	public void TakeDamage(int v)
	{
		IncrementValue(-v);
	}
}
