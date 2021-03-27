using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplode : MonoBehaviour
{
    float deathTime = 3.0f;
    [SerializeField] TarretAttackData tarretAttackData;
    private void Start()
    {
        deathTime = tarretAttackData.explodeExistTime;
    }
    private void OnEnable()
    {
        //Destroy(gameObject, deathTime);
        Invoke("FadeExplode", deathTime);
    }

    void FadeExplode()
    {
        gameObject.SetActive(false);
    }
}
