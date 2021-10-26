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
    MaterialPropertyBlock _material;
    MeshRenderer _meshRenderer;
    [SerializeField] float _dissolveDelay = 0.5f;
    string _dissolveThresholdstr = "Vector1_7e113bfd69c44598824716b9f5574a47";

    public void Reset()
    {
        UpdateDissolveMaterial(0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = new MaterialPropertyBlock();
    }

    void UpdateDissolveMaterial(float threshold)
    {
        _material.SetFloat(_dissolveThresholdstr, threshold);
        _meshRenderer.SetPropertyBlock(_material);

        if (threshold >= 1.0f)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void IsPlayDissolve(bool onPlay)
    {
        StartCoroutine(Dissolve(_dissolveDelay));
    }

    IEnumerator Dissolve(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        var threshold = 0.0f;
        while (threshold < 1.0f)
        {
            threshold += _dissolveSpeed * Time.deltaTime;
            UpdateDissolveMaterial(threshold);
            yield return null;
        }
    }
}
