using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussBullet : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float cannonPower = 5.0f;
    [SerializeField] float effectiveRange = 300.0f;

    ObjectPool _objectPool;
    [Header("オブジェクトプールに設定するもの")]
    [SerializeField] GameObject _explodeEffect;
    [SerializeField] int _startExplodeEffectNum;

    Vector3 startPosi;
    Quaternion startRota;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _objectPool = GetComponent<ObjectPool>();
        _objectPool.CreatePool(_explodeEffect, _startExplodeEffectNum);
    }

    private void OnEnable()
    {
        startPosi = transform.position;
        startRota = transform.rotation;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * cannonPower);
        _rb.velocity = transform.forward * cannonPower;
        FadeGaussBullet(startPosi, effectiveRange);
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
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        var collisionBlockDeath = collision.gameObject.GetComponent<BlockDeath>();

        if (collisionBlockDeath != null)
        {
            _objectPool.GetObject(_explodeEffect, transform.position, Quaternion.identity);
            collisionBlockDeath.IsCollisionEnabled(false);
            collisionBlockDeath.AfterDeadRigidBody();
            collisionBlockDeath.OnDead();

            transform.rotation = startRota;
        }
    }
}
