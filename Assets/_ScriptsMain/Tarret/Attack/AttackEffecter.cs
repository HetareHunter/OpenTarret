using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffecter : MonoBehaviour
{
    /// <summary>�Ռ��g�̃G�t�F�N�g </summary>
    [SerializeField] GameObject _shockWaveEffect;
    /// <summary>�Ռ��g�̐����ʒu </summary>
    [SerializeField] GameObject _shockWaveEffectInsPosi;

    /// <summary>�p�M�G�t�F�N�g�̐����ʒu </summary>
    [SerializeField] GameObject _wasteHeatEffectInsPosi;

    ChildParticleCreater _wasteHeatCreater;
    WorldParticleCreater _shockWaveCreater;

    // Start is called before the first frame update
    void Awake()
    {
        _wasteHeatCreater = _wasteHeatEffectInsPosi.GetComponent<ChildParticleCreater>();
        _shockWaveCreater = _shockWaveEffectInsPosi.GetComponent<WorldParticleCreater>();
    }

    /// <summary>
    /// �Ռ��g�����X�ɑ傫�����Ă����A��莞�Ԃŏ��ł���
    /// </summary>
    public void ShockWaveManager()
    {
        _shockWaveCreater.InstanceParticle(_shockWaveEffectInsPosi.transform.position, _shockWaveEffectInsPosi.transform.rotation);
    }

    /// <summary>
    /// �p�M�G�t�F�N�g�̎��̉�������ł܂ł̊Ǘ������郁�\�b�h
    /// </summary>
    public void InstanceWasteHeatEffect()
    {
        _wasteHeatCreater.InstanceParticle();
    }
}
