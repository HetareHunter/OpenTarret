using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RazerAttacker : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretAttackData;
    LineRenderer razerLineRenderer;
    /// <summary>�r�[���{�̂̃G�t�F�N�g </summary>
    [SerializeField] GameObject m_razerEffect;
    /// <summary>�r�[���̐����ʒu </summary>
    [SerializeField] GameObject m_razerEffectInsPosi;


    // Start is called before the first frame update
    void Start()
    {
        razerLineRenderer = m_razerEffect.transform.GetChild(0).GetComponent<LineRenderer>();
    }

    /// <summary>
    /// ���[�U�[�̃��C�������̃X�N���v�g�A���݂��Ă�����̂��ړ����āA�A���X�ɏ����Ă����悤�ɂ��Ă���
    /// </summary>
    public void InstanceFireEffect()
    {
        m_razerEffect.transform.position = m_razerEffectInsPosi.transform.position;
        m_razerEffect.transform.rotation = m_razerEffectInsPosi.transform.rotation;
        m_razerEffect.SetActive(true);

        razerLineRenderer.startWidth = tarretAttackData.razerWidth;
        razerLineRenderer.endWidth = tarretAttackData.razerWidth;
        DOTween.To(
            () => razerLineRenderer.startWidth,
            (x) => razerLineRenderer.startWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);
        DOTween.To(
            () => razerLineRenderer.endWidth,
            (x) => razerLineRenderer.endWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);

    }


    public void FadeFire()
    {
        m_razerEffect.SetActive(false);
    }
}
