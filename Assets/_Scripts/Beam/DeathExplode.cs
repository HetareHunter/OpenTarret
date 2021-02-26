using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplode : MonoBehaviour
{
    [SerializeField] float deathTime = 3.0f;
    private void Awake()
    {
        Destroy(gameObject, deathTime);
    }
}
