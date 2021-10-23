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

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = new MaterialPropertyBlock();
        _threshold = _material.GetFloat("Vector1_7e113bfd69c44598824716b9f5574a47") - _disolveDelay;
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
        _material.SetFloat("Vector1_7e113bfd69c44598824716b9f5574a47", _threshold);
        _meshRenderer.SetPropertyBlock(_material);

        if (_threshold > 1.0f)
        {
            _threshold = 0.0f;
            Destroy(gameObject);//destroyするのでresetはかけない
        }
    }

    public void IsPlayDissolve(bool onPlay)
    {
        _playDissolve = onPlay;
    }
}
