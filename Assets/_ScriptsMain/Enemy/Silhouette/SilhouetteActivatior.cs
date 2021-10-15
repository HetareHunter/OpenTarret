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
    float _standTime = 0.0f;
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
    }

    private void Update()
    {
        TimeCountUp();
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

    public void Activate()
    {
        IsActive = true;
        _collider.enabled = true;
        TimeCountReset();
        _silhouetteMover.StandSilhouette(SilhouetteStandState.Up, _activateStandUpTime);
        _silhouetteMover.DoRestart();
    }

    void TimeCountUp()
    {
        _standTime += Time.deltaTime;
    }

    void TimeCountReset()
    {
        _standTime = 0.0f;
    }
}
