using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeath : MonoBehaviour, IEnemyDeath
{
    [SerializeField] float deathTime = 0.5f;
    [SerializeField] int addScore = 100;

    public void OnDead()
    {
        AddScore();
        GetComponent<CSGCaller>().SubtractionLR();
        Destroy(gameObject, deathTime);
    }

    public void AddScore()
    {
        ScoreManager.Instance.AddScore(addScore);
    }
}
