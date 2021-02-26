using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDeathTime : MonoBehaviour
{
    public void EnemiesDeath()
    {
        Destroy(gameObject);
    }
}
