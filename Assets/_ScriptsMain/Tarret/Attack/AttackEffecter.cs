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

    ChildParticleCreater _wasteHeatCreater;
    WorldParticleCreater _shockWaveCreater;

    // Start is called before the first frame update
    void Awake()
    {
        _wasteHeatCreater = _wasteHeatEffectInsPosi.GetComponent<ChildParticleCreater>();
        _shockWaveCreater = _shockWaveEffectInsPosi.GetComponent<WorldParticleCreater>();
    }

    /// <summary>
    /// 衝撃波を徐々に大きくしていき、一定時間で消滅する
    /// </summary>
    public void ShockWaveManager()
    {
        _shockWaveCreater.InstanceParticle(_shockWaveEffectInsPosi.transform.position, _shockWaveEffectInsPosi.transform.rotation);
    }

    /// <summary>
    /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
    /// </summary>
    public void InstanceWasteHeatEffect()
    {
        _wasteHeatCreater.InstanceParticle();
    }
}
