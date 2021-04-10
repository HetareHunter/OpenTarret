using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarretVitalManager : MonoBehaviour
{
    [SerializeField] TarretVitalData TarretVitalData;

    [SerializeField] float tarretDamageCoefficient = 0.95f;
    [SerializeField] float sieldDamageCoefficient = 0.9f;

    [SerializeField] Slider tarretHPSlider;
    float tarretHP;

    [SerializeField] Slider sieldHPSlider;
    float sieldHP;

    public bool onSield = true;

    // Start is called before the first frame update
    void Start()
    {
        tarretHP = TarretVitalData.TarretMaxHP;
        sieldHP = TarretVitalData.TarretMaxSield;
    }

    public void TarretDamage(int damage)
    {
        tarretHP -= (damage * tarretDamageCoefficient);
        tarretHPSlider.value = tarretHP / TarretVitalData.TarretMaxHP;
        Debug.Log("TarretVitalData.TarretHP : " + tarretHP);
    }

    public void SieldDamage(int damage)
    {
        sieldHP -= (damage * sieldDamageCoefficient);
        sieldHPSlider.value = sieldHP / TarretVitalData.TarretMaxSield;
        if (sieldHP <= 0)
        {
            onSield = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // レイヤー名を取得して layerName に格納
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        // レイヤー名がEnemyBullet以外の時は何も行わない
        if (layerName != "EnemyBullet") return;

        int damage = other.transform.GetComponent<BulletMove>().power;
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
