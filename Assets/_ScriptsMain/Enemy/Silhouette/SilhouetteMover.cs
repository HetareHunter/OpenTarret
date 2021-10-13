using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �^�[�Q�b�g�ł���V���G�b�g���N��������|�����肷��N���X�BDoTween�ŃA�j���[�V���������Ă���
/// �l�̃V���G�b�g��Position�������Ȃ�
/// </summary>
public class SilhouetteMover : MonoBehaviour, IMovableSilhouette
{
    [SerializeField] float _rotateTime = 1.0f;

    /// <summary>
    /// �����Ă��鎞��
    /// </summary>
    [SerializeField] float _activeTime = 8.0f;

    SilhouetteActivatior _silhouetteActivatior;
    /// <summary>
    /// ���݂̃X�^���h�̏��
    /// </summary>
    SilhouetteStandState _standState = SilhouetteStandState.Down;
    // Start is called before the first frame update
    void Start()
    {
        _silhouetteActivatior = GetComponentInChildren<SilhouetteActivatior>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// �V���G�b�g�̉�]������]�����郁�\�b�h
    /// </summary>
    /// <param name="standState">���ꂩ�炱�̏�Ԃɂ������Ƃ���hikisuu
    /// </param>
    /// <param name="rotateTime"></param>
    public void StandSilhouette(SilhouetteStandState standState, float rotateTime)
    {
        if (_standState == SilhouetteStandState.Down && standState == SilhouetteStandState.Up)//�N���オ�鎞�̏���
        {
            transform.DORotate(new Vector3(-90, 0, 0), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        else if (_standState == SilhouetteStandState.Up && standState == SilhouetteStandState.Down)//�|��鎞�̏���
        {
            transform.DORotate(new Vector3(90, 0, 0), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        _standState = standState;
    }
}
