using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// シルエットが倒れるときの処理を行うクラス
/// </summary>
public class SilhouetteActivatior : EnemyDeath
{
    [SerializeField] int _addScore = 100;
    [SerializeField] float _excellentRankTimeCoe = 0.7f;
    [SerializeField] float _goodRankTimeCoe = 0.5f;
    [SerializeField] float _excellentRankBonusScoreCoe = 2.0f;
    [SerializeField] float _goodRankBonusScoreCoe = 1.5f;
    [SerializeField] float _deathStandDownRotateTime = 0.5f;
    [SerializeField] float _activateStandUpRotateTime = 1.0f;
    bool _isActive = false;
    Collider _collider;
    SilhouetteMover _silhouetteMover;
    float _standTime;
    float _startStandTime;
    bool _countStart = false;
    [Inject]
    ISpawnable _spawnable;

    public bool IsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
            if (value == true)
            {
                _collider.enabled = true;
            }
        }
    }

    public void Reset()
    {
        IsActive = false;
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownRotateTime);
        TimeCountReset();
    }

    private void Start()
    {
        _silhouetteMover = transform.parent.GetComponent<SilhouetteMover>();
        _collider = GetComponent<Collider>();

        _standTime = _silhouetteMover.ActiveTime;
        _startStandTime = _standTime;
    }

    private void Update()
    {
        if (_countStart)
        {
            TimeCountDown();
        }

    }
    public override void OnDead()
    {
        IsActive = false;
        AddScore();
        _collider.enabled = false;
        _spawnable.ChangeEnemyNum(-1);
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownRotateTime);
    }

    public override void AddScore()
    {
        if (_standTime >= _startStandTime * _excellentRankTimeCoe)
        {
            ScoreManager.Instance.AddScore((int)(_addScore * _excellentRankBonusScoreCoe));
            Debug.Log("Excellent!");
        }
        else if (_standTime >= _startStandTime * _goodRankTimeCoe && _standTime < _startStandTime * _excellentRankTimeCoe)
        {
            ScoreManager.Instance.AddScore((int)(_addScore * _goodRankBonusScoreCoe));
            Debug.Log("Good!");
        }
        else if(_standTime < _startStandTime * _goodRankTimeCoe)
        {
            ScoreManager.Instance.AddScore(_addScore);
            Debug.Log("Normal!");
        }
    }

    public void ActivateSilhouette()
    {
        IsActive = true;
        _collider.enabled = true;
        TimeCountReset();
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Up, _activateStandUpRotateTime);
        _silhouetteMover.DoRestart();
        _countStart = true;
    }

    public void NonActivateSilhouette()
    {
        IsActive = false;
        _collider.enabled = false;
        _spawnable.ChangeEnemyNum(-1);
        //TimeCountReset();
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _activateStandUpRotateTime);
    }

    void TimeCountDown()
    {
        _standTime -= Time.deltaTime;
        if (_standTime < 0.0f)//カウントダウン終了
        {
            NonActivateSilhouette();
            TimeCountReset();
            _countStart = false;
        }
    }

    void TimeCountReset()
    {
        _standTime = _startStandTime;
    }
}
