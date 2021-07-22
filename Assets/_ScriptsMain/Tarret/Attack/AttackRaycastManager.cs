using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;

public class AttackRaycastManager : MonoBehaviour
{
    /// <summary>���C�L���X�g�̒��� </summary>
    [SerializeField] Vector3 maxRayDistance;
    [SerializeField] GameObject defaultRazerFinishPosition;
    /// <summary>���C�̌��_�B��������L�тĂ��� </summary>
    [SerializeField] GameObject rayOfOrigin;
    /// <summary> ���������I�u�W�F�N�g�̏������Ă����ϐ� </summary>
    List<RaycastHit> m_hitsEnemy;
    BaseTarretAttackManager BaseTarretAttacker;
    GuardCubeDamager GuardCubeDamager;
    public GameObject muzzle;
    float muzzleRadius;
    [Header("������ƃ��[�U�[�����̃I�u�W�F�N�g���ђʂ��Ȃ����C���[")]
    public LayerMask noPenetrationLayer;
    public int PeneLayerMaskNum;


    // Start is called before the first frame update
    void Start()
    {
        BaseTarretAttacker = GetComponent<BaseTarretAttackManager>();

        //�@�e�̔��a���擾
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
    /// ���C���΂��ē��������I�u�W�F�N�g�����Ȃ̂��𔻒肷��֐�
    /// </summary>
    void RaySearchObject()
    {
        m_hitsEnemy.Clear();
        //�@��΂��ʒu�Ɣ�΂�������ݒ�
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        //�@Sphere�̌`�Ń��C���΂�Enemy�AGameManager���C���[�̃I�u�W�F�N�g��m_hitsEnemy�ɓ����
        //RaycastAll�n�͎擾�����I�u�W�F�N�g����ԉ������̂��珇�ɔz��Ɋi�[���Ă����̂ŁA0�ɋ߂��v�f�����������̂ɂȂ�
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
