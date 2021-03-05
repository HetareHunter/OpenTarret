using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightChanger : MonoBehaviour
{
    Image m_sightTex;
    [SerializeField] Sprite baseSightTex;
    [SerializeField] Sprite RedSightTex;

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

    public void ChangeBaseTex()
    {
        m_sightTex.sprite = baseSightTex;
    }

    public void ChangeRedTex()
    {
        m_sightTex.sprite = RedSightTex;
    }

    /// <summary>
    /// アプリケーション終了時に呼び出され、マテリアルのテクスチャを初期状態に戻す
    /// </summary>
    //private void OnApplicationQuit()
    //{
    //    sightTex.SetTexture("_BaseMap", baseSightTex);
    //}
}
