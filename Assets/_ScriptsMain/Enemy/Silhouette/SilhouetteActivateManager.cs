using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// シルエットのスポナークラス(時間経過とアクティブなシルエットオブジェクトの数の条件で新たにシルエットを起こす処理を行う)
/// </summary>
public class SilhouetteActivateManager : MonoBehaviour, ISpawnable
{
    IGameStateChangable _gameStateChangable;
    [SerializeField] GameObject _gameManager;

    public List<GameObject> _registerSilhouettes = new List<GameObject>();
    Queue<SilhouetteActivatior> _silhouettes = new Queue<SilhouetteActivatior>();

    /// <summary> アクティブなシルエットの数</summary>
    int _activeSilhouetteNum = 0;
    [SerializeField] int _maxActiveSilhouetteNum = 3;

    /// <summary> 起き上がるまでの時間間隔 </summary>
    [SerializeField] float _spawnTime = 5.0f;
    float _nowTime = 0;

    /// <summary> 沸くまでの時間が経過したかどうか </summary>
    bool _onSpawnTimePassed = false;
    /// <summary> 沸いて良い状況かを管理するフラグ </summary>
    bool _spawnable = false;
    /// <summary> スポナーを起動しているかどうか </summary>
    public bool _onSpawn = false;
    /// <summary> ゲームが終了するまでに起動するシルエットの数 </summary>
    int _maxActivateSilhouetteNum;
    int _activatedSilhouetteNum = 0;

    public int ActiveSilhouetteNum
    {
        get
        {
            return _activeSilhouetteNum;
        }
        set
        {
            _activeSilhouetteNum += value;
        }
    }
    private void Awake()
    {
        _gameStateChangable = _gameManager.GetComponent<IGameStateChangable>();
    }

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
                ActivateSilhouette();
            }
        }
    }

    public void ActivateSilhouette()
    {
        _activatedSilhouetteNum++;
        ChangeEnemyNum(1); //敵の数が増える

        _silhouettes.Enqueue(_silhouettes.Peek());
        _silhouettes.Dequeue().Activate();

        _nowTime = 0;
        _onSpawnTimePassed = false;
        _spawnable = false;
    }

    public void ResetEnemies()
    {
        foreach (var item in _silhouettes)
        {
            item.Reset();
        }
    }
    public void ChangeEnemyNum(int num)
    {
        _activeSilhouetteNum += num;
        if (_activatedSilhouetteNum >= _maxActivateSilhouetteNum)
        {
            _onSpawn = false;
        }

        if (num < 0)
        {
            JudgeGameFinish();
        }
    }
    public void SpawnStart()
    {
        _onSpawn = true;
        RegisterSilhouettes(_registerSilhouettes);
        JudgeSpawn();
    }
    public void SpawnEnd()
    {
        _activatedSilhouetteNum = 0;
        _activeSilhouetteNum = 0;
        _nowTime = 0;
        _onSpawnTimePassed = false;
        _onSpawn = false;
        ResetEnemies();
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

    /// <summary>
    /// 的当てゲームに使うシルエットを登録する
    /// ここで登録したシルエットが時間経過で起動し、撃つ事ができるようになる
    /// </summary>
    /// <param name="registerSilhouettes">対象のシルエットはインスペクターから設定する</param>
    void RegisterSilhouettes(List<GameObject> registerSilhouettes)
    {
        _maxActivateSilhouetteNum = registerSilhouettes.Count;
        registerSilhouettes = registerSilhouettes.OrderBy(x => Guid.NewGuid()).ToList();
        for (int i = 0; i < _registerSilhouettes.Count; i++)
        {
            _silhouettes.Enqueue(registerSilhouettes[i].GetComponentInChildren<SilhouetteActivatior>());
        }
    }

    void JudgeGameFinish()
    {
        if (!_onSpawn)
        {
            _gameStateChangable.ChangeGameState(GameState.End);
        }
    }
}
