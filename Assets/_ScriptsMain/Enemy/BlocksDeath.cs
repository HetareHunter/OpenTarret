using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDeath : MonoBehaviour, IEnemyDeath
{
    public void OnDead()
    {
        AddScore();
        Destroy(gameObject, 0);
    }

    public void AddScore()
    {
    }
}
