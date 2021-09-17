using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OutlineRendererOperator : MonoBehaviour
{
    public ForwardRendererData outlineRendererData;
    OutlineRenderer LHandleOutlineRenderer;
    OutlineRenderer RHandleOutlineRenderer;
    // �A�E�g���C�����������I�u�W�F�N�g��Renderer�̃��X�g
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
