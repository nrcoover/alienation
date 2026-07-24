using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SpaceUtils
{
    private static Random _random = new Random();
    
    public static T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(_random.Next(values.Length));
    }

    public static void PlayRandomAnimation(AnimatedSprite2D animatedSprite)
    {
        var animations = animatedSprite.SpriteFrames.GetAnimationNames().ToList();
        animatedSprite.Play(animations.PickRandom());
    }

    public static void SetAndStartTimer(Timer timer, float target, float variance)
    {
        timer.WaitTime = target - variance + (float)(_random.NextDouble() * (2 * variance));
        timer.Start();
    }

    public static T PickRandom<T>(this IList<T> list)
    {
        return list[_random.Next(list.Count)];
    }
}
