using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineRendererOperator : MonoBehaviour
{
    public ForwardRendererData outlineRendererData;
    OutlineRenderer outlineRenderer;
    // �A�E�g���C�����������I�u�W�F�N�g��Renderer�̃��X�g
    public List<Renderer> contentRenderers;

    // Start is called before the first frame update
    void Start()
    {
        outlineRenderer = (OutlineRenderer)outlineRendererData.rendererFeatures[0];
        outlineRenderer.contentRenderers = contentRenderers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
