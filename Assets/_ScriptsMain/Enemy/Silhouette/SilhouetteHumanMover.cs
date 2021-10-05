using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �^�[�Q�b�g�ł���V���G�b�g���N��������|�����肷��N���X�BDoTween�ŃA�j���[�V���������Ă���
/// �l�̃V���G�b�g��Position�������Ȃ�
/// </summary>
public class SilhouetteHumanMover : MonoBehaviour, IMovableSilhouette
{
    [SerializeField] float _rotateTime = 1.0f;
    SilhouetteHumanDeath _silhouetteHumanDeath;
    /// <summary>
    /// ���݂̃X�^���h�̏��
    /// </summary>
    SilhouetteStandState _standState = SilhouetteStandState.Down;
    // Start is called before the first frame update
    void Start()
    {
        _silhouetteHumanDeath = GetComponentInChildren<SilhouetteHumanDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StandSilhouette(SilhouetteStandState.Up, _rotateTime);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StandSilhouette(SilhouetteStandState.Down, _rotateTime / 2);
        }
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
