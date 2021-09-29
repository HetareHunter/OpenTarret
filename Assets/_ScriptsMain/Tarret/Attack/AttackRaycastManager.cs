using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;

public class AttackRaycastManager : MonoBehaviour
{
    /// <summary>レイキャストの長さ </summary>
    [SerializeField] Vector3 maxRayDistance;
    [SerializeField] GameObject defaultRazerFinishPosition;
    /// <summary>レイの原点。ここから伸びていく </summary>
    [SerializeField] GameObject rayOfOrigin;
    /// <summary> 当たったオブジェクトの情報を入れておく変数 </summary>
    List<RaycastHit> _hitsEnemy;
    BaseTarretAttackManager _BaseTarretAttacker;

    ObjectPool _objectPool;
    [Header("ヒットしたとき生成するオブジェクト、オブジェクトプールに設定するもの")]
    [SerializeField] GameObject _objectPoolObj;
    [SerializeField] GameObject _explodeForceEffect;
    [SerializeField] int _explodeForceEffectMax;

    [SerializeField] GameObject _explodeEffect;
    [SerializeField] int _explodeEffectMax;

    float _muzzleRadius;
    [Header("当たるとレーザーがそのオブジェクトを貫通しないレイヤー")]
    public LayerMask _noPenetrationLayer;
    public int _PeneLayerMaskNum;
    [SerializeField] GameObject tarret;

    // Start is called before the first frame update
    void Start()
    {
        _BaseTarretAttacker = tarret.GetComponent<BaseTarretAttackManager>();

        //　弾の半径を取得
        _muzzleRadius = GetComponent<SphereCollider>().radius;
        _hitsEnemy = new List<RaycastHit>();
        _objectPool = _objectPoolObj.GetComponent<ObjectPool>();
        _objectPool.CreatePool(_explodeForceEffect, _explodeForceEffectMax);
        _objectPool.CreatePool(_explodeEffect, _explodeEffectMax);

        int maskNum = _noPenetrationLayer;
        int layerNum = 0;
        while (maskNum > 0)
        {
            maskNum >>= 1;
            if (maskNum <= 0)
            {
                _PeneLayerMaskNum = layerNum;
                break;
            }
            layerNum++;
        }
    }

    void FixedUpdate()
    {
        RaySearchObject();
    }

    /// <summary>
    /// レイを飛ばして当たったオブジェクトが何なのかを判定する関数
    /// </summary>
    void RaySearchObject()
    {
        _hitsEnemy.Clear();
        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(transform.position, transform.forward);

        //　Sphereの形でレイを飛ばしEnemy、GameManagerレイヤーのオブジェクトをm_hitsEnemyに入れる
        //RaycastAll系は取得したオブジェクトを一番遠いものから順に配列に格納していくので、0に近い要素数程遠いものになる
        var hits = Physics.SphereCastAll(ray, _muzzleRadius, maxRayDistance.z, LayerMask.GetMask("Enemy", "Stage", "TutorialTarget"));
        for (int i = 0; i < hits.Length; i++)
        {
            _hitsEnemy.Add(hits[i]);
        }
        _BaseTarretAttacker.ScreenChangeColor(_hitsEnemy);
    }

    public Vector3 FinishHitPosition()
    {

        if (_hitsEnemy.Count == 0)
        {
            return defaultRazerFinishPosition.transform.position;
        }
        _hitsEnemy.Sort((a, b) => a.distance.CompareTo(b.distance));
        for (int i = 0; i < _hitsEnemy.Count; i++)
        {
            if (_hitsEnemy[i].collider.gameObject.layer == _PeneLayerMaskNum)
            {
                if (i != _hitsEnemy.Count - 1)
                {
                    _hitsEnemy.RemoveRange(i + 1, _hitsEnemy.Count - 1 - (i + 1) + 1);
                }

                return _hitsEnemy[i].point;
            }
        }

        return defaultRazerFinishPosition.transform.position;
    }

    public List<RaycastHit> SetRaycastHit()
    {
        return _hitsEnemy;
    }

    /// <summary>
    /// 攻撃したときの具体的な処理、現在タグで区別している
    /// </summary>
    public void KillEnemyFromRazer()
    {
        var hits = SetRaycastHit();
        foreach (var hit in hits)
        {
            //爆発したときの力となるオブジェクトの生成
            _objectPool.GetObject(_explodeForceEffect, hit.point, Quaternion.identity);
            _objectPool.GetObject(_explodeEffect, hit.point, Quaternion.identity);
            if (hit.collider.gameObject.layer != _PeneLayerMaskNum)
            {
                IEnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<IEnemyDeath>();
                enemyDeath.OnDead();
            }
        }
    }
}
