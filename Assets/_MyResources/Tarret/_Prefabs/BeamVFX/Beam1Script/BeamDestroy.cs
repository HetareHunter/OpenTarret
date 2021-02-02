using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDestroy : MonoBehaviour
{
    bool deathCall = false;
    [SerializeField] float untilDeath = 1;

    // Update is called once per frame
    void Update()
    {
        if (!deathCall)
        {
            Destroy(gameObject, untilDeath);
            deathCall = true;
        }
    }
}
