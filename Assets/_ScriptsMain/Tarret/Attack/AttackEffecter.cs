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

    ParticleCreater _wasteHeatCreater;
    ParticleCreater _shockWaveCreater;

    // Start is called before the first frame update
    void Awake()
    {
        _wasteHeatCreater = _wasteHeatEffectInsPosi.GetComponent<ParticleCreater>();
        _shockWaveCreater = _shockWaveEffectInsPosi.GetComponent<ParticleCreater>();
    }

    /// <summary>
    /// �Ռ��g�����X�ɑ傫�����Ă����A��莞�Ԃŏ��ł���
    /// </summary>
    public void ShockWaveManager()
    {
        _shockWaveCreater.InstanceParticle();
    }

    /// <summary>
    /// �p�M�G�t�F�N�g�̎��̉�������ł܂ł̊Ǘ������郁�\�b�h
    /// </summary>
    public void InstanceWasteHeatEffect()
    {
        _wasteHeatCreater.InstanceParticle();
    }
}
