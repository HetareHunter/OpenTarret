using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplode : MonoBehaviour
{
    [SerializeField] float deathTime = 3.0f;
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
