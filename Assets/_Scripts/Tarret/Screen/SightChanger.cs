using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightChanger : MonoBehaviour
{
    Image m_sightTex;
    [SerializeField] Sprite baseSightTex;
    [SerializeField] Sprite redSightTex;
    [SerializeField] Material baseSigjtMT;
    [SerializeField] Material redSigjtMT;

    private void Start()
    {
        m_sightTex = GetComponent<Image>();
    }

    //public void ChangeTexture()
    //{
    //    if (sightMaterial.mainTexture == baseSightTex)
    //    {
    //        sightMaterial.SetTexture("_BaseMap", RedSightTex);
    //    }
    //    else
    //    {
    //        sightMaterial.SetTexture("_BaseMap", baseSightTex);
    //    }
    //}

    public void ChangeBase()
    {
        m_sightTex.sprite = baseSightTex;
        m_sightTex.material = baseSigjtMT;
    }

    public void ChangeRed()
    {
        m_sightTex.sprite = redSightTex;
        m_sightTex.material = redSigjtMT;
    }

    /// <summary>
    /// アプリケーション終了時に呼び出され、マテリアルのテクスチャを初期状態に戻す
    /// </summary>
    //private void OnApplicationQuit()
    //{
    //    sightTex.SetTexture("_BaseMap", baseSightTex);
    //}
}
