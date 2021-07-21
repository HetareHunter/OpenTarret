using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tarret;

public class AttackRaycastManager : MonoBehaviour
{
    /// <summary>���C�L���X�g�̒��� </summary>
    [SerializeField] Vector3 maxRayDistance;
    Vector3 rayDistance;
    /// <summary>���C�̌��_�B��������L�тĂ��� </summary>
    [SerializeField] GameObject rayOfOrigin;
    /// <summary> ���������I�u�W�F�N�g�̏������Ă����ϐ� </summary>
    List<RaycastHit> m_hitsEnemy;
    [SerializeField] int hitsNum = 100;
    BaseTarretAttackManager BaseTarretAttacker;
    [SerializeField] GameObject muzzle;
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
