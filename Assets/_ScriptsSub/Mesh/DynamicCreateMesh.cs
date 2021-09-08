using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]//MeshRendererの用意。メッシュを表示するにはMeshRendererまたはSkiningMeshRendererが必要
[RequireComponent(typeof(MeshFilter))]//MeshRendererと同様に必要。MeshをMeshRendererに渡すため
public class DynamicCreateMesh : MonoBehaviour
{
    [SerializeField] Material _mat;
    private void Start()
    {
        //Meshインスタンスを作成
        var mesh = new Mesh();
        //meshに頂点座標配列を渡す。アタッチしたGameObjectのローカル座標が原点となる
        mesh.vertices = new Vector3[] {
            new Vector3 (0, 1f),
            new Vector3 (1f, -1f),
            new Vector3 (-1f, -1f),
            new Vector3 (0.2f, 1f),
            new Vector3 (2.2f, 1f),
            new Vector3 (1.2f, -1f),
        };
        //meshに頂点の順番配列を渡す。頂点の描画順となる。unityは時計回りに頂点を結んだ面が前面となる
        mesh.triangles = new int[] {
            0, 1, 2,
            3,4,5
        };
        //法線方向の再計算を行う。これを呼ばないと法線方向が(0,0,1)固定になってしまう
        mesh.RecalculateNormals();

        //float texSize = 256f;
        //mesh.uv = new Vector2[]
        //{
        //    new Vector2(86f/texSize,100f/texSize),
        //    new Vector2(116f/texSize,42f/texSize),
        //    new Vector2(60f/texSize,42f/texSize)
        //};

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        var renderer = GetComponent<MeshRenderer>();
        renderer.material = _mat;
    }
}