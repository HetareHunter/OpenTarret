using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineRendererOperator : MonoBehaviour
{
    public ForwardRendererData outlineRendererData;
    OutlineRenderer LHandleOutlineRenderer;
    OutlineRenderer RHandleOutlineRenderer;
    // アウトラインをつけたいオブジェクトのRendererのリスト
    public List<Renderer> LHandleContentRenderers;
    public List<Renderer> RHandleContentRenderers;

    // Start is called before the first frame update
    void Start()
    {
        LHandleOutlineRenderer = (OutlineRenderer)outlineRendererData.rendererFeatures[0];
        RHandleOutlineRenderer = (OutlineRenderer)outlineRendererData.rendererFeatures[1];
        LHandleOutlineRenderer.contentRenderers = LHandleContentRenderers;
        RHandleOutlineRenderer.contentRenderers = RHandleContentRenderers;
    }
}
