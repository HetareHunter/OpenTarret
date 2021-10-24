using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeath : EnemyDeath
{
    [SerializeField] int _addScore = 100;

    Rigidbody _rb;
    Collider collider;
    [SerializeField] float _strongDrag = 4.0f;
    [SerializeField] float _strongAnglarDrag = 4.0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public override void OnDead()
    {
        AddScore();
        IsCollisionEnabled(false);
        AfterDeadRigidBody();
        GetComponent<MeshDissolver>().IsPlayDissolve(true);
        _rb.useGravity = false;
    }

    public override void AddScore()
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
