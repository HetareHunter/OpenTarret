using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerEffecter : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretAttackData;

    /// <summary>衝撃波のエフェクト </summary>
    [SerializeField] GameObject m_shockWaveEffect;
    /// <summary>衝撃波の生成位置 </summary>
    [SerializeField] GameObject m_shockWaveEffectInsPosi;

    /// <summary>廃熱のエフェクト </summary>
    [SerializeField] GameObject[] m_wasteHeatEffects;
    int wasteHeatIndex = 0;
    /// <summary>廃熱エフェクトの生成位置 </summary>
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 衝撃波を徐々に大きくしていき、一定時間で消滅する
    /// </summary>
    public void ShockWaveManager()
    {
        m_shockWaveEffect.transform.position = m_shockWaveEffectInsPosi.transform.position;
        m_shockWaveEffect.transform.rotation = m_shockWaveEffectInsPosi.transform.rotation;
        m_shockWaveEffect.SetActive(true);
    }

    /// <summary>
    /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
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
