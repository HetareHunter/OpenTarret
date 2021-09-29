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
    List<RaycastHit> _hitsEnemy;
    BaseTarretAttackManager _BaseTarretAttacker;

    ObjectPool _objectPool;
    [Header("�q�b�g�����Ƃ���������I�u�W�F�N�g�A�I�u�W�F�N�g�v�[���ɐݒ肷�����")]
    [SerializeField] GameObject _objectPoolObj;
    [SerializeField] GameObject _explodeForceEffect;
    [SerializeField] int _explodeForceEffectMax;

    [SerializeField] GameObject _explodeEffect;
    [SerializeField] int _explodeEffectMax;

    float _muzzleRadius;
    [Header("������ƃ��[�U�[�����̃I�u�W�F�N�g���ђʂ��Ȃ����C���[")]
    public LayerMask _noPenetrationLayer;
    public int _PeneLayerMaskNum;
    [SerializeField] GameObject tarret;

    // Start is called before the first frame update
    void Start()
    {
        _BaseTarretAttacker = tarret.GetComponent<BaseTarretAttackManager>();

        //�@�e�̔��a���擾
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
    /// ���C���΂��ē��������I�u�W�F�N�g�����Ȃ̂��𔻒肷��֐�
    /// </summary>
    void RaySearchObject()
    {
        _hitsEnemy.Clear();
        //�@��΂��ʒu�Ɣ�΂�������ݒ�
        Ray ray = new Ray(transform.position, transform.forward);

        //�@Sphere�̌`�Ń��C���΂�Enemy�AGameManager���C���[�̃I�u�W�F�N�g��m_hitsEnemy�ɓ����
        //RaycastAll�n�͎擾�����I�u�W�F�N�g����ԉ������̂��珇�ɔz��Ɋi�[���Ă����̂ŁA0�ɋ߂��v�f�����������̂ɂȂ�
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
    /// �U�������Ƃ��̋�̓I�ȏ����A���݃^�O�ŋ�ʂ��Ă���
    /// </summary>
    public void KillEnemyFromRazer()
    {
        var hits = SetRaycastHit();
        foreach (var hit in hits)
        {
            //���������Ƃ��̗͂ƂȂ�I�u�W�F�N�g�̐���
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
