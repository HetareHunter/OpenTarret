using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DissolveShader�̃}�e���A�����t���Ă���I�u�W�F�N�g�ɃA�^�b�`����B
/// ���b�V�����n���Ă������o�𐧌䂷��X�N���v�g
/// </summary>
public class MeshDissolver : MonoBehaviour
{
    [SerializeField] float _dissolveSpeed = 0.2f;
    float _threshold;
    const float DissolveAlpha= 0.5f;
    MaterialPropertyBlock _material;
    MeshRenderer _meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = new MaterialPropertyBlock();
        _threshold = _material.GetFloat("_Threshold");
        _material.SetFloat("_Alpha", DissolveAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDissolveMaterial();
    }

    void UpdateDissolveMaterial()
    {
        _threshold += _dissolveSpeed * Time.deltaTime;
        _material.SetFloat("_Threshold", _threshold);
        _meshRenderer.SetPropertyBlock(_material);

        if (_threshold > 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
