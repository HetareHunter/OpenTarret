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

        //メッシュ破壊の始まる中心。ここから弾丸がガラスを打ち抜いたときのように円状に亀裂が入る感じでメッシュを分割したい
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
        //一番近い3つの点を探し、どの面から分割するか決定する
        var pointDistance = new List<PointDistance>();

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            pointDistance.Add(new PointDistance(mesh.vertices[i],
                Vector3.Distance(centerBreakPosi, mesh.vertices[i]),
                i
                ));
        }
        pointDistance = pointDistance.OrderBy(x => x._distance).ToList();//距離の昇順にソート

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
/// 点と点の距離を格納するためだけのクラス。片方の点は固定の点を用いることを想定している
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