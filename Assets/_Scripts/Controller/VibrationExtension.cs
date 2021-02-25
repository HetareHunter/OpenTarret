using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 振動処理拡張クラス
/// </summary>
public class VibrationExtension : SingletonMonoBehaviour<VibrationExtension>
{
    public void VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        StartCoroutine(VibrateForSeconds(duration, frequency, amplitude, controller));
    }

    IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        // 振動開始
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        // 振動間隔分待つ
        yield return new WaitForSeconds(duration);

        // 振動終了
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
