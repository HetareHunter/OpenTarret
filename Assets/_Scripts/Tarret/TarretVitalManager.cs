using System.Collections;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class TarretVitalManager : MonoBehaviour
{
    [SerializeField] TarretVitalData TarretVitalData;

    [SerializeField] float tarretDamageCoefficient = 0.95f;
    [SerializeField] float sieldDamageCoefficient = 0.9f;

    [SerializeField] Slider tarretHPSlider;
    float tarretHP;

    [SerializeField] Slider sieldHPSlider;
    float sieldHP;
    [SerializeField] float recoveryTime = 1.0f;
    float currentRecoveryTime = 0;
    [SerializeField] float sieldRecoverySpeed = 2.0f;

    public bool onSield = true;
    Sequence sieldRecoverySequence;

    /// <summary>攻撃を受けているときtrueになる </summary>
    bool onAttacked = false;
    //CancellationTokenSource cancellationToken;

    // Start is called before the first frame update
    void Start()
    {
        tarretHP = TarretVitalData.TarretMaxHP;
        sieldHP = TarretVitalData.TarretMaxSield;

        //cancellationToken = new CancellationTokenSource();
        //SieldRecovery(cancellationToken.Token).Forget();
        //sieldRecoverySequence
        //    .OnStart(() =>
        //    {
        //        onSield = true;
        //        Debug.Log("シールドオン！");
        //    }
        //    )
        //    .Append(
        //    sieldRecoveryTweener = DOTween.To(
        //    () => sieldHP,
        //    (x) => sieldHP = x,
        //    TarretVitalData.TarretMaxSield,
        //    sieldRecoverySpeed)
        //    .SetEase(Ease.Linear)
        //    );
    }

    private void Update()
    {
        sieldHPSlider.value = sieldHP / TarretVitalData.TarretMaxSield;
        if (onAttacked)
        {
            currentRecoveryTime += Time.deltaTime;
            if (currentRecoveryTime > recoveryTime)
            {
                currentRecoveryTime = 0;
                onAttacked = false;
                SieldRecovery();
                //onSield = true;
            }
        }
    }

    public void TarretDamage(float damage)
    {
        onAttacked = true;
        currentRecoveryTime = 0;
        tarretHP -= (damage * tarretDamageCoefficient);
        tarretHPSlider.value = tarretHP / TarretVitalData.TarretMaxHP;
        Debug.Log("TarretVitalData.TarretHP : " + tarretHP);
    }

    public void SieldDamage(float damage)
    {
        onAttacked = true;
        currentRecoveryTime = 0;
        //sieldRecoveryTweener.Kill();
        sieldRecoverySequence.Kill();
        sieldHP -= (damage * sieldDamageCoefficient);

        if (sieldHP <= 0)
        {
            onSield = false;
        }

    }

    void SieldRecovery()
    {

        sieldRecoverySequence = DOTween.Sequence()
            .OnStart(() =>
            {
                onSield = true;
                Debug.Log("シールドオン！");
            }
            )
            .Append(
            DOTween.To(
            () => sieldHP,
            (x) => sieldHP = x,
            TarretVitalData.TarretMaxSield,
            sieldRecoverySpeed)
            .SetEase(Ease.Linear)
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        // レイヤー名を取得して layerName に格納
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        // レイヤー名がEnemyBullet以外の時は何も行わない
        if (layerName != "EnemyBullet") return;

        float damage = other.transform.GetComponent<BulletMove>().power;
        if (onSield)
        {
            SieldDamage(damage);
        }
        else
        {
            TarretDamage(damage);
        }
    }
}
