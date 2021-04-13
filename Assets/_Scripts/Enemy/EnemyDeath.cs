using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float deathTime = 0.5f;
    [SerializeField] int addScore = 100;
    [SerializeField] GameObject[] muzzle;

    public void OnDead()
    {
        AddScore();
        BulletDead();
        Destroy(gameObject, deathTime);
        
    }

    void BulletDead()
    {
        foreach (var item in muzzle)
        {
            item.GetComponent<EnemyBulletManager>().onDead();
        }
    }

    void AddScore()
    {
        ScoreManager.Instance.AddScore(addScore);
    }
}