using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteActivatManager : MonoBehaviour, ISpawnable
{
    public List<GameObject> _silhouettes = new List<GameObject>();

    /// <summary> アクティブなシルエットの数</summary>
    public int _activeSilhouetteNum = 0;
    [SerializeField] int _maxActiveSilhouetteNum = 3;

    /// <summary> 起き上がるまでの時間間隔 </summary>
    [SerializeField] float _spawnTime = 3.0f;
    float _nowTime = 0;

    /// <summary> 沸くまでの時間が経過したかどうか </summary>
    bool _onSpawnTimePassed = false;
    /// <summary> 沸いて良い状況かを管理するフラグ </summary>
    bool _spawnable = false;
    /// <summary> スポナーを起動しているかどうか </summary>
    public bool _onSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        JudgeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (_onSpawn)
        {
            if (_activeSilhouetteNum < _maxActiveSilhouetteNum)
            {
                CalculateSpawnTime();
            }
            JudgeSpawn();

            if (_spawnable)
            {
                EnemySpawn();
            }
        }
    }

    public void EnemySpawn()
    {
        int index = Random.Range(0, _silhouettes.Count); //どこに敵を生成するかの乱数
        //enemies.Add(Instantiate(enemy, spawners[index].transform.position, Quaternion.identity)); //敵を生成する
        //_silhouettes[index].
        ChangeEnemyNum(1); //敵の数が増える
        _nowTime = 0;
        _onSpawnTimePassed = false;
        _spawnable = false;
    }

    public void ResetEnemies()
    {

    }
    public void ChangeEnemyNum(int num)
    {

    }
    public void SpawnStart()
    {

    }
    public void SpawnEnd()
    {

    }
    void JudgeSpawn()
    {
        if (_activeSilhouetteNum < _maxActiveSilhouetteNum && _onSpawnTimePassed)
        {
            _spawnable = true;
        }
        else
        {
            _spawnable = false;
        }
    }

    /// <summary>
    /// 前回敵が沸いてからどれだけ時間が経ったのかを計測する
    /// </summary>
    void CalculateSpawnTime()
    {
        _nowTime += Time.deltaTime;
        if (_nowTime > _spawnTime)
        {
            _onSpawnTimePassed = true;
            _nowTime = 0;
        }
    }
}
