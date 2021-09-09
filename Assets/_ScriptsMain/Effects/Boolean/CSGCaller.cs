using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

/// <summary>
/// 現状Booleanモデリングの引く処理のみ利用している。
/// このスクリプトをアタッチしたオブジェクトが引かれる側のオブジェクトとなる
/// 引かれた部分のマテリアルは引いたオブジェクトのマテリアルが適用される
/// </summary>
public class CSGCaller : MonoBehaviour
{
    public GameObject subtracter;
    [HideInInspector] public GameObject composite;
    /// <summary>
    /// Dissolveマテリアルを入れる
    /// </summary>
    [SerializeField] Material _Dissolve_MT;

    enum BoolOp
    {
        Union,
        SubtractLR,
        SubtractRL,
        Intersect
    };


    // Start is called before the first frame update
    void Start()
    {
        //SubtractionLR();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Union()
    {
        DoBooleanOperation(BoolOp.Union);
    }

    public void SubtractionLR()
    {
        DoBooleanOperation(BoolOp.SubtractLR);
    }

    public void SubtractionRL()
    {
        DoBooleanOperation(BoolOp.SubtractRL);
    }

    public void Intersection()
    {
        DoBooleanOperation(BoolOp.Intersect);
    }

    void DoBooleanOperation(BoolOp operation)
    {
        CSG_Model result;

        /**
         * All boolean operations accept two gameobjects and return a new mesh.
         * Order matters - left, right vs. right, left will yield different
         * results in some cases.
         */
        switch (operation)
        {
            case BoolOp.Union:
                result = Boolean.Union(gameObject, subtracter);
                break;

            case BoolOp.SubtractLR:
                result = Boolean.Subtract(gameObject, subtracter);
                break;

            case BoolOp.SubtractRL:
                result = Boolean.Subtract(subtracter, gameObject);
                break;

            default:
                result = Boolean.Intersect(subtracter, gameObject);
                break;
        }

        composite = new GameObject();
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
        //composite.AddComponent<MeshRenderer>().sharedMaterial = _Dissolve_MT;
        composite.AddComponent<MeshDissolver>();

        GenerateBarycentric(gameObject);

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    /**
		 * Rebuild mesh with individual triangles, adding barycentric coordinates
		 * in the colors channel.  Not the most ideal wireframe implementation,
		 * but it works and didn't take an inordinate amount of time :)
		 * barycentricとは"重心の"という意味
		 */
    void GenerateBarycentric(GameObject go)
    {
        Mesh m = go.GetComponent<MeshFilter>().sharedMesh;//goのmeshデータを読み込み、mに代入している

        if (m == null) return;

        int[] tris = m.triangles;
        int triangleCount = tris.Length;//各三角形の頂点の個数

        Vector3[] mesh_vertices = m.vertices;
        Vector3[] mesh_normals = m.normals;
        Vector2[] mesh_uv = m.uv;

        Vector3[] vertices = new Vector3[triangleCount];
        Vector3[] normals = new Vector3[triangleCount];
        Vector2[] uv = new Vector2[triangleCount];
        Color[] colors = new Color[triangleCount];

        for (int i = 0; i < triangleCount; i++)
        {
            vertices[i] = mesh_vertices[tris[i]];//一つ一つの三角形の頂点配列の順番にmeshの頂点番号を書き換えている。memo参照"Assets/pb_CSG-master/CSG/Classes/StudyMemo/Demo_cs_GenerateBarycentric_for.jpg"
            normals[i] = mesh_normals[tris[i]];
            uv[i] = mesh_uv[tris[i]];

            colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);

            tris[i] = i;//三角形の頂点も、上で書き換えた頂点番号に対応するように変更している
        }

        Mesh wireframeMesh = new Mesh();

        wireframeMesh.Clear();
        wireframeMesh.vertices = vertices;
        wireframeMesh.triangles = tris;
        wireframeMesh.normals = normals;
        wireframeMesh.colors = colors;
        wireframeMesh.uv = uv;

        go.GetComponent<MeshFilter>().sharedMesh = wireframeMesh;
    }


}
