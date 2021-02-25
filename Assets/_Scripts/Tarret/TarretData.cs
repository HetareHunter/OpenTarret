using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="_Scripts/Create TarretData")]

public class TarretData : ScriptableObject
{
    public float shockWaveExistTime = 0.2f;
    public float untilRotateMagazine = 0.5f;

    public float attackVibeDuration = 1.0f;
    public float attackVibeFrequency = 0.8f;
    public float attackVibeAmplitude = 1.0f;
}
