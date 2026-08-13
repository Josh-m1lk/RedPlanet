using System;
using UnityEngine;

public enum SoundType
{
    Footstep,
    Gunshot
}

public class SoundManager : MonoBehaviour
{
    public static event Action<Vector3, float, SoundType> OnSoundEmitted;//Initialize sound action event

    public static void EmitSound(Vector3 position, float radius, SoundType type)
    {
        OnSoundEmitted?.Invoke(position, radius, type);//Grab pos, radius, and sound type on invoke when it happens
    }
}
