using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

/// <summary>
/// ����Boolean���f�����O�̈��������̂ݗ��p���Ă���B
/// ���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g��������鑤�̃I�u�W�F�N�g�ƂȂ�
/// �����ꂽ�����̃}�e���A���͈������I�u�W�F�N�g�̃}�e���A�����K�p�����
/// </summary>
public class CSGCaller : MonoBehaviour
{
    public GameObject subtracter;
    [HideInInspector] public GameObject composite;
    /// <summary>
    /// Dissolve�}�e���A��������
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
		 * barycentric�Ƃ�"�d�S��"�Ƃ����Ӗ�
		 */
    void GenerateBarycentric(GameObject go)
    {
        Mesh m = go.GetComponent<MeshFilter>().sharedMesh;//go��mesh�f�[�^��ǂݍ��݁Am�ɑ�����Ă���

        if (m == null) return;

        int[] tris = m.triangles;
        int triangleCount = tris.Length;//�e�O�p�`�̒��_�̌�

        Vector3[] mesh_vertices = m.vertices;
        Vector3[] mesh_normals = m.normals;
        Vector2[] mesh_uv = m.uv;

        Vector3[] vertices = new Vector3[triangleCount];
        Vector3[] normals = new Vector3[triangleCount];
        Vector2[] uv = new Vector2[triangleCount];
        Color[] colors = new Color[triangleCount];

        for (int i = 0; i < triangleCount; i++)
        {
            vertices[i] = mesh_vertices[tris[i]];//���̎O�p�`�̒��_�z��̏��Ԃ�mesh�̒��_�ԍ������������Ă���Bmemo�Q��"Assets/pb_CSG-master/CSG/Classes/StudyMemo/Demo_cs_GenerateBarycentric_for.jpg"
            normals[i] = mesh_normals[tris[i]];
            uv[i] = mesh_uv[tris[i]];

            colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);

            tris[i] = i;//�O�p�`�̒��_���A��ŏ������������_�ԍ��ɑΉ�����悤�ɕύX���Ă���
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
