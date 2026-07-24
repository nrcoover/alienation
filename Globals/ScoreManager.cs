using Godot;
using System;

public partial class ScoreManager : Node
{
    private int _score = 0;
    private int _highScore = 0;

    public static ScoreManager Instance;

    public override void _Ready()
    {
        Instance = this;
    }

    public static int GetScore()
    {
        return Instance._score;
    }

    public static int GetHighScore()
    {
        return Instance._highScore;
    }

    public static void IncrementScore(int value)
    {
        Instance._score += value;
        if (Instance._highScore < Instance._score)
        {
            Instance._highScore = Instance._score;
        }
        SignalManager.EmitOnScoreUpdated(Instance._score);
    }

    public static void ResetScore()
    {
        Instance._score = 0;
        SignalManager.EmitOnScoreUpdated(Instance._score);

    }
}
