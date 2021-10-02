using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DissolveShaderのマテリアルが付いているオブジェクトにアタッチする。
/// メッシュが溶けていく演出を制御するスクリプト
/// </summary>
public class MeshDissolver : MonoBehaviour
{
    [SerializeField] float _dissolveSpeed = 0.2f;
    float _threshold;
    MaterialPropertyBlock _material;
    MeshRenderer _meshRenderer;
    [SerializeField] float _disolveDelay = 0.1f;
    bool _playDissolve = false;

    const float DissolveAlpha = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = new MaterialPropertyBlock();
        _threshold = _material.GetFloat("_Threshold") - _disolveDelay;
        _material.SetFloat("_Alpha", DissolveAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playDissolve)
        {
            UpdateDissolveMaterial();
        }
    }

    void UpdateDissolveMaterial()
    {
        _threshold += _dissolveSpeed * Time.deltaTime;
        _material.SetFloat("_Threshold", _threshold);
        _meshRenderer.SetPropertyBlock(_material);

        if (_threshold > 1.0f)
        {
            _threshold = 0.0f;
            Destroy(gameObject);//destroyするのでresetはかけない
        }
    }

    public void ISPlayDissolve(bool a)
    {
        _playDissolve = a;
    }
}
