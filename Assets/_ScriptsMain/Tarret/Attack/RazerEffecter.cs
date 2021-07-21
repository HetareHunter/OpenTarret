using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerEffecter : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretAttackData;

    /// <summary>�Ռ��g�̃G�t�F�N�g </summary>
    [SerializeField] GameObject m_shockWaveEffect;
    /// <summary>�Ռ��g�̐����ʒu </summary>
    [SerializeField] GameObject m_shockWaveEffectInsPosi;

    /// <summary>�p�M�̃G�t�F�N�g </summary>
    [SerializeField] GameObject[] m_wasteHeatEffects;
    int wasteHeatIndex = 0;
    /// <summary>�p�M�G�t�F�N�g�̐����ʒu </summary>
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// �Ռ��g�����X�ɑ傫�����Ă����A��莞�Ԃŏ��ł���
    /// </summary>
    public void ShockWaveManager()
    {
        m_shockWaveEffect.transform.position = m_shockWaveEffectInsPosi.transform.position;
        m_shockWaveEffect.transform.rotation = m_shockWaveEffectInsPosi.transform.rotation;
        m_shockWaveEffect.SetActive(true);
    }

    /// <summary>
    /// �p�M�G�t�F�N�g�̎��̉�������ł܂ł̊Ǘ������郁�\�b�h
    /// </summary>
    public void InstanceWasteHeatEffect()
    {
        m_wasteHeatEffects[wasteHeatIndex].SetActive(true);
        wasteHeatIndex++;
        if (wasteHeatIndex >= m_wasteHeatEffects.Length)
        {
            wasteHeatIndex = 0;
        }
    }
}
