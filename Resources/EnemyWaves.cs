using Godot;
using System;

[GlobalClass]
public partial class EnemyWaves : Resource
{
  [Export] private Godot.Collections.Array<EnemyWave> _waves = new();

  public EnemyWave GetWaveForWaveCount(int waveCount)
  {
    return _waves[waveCount % _waves.Count];
  }

  public bool WaveIsStart(int waveCount)
  {
    return waveCount % _waves.Count == 0;
  }
}
