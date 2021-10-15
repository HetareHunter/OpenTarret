using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SilhouetteActivatior : EnemyDeath
{
    [SerializeField] int _addScore = 100;
    [SerializeField] float _deathStandDownTime = 0.5f;
    [SerializeField] float _activateStandUpTime = 1.0f;
    bool _isActive = false;
    Collider _collider;
    SilhouetteMover _silhouetteMover;
    [SerializeField] float _standTime = 6.0f;
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
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownTime);
        TimeCountReset();
    }

    private void Start()
    {
        _silhouetteMover = transform.parent.GetComponent<SilhouetteMover>();
        _collider = GetComponent<Collider>();

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
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownTime);
    }

    public override void AddScore()
    {
        ScoreManager.Instance.AddScore(_addScore);
    }

    public void ActivateSilhouette()
    {
        IsActive = true;
        _collider.enabled = true;
        TimeCountReset();
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Up, _activateStandUpTime);
        _silhouetteMover.DoRestart();
        //_countStart = true;
    }

    public void NonActivateSilhouette()
    {
        IsActive = false;
        _collider.enabled = false;
        _spawnable.ChangeEnemyNum(-1);
        //TimeCountReset();
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Down, _activateStandUpTime);
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
