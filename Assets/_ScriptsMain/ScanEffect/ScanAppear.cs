using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAppear : MonoBehaviour
{
    Vector3 appearLinePosi;
    [SerializeField] Vector3 startAppearPosi;
    [SerializeField] Vector3 endAppearPosi;
    float[] lineRanges;
    [SerializeField] float scanWallSpeed = 1.0f;
    [SerializeField] Material[] SpawnMTs;
    public bool playScan = false;

    // Start is called before the first frame update
    void Start()
    {
        appearLinePosi = startAppearPosi;
    }

    // Update is called once per frame
    void Update()
    {
        if (playScan)
        {
            UpdateAppearLinePosi();
        }
    }

    void UpdateAppearLinePosi()
    {
        appearLinePosi.y += scanWallSpeed * Time.deltaTime;
        UpdateMTAppearLinePosi();

        if (appearLinePosi.y >= endAppearPosi.y)
        {
            FinishScan();
        }
    }

    void UpdateMTAppearLinePosi()
    {
        for (int i = 0; i < SpawnMTs.Length; i++)
        {
            SpawnMTs[i].SetFloat("_ScanPosi", -appearLinePosi.y + lineRanges[i]);
        }
    }

    void GetStartAppearLinePosi()
    {
        lineRanges = new float[SpawnMTs.Length];
        for (int i = 0; i < SpawnMTs.Length; i++)
        {
            lineRanges[i] = SpawnMTs[i].GetFloat("_LineRange");
        }
    }

    public void StartSpawn()
    {
        playScan = true;
        GetStartAppearLinePosi();
    }

    void FinishScan()
    {
        playScan = false;
    }

    /// <summary>
    /// SpawnMTの見えない部分と見える部分の境界を初期位置に戻す。つまり全部見えない状態に戻る
    /// </summary>
    public void ResetApeearLinePosi()
    {
        appearLinePosi = startAppearPosi;
        UpdateMTAppearLinePosi();
    }

#if UNITY_EDITOR

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartSpawn();
        }
    }

#endif
}
