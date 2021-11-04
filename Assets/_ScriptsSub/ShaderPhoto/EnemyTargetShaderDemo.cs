using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetShaderDemo : MonoBehaviour
{
    public Vector3 appearLinePosi;
    public Vector3 startAppearPosi;
    public Vector3 endAppearPosi;

    [SerializeField] float scanWallSpeed = 1.0f;
    [SerializeField] float _dissolveSpeed = 1.0f;
    float _startDissolveThreshold = 0.0f;
    public float _nowDissolveThreshold;
    [SerializeField] Material[] spawnMTs;
    public bool _playScan = false;
    public bool _playDissolve = false;
    [SerializeField] string _scanPosi = "Vector1_bc6a38dab71149e392ba24144467bb94";
    [SerializeField] string _lineRange = "Vector1_73826083ca9b46dbb89f50e79b0c5527";
    [SerializeField] string _dissolveThresholdstr = "Vector1_7e113bfd69c44598824716b9f5574a47";
    Animator _animator;
    [SerializeField] bool _OnDemoAnimation = false;


    // Start is called before the first frame update
    void Start()
    {
        ResetApeearLinePosi();
        ResetDissolveThreshold();
        appearLinePosi = startAppearPosi;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_OnPhotoMode)
        //{
        //    return;
        //}
        //if (_playScan)
        //{
        //    UpdateAppearLinePosi();
        //}
        //if (_playDissolve)
        //{
        //    UpdateDissolveThreshold();
        //}
        if (Input.GetKeyDown(KeyCode.S))
        {
            _animator.SetTrigger("DemoStart");
        }
        UpdateMTAppearLinePosi();
        UpdateMTDissolve();
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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetApeearLinePosi();
            ResetDissolveThreshold();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

        }
    }

#endif
}
