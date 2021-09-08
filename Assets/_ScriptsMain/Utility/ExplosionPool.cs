using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class ExplosionPool : ObjectPool<ExplosionForce>
{
    private readonly ExplosionForce _original;

    /// <summary>
    /// �I���W�i����n���ď�����
    /// </summary>
    public ExplosionPool(ExplosionForce original)
    {
        //�I���W�i���͔�\����
        _original = original;
        //_original.gameObject.SetActive(false);
    }

    //�C���X�^���X����鏈��
    protected override ExplosionForce CreateInstance()
    {
        //�I���W�i���𕡐����ăC���X�^���X�쐬(�I���W�i���Ɠ����e�̉��ɔz�u)
        return ExplosionForce.Instantiate(_original, _original.transform.parent);
    }

    //�v�[������I�u�W�F�N�g���擾����O�Ɏ��s�����
    protected override void OnBeforeRent(ExplosionForce instance)
    {
        Debug.Log($"{instance.name}���v�[��������o����܂���");
        base.OnBeforeRent(instance);
    }

    //�I�u�W�F�N�g���v�[���ɖ߂�O�Ɏ��s�����
    protected override void OnBeforeReturn(ExplosionForce instance)
    {
        Debug.Log($"{instance.name}���v�[���ɖ߂���܂���");
        base.OnBeforeReturn(instance);
    }
}
