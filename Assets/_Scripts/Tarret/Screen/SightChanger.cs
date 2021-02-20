using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightChanger : MonoBehaviour
{
    [SerializeField] Material sightMaterial;
    [SerializeField] Texture baseSightTex;
    [SerializeField] Texture RedSightTex;

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
        sightMaterial.SetTexture("_BaseMap", baseSightTex);
    }

    public void ChangeRedTex()
    {
        sightMaterial.SetTexture("_BaseMap", RedSightTex);
    }

    /// <summary>
    /// アプリケーション終了時に呼び出され、マテリアルのテクスチャを初期状態に戻す
    /// </summary>
    private void OnApplicationQuit()
    {
        sightMaterial.SetTexture("_BaseMap", baseSightTex);
    }
}
