using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
    PackedScene _gameScene;
    PackedScene _mainScene;


    public static GameManager Instance { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;
        
        _gameScene = GD.Load<PackedScene>("res://Scenes/Level/Level.tscn");
        _mainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");
	}

    public static void LoadGameScene()
    {
        Instance.GetTree().ChangeSceneToPacked(Instance._gameScene);
    }

    public static void LoadMainScene()
    {
        Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
    }
}
