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
    ExplosionForce explosionForce;
    //public GameObject muzzle;
    float muzzleRadius;
    [Header("当たるとレーザーがそのオブジェクトを貫通しないレイヤー")]
    public LayerMask noPenetrationLayer;
    public int PeneLayerMaskNum;
    [SerializeField] GameObject tarret;

    /// <summary>爆発のエフェクト </summary>
    [SerializeField] GameObject[] m_hitExplodeEffects;

    int hitExplodeIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        BaseTarretAttacker = tarret.GetComponent<BaseTarretAttackManager>();

        //　弾の半径を取得
        muzzleRadius = GetComponent<SphereCollider>().radius;
        m_hitsEnemy = new List<RaycastHit>();
        explosionForce = GetComponent<ExplosionForce>();

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
        Ray ray = new Ray(transform.position, transform.forward);

        //　Sphereの形でレイを飛ばしEnemy、GameManagerレイヤーのオブジェクトをm_hitsEnemyに入れる
        //RaycastAll系は取得したオブジェクトを一番遠いものから順に配列に格納していくので、0に近い要素数程遠いものになる
        var hits = Physics.SphereCastAll(ray, muzzleRadius, maxRayDistance.z, LayerMask.GetMask("Enemy", "Stage", "TutorialTarget"));
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

        return defaultRazerFinishPosition.transform.position;
    }

    public List<RaycastHit> SetRaycastHit()
    {
        return m_hitsEnemy;
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
            explosionForce.ActiveExplosionForce(hit.point);
            PlayHitExplodeEffect(hit.point);
            if (hit.collider.gameObject.layer != PeneLayerMaskNum)
            {
                IEnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<IEnemyDeath>();
                enemyDeath.OnDead();
            }


            if (hitExplodeIndex >= m_hitExplodeEffects.Length)
            {
                hitExplodeIndex = 0;
            }
        }
    }

    void PlayHitExplodeEffect(Vector3 hit)
    {
        //爆発エフェクトの再生
        m_hitExplodeEffects[hitExplodeIndex].transform.position = hit;
        m_hitExplodeEffects[hitExplodeIndex].SetActive(true);
        hitExplodeIndex++;
    }
}
