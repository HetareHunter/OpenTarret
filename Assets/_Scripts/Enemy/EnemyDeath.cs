using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] float deathTime = 0.5f;
    [SerializeField] int addScore = 100;
    [SerializeField] GameObject[] muzzle;

    private void Update()
    {
        
    }
    public void OnDead()
    {
        SpawnerManager.Instance.ChangeEnemyNum(-1); //敵のカウントを1減らす
        AddScore();
        BulletDead();
        Destroy(gameObject, deathTime);

        //Observable.Timer(TimeSpan.FromSeconds(deathTime))
        //    .Subscribe(_ => gameObject.SetActive(false))
        //    .AddTo(this);
    }

    public void BulletDead()
    {
        foreach (var item in muzzle)
        {
            if (item == null) continue;
            item.GetComponent<EnemyBulletManager>().OnDead();
        }
    }

    void AddScore()
    {
        ScoreManager.Instance.AddScore(addScore);
    }
}