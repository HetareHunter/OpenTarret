using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float deathTime = 0.5f;
    [SerializeField] int addScore = 100;

    public void OnDead()
    {
        AddScore();
        Destroy(gameObject, deathTime);
    }

    void AddScore()
    {
        ScoreManager.Instance.AddScore(addScore);
    }
}