using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の沸き方を管理するクラス
/// </summary>
public class SpawnerManager : MonoBehaviour,ISpawnable
{
    List<GameObject> spawners = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();
    [SerializeField] GameObject enemy;
    //public GameObject enemyTarget;
    /// <summary> ゲーム上にいる敵の数をカウントする変数</summary>
    public int enemyNum = 0;
    [SerializeField] int maxEnemyNum = 3;
    bool lessMaxEnemyNum = false;
    /// <summary> 沸くまでの時間間隔 </summary>
    [SerializeField] float spawnTime = 3.0f;
    float nowTime = 0;
    /// <summary> 沸くまでの時間が経過したかどうか </summary>
    bool onSpawnTimePassed = false;
    /// <summary> 沸いて良い状況かを管理するフラグ </summary>
    bool spawnable = false;
    /// <summary> スポナーを起動しているかどうか </summary>
    public bool onSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in transform)
        {
            spawners.Add(item.gameObject); //子オブジェクトの全てをスポナーの位置とする
        }

        JudgeEnemyNum();
        JudgeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (onSpawn)
        {
            if (lessMaxEnemyNum)
            {
                CalculateSpawnTime();
            }
            
            JudgeSpawn();
            if (spawnable)
            {
                EnemySpawn();
            }
        }
    }

    public void SpawnStart()
    {
        onSpawn = true;
        JudgeEnemyNum();
        JudgeSpawn();
    }

    public void SpawnEnd()
    {
        onSpawn = false;
        ResetSpawner();
        ResetEnemies();
    }

    public void ChangeEnemyNum(int num)
    {
        enemyNum += num;
        JudgeEnemyNum();
    }

    void JudgeEnemyNum()
    {
        if (enemyNum < maxEnemyNum)
        {
            lessMaxEnemyNum = true;
        }
        else
        {
            lessMaxEnemyNum = false;
        }
    }

    void JudgeSpawn()
    {
        if (lessMaxEnemyNum && onSpawnTimePassed)
        {
            spawnable = true;
        }
        else
        {
            spawnable = false;
        }
    }

    /// <summary>
    /// 前回敵が沸いてからどれだけ時間が経ったのかを計測する
    /// </summary>
    void CalculateSpawnTime()
    {
        nowTime += Time.deltaTime;
        if (nowTime > spawnTime)
        {
            onSpawnTimePassed = true;
            nowTime = 0;
        }
    }

    public void EnemySpawn()
    {
        int index = Random.Range(0, spawners.Count); //どこに敵を生成するかの乱数
        enemies.Add(Instantiate(enemy, spawners[index].transform.position, Quaternion.identity)); //敵を生成する
        spawners.RemoveAt(index); //敵が1度出現したスポナーは消す
        if (spawners.Count <= 1)
        {
            ResetSpawner();
        }
        ChangeEnemyNum(1); //敵の数が増える
        nowTime = 0;
        onSpawnTimePassed = false;
        spawnable = false;
    }

    /// <summary>
    /// スポナーのリストを初期化する
    /// </summary>
    void ResetSpawner()
    {
        spawners.Clear();

        foreach (Transform item in transform)
        {
            spawners.Add(item.gameObject);
        }
    }

    /// <summary>
    /// 敵をすべて削除し、タイマー、敵のカウントを0にする
    /// </summary>
    public void ResetEnemies()
    {
        foreach (var item in enemies)
        {
            if (item == null) continue;
            var enemyDeath = item.GetComponent<EnemyDeath>();
            if (enemyDeath != null)
            {
                item.GetComponent<DroneDeath>().BulletDead();
            }
            
            Destroy(item);
        }
        enemies.Clear();
        nowTime = 0;
        enemyNum = 0;
    }
}
