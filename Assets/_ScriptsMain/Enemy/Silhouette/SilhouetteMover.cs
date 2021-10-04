using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �^�[�Q�b�g�ł���V���G�b�g���N��������|�����肷��N���X�BDoTween�ŃA�j���[�V���������Ă���
/// </summary>
public class SilhouetteMover : MonoBehaviour
{
    public enum Stand
    {
        Up,
        Down
    }

    [SerializeField] float _rotateTime = 1.0f;
    /// <summary>
    /// ���݂̃X�^���h�̏��
    /// </summary>
    Stand _standState = Stand.Down;
    // Start is called before the first frame update
    void Start()
    {
        //RotatePivot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            RotatePivot(Stand.Up, _rotateTime);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            RotatePivot(Stand.Down, _rotateTime / 2);
        }
    }

    /// <summary>
    /// �V���G�b�g�̉�]������]�����郁�\�b�h
    /// </summary>
    /// <param name="standState">���ꂩ�炱�̏�Ԃɂ������Ƃ���hikisuu
    /// </param>
    /// <param name="rotateTime"></param>
    public void RotatePivot(Stand standState, float rotateTime)
    {
        if (_standState == Stand.Down && standState == Stand.Up)
        {
            transform.DORotate(new Vector3(-90, 0, 0), rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        else if (_standState == Stand.Up && standState == Stand.Down)
        {
            transform.DORotate(new Vector3(90, 0, 0), rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutBounce);
        }
        _standState = standState;
    }
}
