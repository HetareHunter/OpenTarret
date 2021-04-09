using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarretVitalManager : MonoBehaviour
{
    [SerializeField] TarretVitalData TarretVitalData;

    [SerializeField] float tarretDamageCoefficient = 0.95f;
    [SerializeField] float sieldDamageCoefficient = 0.9f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TarretDamage(int damage)
    {
        TarretVitalData.TarretHP -= (damage * tarretDamageCoefficient);
        Debug.Log("TarretVitalData.TarretHP : " + TarretVitalData.TarretHP);
    }

    public void SieldDamage(int damage)
    {
        TarretVitalData.TarretSield -= (damage * sieldDamageCoefficient);
    }
}
