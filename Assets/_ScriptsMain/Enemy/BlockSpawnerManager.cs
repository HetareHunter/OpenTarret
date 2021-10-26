using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の沸き方を管理するクラス
/// </summary>
public class BlockSpawnerManager : MonoBehaviour, ISpawnable
{
    [SerializeField] GameObject enemy;

    public void SpawnStart()
    {
        EnemySpawn();
    }

    public void SpawnEnd()
    {
        Reset();
    }

    public void ChangeEnemyNum(int num)
    {
    }


    public void EnemySpawn()
    {
        enemy.gameObject.SetActive(true);
    }

    /// <summary>
    /// 敵をすべて削除し、タイマー、敵のカウントを0にする
    /// </summary>
    public void Reset()
    {
        enemy.gameObject.SetActive(false);
    }
}
