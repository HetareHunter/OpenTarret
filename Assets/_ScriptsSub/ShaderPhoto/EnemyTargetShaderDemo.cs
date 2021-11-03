using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetShaderDemo : MonoBehaviour
{
    Vector3 appearLinePosi;
    [SerializeField] Vector3 startAppearPosi;
    [SerializeField] Vector3 endAppearPosi;
    //float[] _nowlinePosi;
    [SerializeField] float scanWallSpeed = 1.0f;
    [SerializeField] float _dissolveSpeed = 1.0f;
    float _startDissolveThreshold = 0.0f;
    float _nowDissolveThreshold;
    [SerializeField] Material[] spawnMTs;
    public bool _playScan = false;
    public bool _playDissolve = false;
    string _scanPosi = "Vector1_bc6a38dab71149e392ba24144467bb94";
    string _lineRange = "Vector1_73826083ca9b46dbb89f50e79b0c5527";
    string _dissolveThresholdstr = "Vector1_7e113bfd69c44598824716b9f5574a47";
    [SerializeField] bool _OnPhotoMode = false;


    // Start is called before the first frame update
    void Start()
    {
        //GetStartAppearLinePosi();
        ResetApeearLinePosi();
        ResetDissolveThreshold();
        appearLinePosi = startAppearPosi;
    }

    // Update is called once per frame
    void Update()
    {
        if (_OnPhotoMode)
        {

            return;
        }
        if (_playScan)
        {
            UpdateAppearLinePosi();
        }
        if (_playDissolve)
        {
            UpdateDissolveThreshold();
        }
    }


    void UpdateAppearLinePosi()
    {
        appearLinePosi.y -= scanWallSpeed * Time.deltaTime;
        UpdateMTAppearLinePosi();

        if (appearLinePosi.y <= endAppearPosi.y)
        {
            FinishScan();
        }
    }

    void UpdateDissolveThreshold()
    {
        _nowDissolveThreshold += _dissolveSpeed * Time.deltaTime;
        UpdateMTDissolve();

        if (_nowDissolveThreshold >= 1.0f)
        {
            FinishDissolve();
        }
    }

    void UpdateMTAppearLinePosi()
    {
        for (int i = 0; i < spawnMTs.Length; i++)
        {
            spawnMTs[i].SetFloat(_scanPosi, appearLinePosi.y);
        }
    }

    void UpdateMTDissolve()
    {
        for (int i = 0; i < spawnMTs.Length; i++)
        {
            spawnMTs[i].SetFloat(_dissolveThresholdstr, _nowDissolveThreshold);
        }
    }

    void FinishScan()
    {
        _playScan = false;
    }

    void FinishDissolve()
    {
        _playDissolve = false;
    }

    /// <summary>
    /// ScanPosiの値をスタート時点の値に戻る
    /// </summary>
    void ResetApeearLinePosi()
    {
        appearLinePosi = startAppearPosi;
        UpdateMTAppearLinePosi();
    }

    void ResetDissolveThreshold()
    {
        _nowDissolveThreshold = _startDissolveThreshold;
        UpdateMTDissolve();
    }

#if UNITY_EDITOR

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetApeearLinePosi();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ResetDissolveThreshold();
        }
    }

#endif
}
