using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeath : MonoBehaviour, IEnemyDeath
{
    [SerializeField] float _deathTime = 0.5f;
    [SerializeField] int _addScore = 100;

    Rigidbody _rb;
    Collider collider;
    float _strongDrag = 4.0f;
    float _strongAnglarDrag = 4.0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void OnDead()
    {
        AddScore();
        GetComponent<MeshDissolver>().ISPlayDissolve(true);
        _rb.useGravity = false;
    }

    public void AddScore()
    {
        ScoreManager.Instance.AddScore(_addScore);
    }

    public void AfterDeadRigidBody()
    {
        _rb.drag = _strongDrag;
        _rb.angularDrag = _strongAnglarDrag;
    }

    public void IsCollisionEnabled(bool enabled)
    {
        collider.enabled = enabled;
    }
}
