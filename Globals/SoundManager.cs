using Godot;
using System.Collections.Generic;

public partial class SoundManager : Node
{
    public static SoundManager Instance;

    private AudioStream SoundDoorOpen = GD.Load<AudioStream>("res://assets/sounds/misc/sci-fi-door.wav");
    private AudioStream SoundPowerUp = GD.Load<AudioStream>("res://assets/sounds/powerup/power_up_deploy.wav");

    private Dictionary<Defs.PowerUpType, AudioStream> PowerUpSounds;
    private List<AudioStream> ExplosionSounds = new List<AudioStream>();
    private List<AudioStream> LaserSounds = new List<AudioStream>();

    public override void _Ready()
    {
        Instance = this;

        PowerUpSounds = new Dictionary<Defs.PowerUpType, AudioStream>
        {
            { Defs.PowerUpType.Health, GD.Load<AudioStream>("res://assets/sounds/powerup/health_16.wav") },
            { Defs.PowerUpType.Shield, GD.Load<AudioStream>("res://assets/sounds/powerup/shield_18.wav") }
        };

        for (int i = 1; i <= 13; i++)
        {
            ExplosionSounds.Add(GD.Load<AudioStream>($"res://assets/sounds/explosions/sfx_exp_medium{i}.wav"));
        }

        for (int i = 1; i <= 12; i++)
        {
            LaserSounds.Add(GD.Load<AudioStream>($"res://assets/sounds/lasers/sfx_wpn_laser{i}.wav"));
        }
    }

    private AudioStream GetRandomSoundFromList(List<AudioStream> soundList)
    {
        return soundList.PickRandom();
    }

    public void PlayRandomSoundFromList(AudioStreamPlayer2D audio, List<AudioStream> soundList)
    {
        audio.Stream = GetRandomSoundFromList(soundList);
        audio.Play();
    }

    public static void PlayExplosionRandom(AudioStreamPlayer2D audio)
    {
        Instance.PlayRandomSoundFromList(audio, Instance.ExplosionSounds);
    }

    public static void PlayLaserRandom(AudioStreamPlayer2D audio)
    {
        Instance.PlayRandomSoundFromList(audio, Instance.LaserSounds);
    }

    public AudioStream GetSound(int fileNum, List<AudioStream> soundList)
    {
        return soundList[fileNum - 1];
    }

    public static void PlayPowerUpSound(AudioStreamPlayer2D audio, Defs.PowerUpType puType)
    {
        audio.Stream = Instance.PowerUpSounds[puType];
        audio.Play();
    }

    public static void PlayPowerUpDeploySound(AudioStreamPlayer2D audio)
    {
        audio.Stream = Instance.SoundPowerUp;
        audio.Play();
    }
}
