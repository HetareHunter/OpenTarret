using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキャン対象のオブジェクトに付ける。試作したスキャンシェーダのスクリプト
/// </summary>
public class ScanEffect : MonoBehaviour
{
    Material material;
    float minposi;
    [SerializeField] float limitScanLine = 1.0f;
    public bool scanTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        RunScanLine();
        material.SetFloat("_ScanLinePosi", minposi);
    }

    void RunScanLine()
    {
        if (scanTrigger)
        {
            minposi += Time.deltaTime;
            if (minposi > limitScanLine)
            {
                minposi = 0;
                scanTrigger = false;
            }
        }
        else
        {
            minposi = 0;
        }
    }
}
