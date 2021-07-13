using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "_Scripts/Create TarretAttackData")]

public class TarretAttackData : ScriptableObject
{
    [Header("衝撃波の存在する時間")]
    public float shockWaveExistTime = 0.2f;
    public int shockWaveExistFrame = 2;

    [Header("爆発の存在する時間")]
    public float explodeExistTime = 3.0f;
    public int explodeExistFrame = 23;

    [Header("廃熱蒸気の存在する時間")]
    public float wasteHeatExistTime = 2.0f;
    public int wasteHeatExistFrame = 15;

    [Header("マガジンの回転するまでの時間")]
    public float untilRotateMagazine = 0.5f;
    public int untilRotateMagazineFrame = 4;

    public int wasteHeatCount = 2;
    [Header("レーザーの太さ")]
    public float razerWidth = 1.3f;
    [Header("レーザーの存在時間")]
    public float razerExistTime = 0.5f;
    public int razerExistFrame = 4;

    [Header("攻撃時のコントローラの振動時間")]
    public float attackVibeDuration = 1.0f;
    [Header("攻撃時のコントローラの振動数")]
    public float attackVibeFrequency = 0.8f;
    [Header("攻撃時のコントローラの振幅")]
    public float attackVibeAmplitude = 1.0f;
}
