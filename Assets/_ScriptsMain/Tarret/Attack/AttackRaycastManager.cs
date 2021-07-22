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
    List<RaycastHit> m_hitsEnemy;
    BaseTarretAttackManager BaseTarretAttacker;
    GuardCubeDamager GuardCubeDamager;
    public GameObject muzzle;
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
        //RaycastAll系は取得したオブジェクトを一番遠いものから順に配列に格納していくので、0に近い要素数程遠いものになる
        var hits = Physics.SphereCastAll(ray, muzzleRadius, maxRayDistance.z, LayerMask.GetMask("Enemy", "Stage"));
        for (int i = 0; i < hits.Length; i++)
        {
            m_hitsEnemy.Add(hits[i]);

        }
        BaseTarretAttacker.ScreenChangeColor(m_hitsEnemy);
    }

    public Vector3 FinishHitPosition()
    {

        if (m_hitsEnemy.Count == 0)
        {
            return defaultRazerFinishPosition.transform.position;
        }
        m_hitsEnemy.Sort((a, b) => a.distance.CompareTo(b.distance));
        for (int i = 0; i < m_hitsEnemy.Count; i++)
        {
            if (m_hitsEnemy[i].collider.gameObject.layer == PeneLayerMaskNum)
            {
                if (i != m_hitsEnemy.Count - 1)
                {
                    m_hitsEnemy.RemoveRange(i + 1, m_hitsEnemy.Count - 1 - (i + 1) + 1);
                }

                return m_hitsEnemy[i].point;
            }
        }
        foreach (var item in m_hitsEnemy)
        {

        }
        return defaultRazerFinishPosition.transform.position;
    }

    public List<RaycastHit> SetRaycastHit()
    {
        return m_hitsEnemy;
    }


}
