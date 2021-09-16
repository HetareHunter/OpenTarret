using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineRendererPass : ScriptableRenderPass
{

    const string NAME = nameof(OutlineRendererPass);

    Material outlineMaterial = null;
    RenderTargetIdentifier renderTargetID = default;
    OutlineRenderer parentRendererFeature;

    public OutlineRendererPass(OutlineRenderer parentRendererFeature, Material outlineMaterial)
    {
        // �����ł��̃p�X���ǂ̃^�C�~���O�ő}�����邩���߂�
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        this.parentRendererFeature = parentRendererFeature;
        this.outlineMaterial = outlineMaterial;
    }

    public void SetRenderTarget(RenderTargetIdentifier renderTargetID)
    {
        this.renderTargetID = renderTargetID;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {

        // �ꌩ����Ȃ��悤�Ɍ����邪�ASceneView�ؑ֎��ȂǂŃ��Z�b�g����邱�Ƃ�����A�Ȃ��Ɖ�ʂ��}�[���_�̃A���ɂȂ�
        if (parentRendererFeature.contentRenderers == null) return;

        CommandBuffer CB = CommandBufferPool.Get(NAME);
        CameraData camData = renderingData.cameraData;

        int texId = Shader.PropertyToID("_MainTex");
        int w = camData.camera.scaledPixelWidth;
        int h = camData.camera.scaledPixelHeight;
        int shaderPass = 0;

        CB.GetTemporaryRT(texId, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
        CB.SetRenderTarget(texId);
        CB.ClearRenderTarget(false, true, Color.clear);

        // �A�E�g���C���𒅂������I�u�W�F�N�g��`��
        foreach (Renderer renderer in parentRendererFeature.contentRenderers)
        {
            if (renderer == null) continue; // Play�I�����ɁAList�͔j�����ꂸ���̎Q�Ɛ�̂ݔj�������̂�Scene�`��̂��߂ɓ���Ƃ��Ȃ��Ɨ�̃}�[���_(ry
            // �I�u�W�F�N�g�̃}�e���A�����ׂĂł���Forward�p�X�ŕ`��
            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
            {
                CB.DrawRenderer(renderer, renderer.sharedMaterials[i], i, 0);
            }
        }

        CB.Blit(texId, renderTargetID, outlineMaterial, shaderPass);

        context.ExecuteCommandBuffer(CB);
        CommandBufferPool.Release(CB);
    }
}

public class OutlineRenderer : ScriptableRendererFeature
{

    // �C���X�y�N�^�[�Őݒ肷��@�A�E�g���C���`�悷��}�e���A��
    public Material outlineMaterial;
    // �A�E�g���C�����������I�u�W�F�N�g��Renderer�̃��X�g
    public List<Renderer> contentRenderers;

    OutlineRendererPass outlineRenderPass = null;

    public override void Create()
    {
        if (outlineRenderPass == null)
        {
            outlineRenderPass = new OutlineRendererPass(this, outlineMaterial);
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        outlineRenderPass.SetRenderTarget(renderer.cameraColorTarget);
        renderer.EnqueuePass(outlineRenderPass);
    }

}