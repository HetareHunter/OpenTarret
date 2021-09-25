using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffecter : MonoBehaviour
{
    /// <summary>衝撃波のエフェクト </summary>
    [SerializeField] GameObject _shockWaveEffect;
    /// <summary>衝撃波の生成位置 </summary>
    [SerializeField] GameObject _shockWaveEffectInsPosi;

    /// <summary>廃熱エフェクトの生成位置 </summary>
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
    /// 衝撃波を徐々に大きくしていき、一定時間で消滅する
    /// </summary>
    public void ShockWaveManager()
    {
        _shockWaveCreater.InstanceParticle();
    }

    /// <summary>
    /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
    /// </summary>
    public void InstanceWasteHeatEffect()
    {
        _wasteHeatCreater.InstanceParticle();
    }
}
