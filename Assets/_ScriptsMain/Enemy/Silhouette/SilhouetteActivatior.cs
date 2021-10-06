using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteActivatior : MonoBehaviour, IEnemyDeath
{
    [SerializeField] int _addScore = 100;
    [SerializeField] float _deathStandDownTime = 0.5f;
    [SerializeField] float _activateStandUpTime = 1.0f;
    bool isActive = false;
    Collider _collider;
    SilhouetteMover _silhouetteHumanMover;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            _collider.enabled = true;
        }
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
