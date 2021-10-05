using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteHumanDeath : MonoBehaviour, IEnemyDeath
{
    [SerializeField] int _addScore = 100;
    [SerializeField] float _deathStandDownTime = 0.5f;
    Collider _collider;
    SilhouetteHumanMover _silhouetteHumanMover;
    private void Start()
    {
        _silhouetteHumanMover = transform.parent.GetComponent<SilhouetteHumanMover>();
        _collider = GetComponent<Collider>();
    }
    public void OnDead()
    {
        AddScore();
        _collider.enabled = false;
        _silhouetteHumanMover.StandSilhouette(SilhouetteStandState.Down, _deathStandDownTime);
    }

    public void AddScore()
    {
        ScoreManager.Instance.AddScore(_addScore);
    }
}
