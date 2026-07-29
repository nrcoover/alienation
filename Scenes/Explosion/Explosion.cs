using Godot;
using System;

public partial class Explosion : AnimatedSprite2D
{
	const string ANIMATION_EXPLOSION = "explosion";
	const string ANIMATION_BOOM = "boom";
	
	[Export] private AudioStreamPlayer2D _sound;

	public override void _Ready()
	{
		AnimationFinished += OnAnimationFinished;
		SoundManager.PlayExplosionRandom(_sound);
		Play();
	}

	public void Setup(Defs.ExplosionType explosionType)
	{
		switch(explosionType) {
			case Defs.ExplosionType.Explosion:
				Animation = ANIMATION_EXPLOSION;
				break;
			case Defs.ExplosionType.Boom:
				Animation = ANIMATION_BOOM;
				break;
		}
	}

	private void OnAnimationFinished()
	{
		QueueFree();
	}
}
