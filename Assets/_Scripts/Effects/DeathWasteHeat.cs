using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWasteHeat : MonoBehaviour
{
    float deathTime = 1.3f;
    [SerializeField] TarretAttackData tarretAttackData;

    private void Start()
    {
        deathTime = tarretAttackData.wasteHeatExistTime;
    }
    private void OnEnable()
    {
        //Destroy(gameObject, deathTime);
        Invoke("FadeWasteHeat", deathTime);
    }

    void FadeWasteHeat()
    {
        gameObject.SetActive(false);
    }
}
