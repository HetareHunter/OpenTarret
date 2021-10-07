using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SilhouetteActivatior : MonoBehaviour, IEnemyDeath
{
    [SerializeField] int _addScore = 100;
    [SerializeField] float _deathStandDownTime = 0.5f;
    [SerializeField] float _activateStandUpTime = 1.0f;
    bool isActive = false;
    Collider _collider;
    SilhouetteMover _silhouetteHumanMover;
    [Inject]
    ISpawnable spawnable;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if (value == true)
            {
                _collider.enabled = true;
            }
        }
    }

    public void Reset()
    {
        IsActive = false;
        _silhouetteHumanMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownTime);
    }

    private void Start()
    {
        _silhouetteHumanMover = transform.parent.GetComponent<SilhouetteMover>();
        _collider = GetComponent<Collider>();
    }
    public void OnDead()
    {
        IsActive = false;
        AddScore();
        _collider.enabled = false;
        spawnable.ChangeEnemyNum(-1);
        _silhouetteHumanMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownTime);
    }

    public void AddScore()
    {
        ScoreManager.Instance.AddScore(_addScore);
    }

    public void Activate()
    {
        IsActive = true;
        _collider.enabled = true;
        _silhouetteHumanMover.StandSilhouette(SilhouetteStandState.Up, _activateStandUpTime);
    }
}
