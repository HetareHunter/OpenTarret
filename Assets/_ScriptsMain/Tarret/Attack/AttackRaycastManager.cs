using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;

public class AttackRaycastManager : MonoBehaviour
{
    /// <summary>レイキャストの長さ </summary>
    [SerializeField] Vector3 maxRayDistance;
    Vector3 rayDistance;
    /// <summary>レイの原点。ここから伸びていく </summary>
    [SerializeField] GameObject rayOfOrigin;
    /// <summary> 当たったオブジェクトの情報を入れておく変数 </summary>
    List<RaycastHit> m_hitsEnemy;
    [SerializeField] int hitsNum = 100;
    BaseTarretAttackManager BaseTarretAttacker;
    [SerializeField] GameObject muzzle;
    float muzzleRadius;
    [Header("当たるとレーザーがそのオブジェクトを貫通しないレイヤー")]
    public LayerMask noPenetrationLayer;
    public int PeneLayerMaskNum;


    // Start is called before the first frame update
    void Start()
    {
        BaseTarretAttacker = GetComponent<BaseTarretAttackManager>();

        //　弾の半径を取得
        muzzleRadius = muzzle.GetComponent<SphereCollider>().radius;
        m_hitsEnemy = new List<RaycastHit>();

        //PeneLayerMaskNum = LayerMask.NameToLayer(noPenetrationLayer.ToString());
        int maskNum = noPenetrationLayer;
        int layerNum = 0;
        while (maskNum > 0)
        {
            maskNum >>= 1;
            if (maskNum <= 0)
            {
                PeneLayerMaskNum = layerNum;
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
        m_hitsEnemy.Clear();
        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        //　Sphereの形でレイを飛ばしEnemy、GameManagerレイヤーのオブジェクトをm_hitsEnemyに入れる
        var hits = Physics.SphereCastAll(ray, muzzleRadius, maxRayDistance.z, LayerMask.GetMask("Enemy", "Stage"));
        for (int i = 0; i < hits.Length; i++)
        {
            m_hitsEnemy.Add(hits[i]);
            if (hits[i].collider.gameObject.layer == PeneLayerMaskNum)
            {
                break;
            }
        }

        BaseTarretAttacker.ScreenChangeColor(m_hitsEnemy);
        //m_hitsEnemy.Clear();
    }

    public List<RaycastHit> SetRaycastHit()
    {
        return m_hitsEnemy;
    }
}
