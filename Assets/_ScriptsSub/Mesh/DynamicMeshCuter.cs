using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class DynamicMeshCuter : MonoBehaviour
{
    Mesh _mesh;
    [SerializeField] int addTriangles = 10;
    [SerializeField] float _radius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;

        _mesh = CreateSimpleCutMesh(_mesh, addTriangles, _radius);
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    //Update is called once per frame
    void Update()
    {
        bool leftClick = Input.GetMouseButton(0);
        if (leftClick)
        {
            _mesh = CreateSimpleCutMesh(_mesh, addTriangles, _radius);
            GetComponent<MeshFilter>().mesh = _mesh;
        }
    }

    Mesh CreateSimpleCutMesh(Mesh mesh, int addTriangles, float radius)
    {
        var addVerts = addTriangles * 3;
        var addVertPoints = new List<Vector3>();
        var vertPoints = new List<Vector3>();
        var normals = new List<Vector3>();
        var triangleList = new List<int>();
        var indecies = new List<int>();

        triangleList = mesh.GetTriangles(0).ToList();
        mesh.GetVertices(vertPoints);
        mesh.GetNormals(normals);

        //���b�V���j��̎n�܂钆�S�B��������e�ۂ��K���X��ł��������Ƃ��̂悤�ɉ~��ɋT�􂪓��銴���Ń��b�V���𕪊�������
        Vector3 breakCenterPosi;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out var hit))
        {
            breakCenterPosi = new Vector3(hit.textureCoord.x, hit.textureCoord.y, -0.5f);
            addVertPoints.Add(breakCenterPosi);
            indecies.Add(indecies.Count);
            triangleList.AddRange(FirstCircle(mesh, breakCenterPosi, addVertPoints.Count - 1));
        }



        vertPoints.AddRange(addVertPoints);

        var newMesh = new Mesh();

        newMesh.SetVertices(vertPoints);
        newMesh.SetIndices(indecies, MeshTopology.Triangles, 0);
        newMesh.SetTriangles(triangleList, 0);
        //newMesh.SetNormals(normals);

        newMesh.RecalculateNormals();
        newMesh.RecalculateBounds();
        return newMesh;
    }

    int[] FirstCircle(Mesh mesh, Vector3 centerBreakPosi, int centerBreakVertIndex)
    {
        //��ԋ߂�3�̓_��T���A�ǂ̖ʂ��番�����邩���肷��
        var pointDistance = new List<PointDistance>();

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            pointDistance.Add(new PointDistance(mesh.vertices[i],
                Vector3.Distance(centerBreakPosi, mesh.vertices[i]),
                i
                ));
        }
        pointDistance = pointDistance.OrderBy(x => x._distance).ToList();//�����̏����Ƀ\�[�g

        var triangles = new int[9];
        for (int i = 0; i < 3; i++)
        {
            triangles[i * 3] = centerBreakVertIndex;
            triangles[i * 3 + 1] = pointDistance[i % 3]._vertIndex;
            triangles[i * 3 + 2] = pointDistance[(i + 1) % 3]._vertIndex;
        }
        return triangles;
    }
}



/// <summary>
/// �_�Ɠ_�̋������i�[���邽�߂����̃N���X�B�Е��̓_�͌Œ�̓_��p���邱�Ƃ�z�肵�Ă���
/// </summary>
class PointDistance
{
    public Vector3 _point;
    public float _distance;
    public int _vertIndex;

    public PointDistance(Vector3 point, float distance, int vertIndex)
    {
        _point = point;
        _distance = distance;
        _vertIndex = vertIndex;
    }
}