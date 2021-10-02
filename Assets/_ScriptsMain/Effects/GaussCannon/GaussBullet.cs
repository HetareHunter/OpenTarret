using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaussBullet : MonoBehaviour
{
    Rigidbody _rb;
    Collider _collider;
    [SerializeField] float cannonPower = 5.0f;
    [SerializeField] float effectiveRange = 300.0f;
    [SerializeField] float dissolveTime = 1.0f;

    ObjectPool _objectPool;
    [Header("オブジェクトプールに設定するもの")]
    [SerializeField] GameObject _explodeEffect;
    [SerializeField] int _startExplodeEffectNum;

    Vector3 startPosi;
    Quaternion startRota;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _objectPool = GetComponent<ObjectPool>();
        Reset();
        //MoveFoward(ca);
    }

    // Start is called before the first frame update
    void Start()
    {
        _objectPool.CreatePool(_explodeEffect, _startExplodeEffectNum);
    }

    private void OnEnable()
    {
        startPosi = transform.position;
        startRota = transform.rotation;
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        Reset();
        MoveFoward(cannonPower);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        FadeGaussBullet(startPosi, effectiveRange);
    }

    private void Reset()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _collider.enabled = true;
    }

    /// <summary>
    /// 発射位置からの距離で消えるようにしている
    /// </summary>
    /// <param name="startBulletPosi"></param>
    /// <param name="maxEffectiveRange"></param>
    void FadeGaussBullet(Vector3 startBulletPosi, float maxEffectiveRange)
    {
        if (Vector3.Distance(transform.position, startBulletPosi) > maxEffectiveRange)
        {
            Reset();
            gameObject.SetActive(false);
        }
    }

    public void MoveFoward(float speed)
    {
        _rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        var collisionBlockDeath = collision.gameObject.GetComponent<BlockDeath>();
        _objectPool.GetObject(_explodeEffect, transform.position, Quaternion.identity);

        if (collisionBlockDeath != null)
        {
            collisionBlockDeath.IsCollisionEnabled(false);
            collisionBlockDeath.AfterDeadRigidBody();
            collisionBlockDeath.OnDead();

            transform.rotation = startRota;
            MoveFoward(cannonPower);
        }
        else if (collision.transform.CompareTag("Ground"))
        {
            _collider.enabled = false;
            MoveFoward(0);
            DOVirtual.DelayedCall(dissolveTime, () =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
