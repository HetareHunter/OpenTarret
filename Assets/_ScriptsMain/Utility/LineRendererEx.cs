using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// ����̃��b�V����`��
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer)), ExecuteInEditMode]
public class LineRendererEx : MonoBehaviour
{

	/// <summary>
	/// �m�[�h�L���p�N���X
	/// </summary>
	[Serializable]
	public class Node
	{
		public Vector3 pos; //���W
		public Vector3 scale;   //�傫��
		public Color color; //�F
							//public Mesh mesh;	//���b�V��

		//Constructor
		public Node()
		{
			this.pos = Vector3.zero;
			scale = Vector3.one;
			color = new Color(1f, 0.5f, 0.2f, 0.5f);
		}
		public Node(Vector3 pos, Vector3 scale, Color color)
		{
			this.pos = pos;
			this.scale = scale;
			this.color = color;
		}
	}
	/// <summary>
	/// �G�b�W�L���p�N���X(?)
	/// </summary>
	public class Edge
	{
		public Mesh mesh;   //���b�V��

		//Constructor
		public Edge()
		{

		}
	}

	[Header("Edge Parameter")]
	[SerializeField, Range(0.001f, 1f)]
	private float edgeSize = 1f;        //�G�b�W�̑傫��
	[SerializeField, Range(2, 16)]
	private int edgeSquare = 4;         //�G�b�W�̊p�`
	[Header("Node Parameter")]
	[SerializeField]
	private Mesh nodeMesh;              //�m�[�h�̃��b�V��
	[SerializeField]
	private Vector3 defaultNodeScale = Vector3.one; //�f�t�H���g�m�[�h�X�P�[��
	[SerializeField]
	private Color defaultNodeColor = Color.white;   //�f�t�H���g�m�[�h�J���[
	[Header("Nodes")]
	[SerializeField]
	private List<Node> nodes;   //�m�[�h���X�g
								//�����p�����[�^
	private MeshFilter mf;      //�\���p
	private List<Edge> edges;   //�G�b�W���X�g

	#region MonoBehaviourEvent
	private void Awake()
	{
		if (mf == null) mf = GetComponent<MeshFilter>();
		if (nodes == null) nodes = new List<Node>();
		if (edges == null) edges = new List<Edge>();
	}
	private void OnValidate()
	{
		Awake();
	}
	#endregion
	#region Function
	/// <summary>
	/// �X�V
	/// </summary>
	public void Apply()
	{
		if (nodes.Count <= 0) return;
		//����̃��b�V�����쐬
		Mesh mesh = CreateLineMesh(nodes, edges);
		mesh.RecalculateNormals();
		mf.mesh = mesh;
	}
	/// <summary>
	/// ���W�̒ǉ�
	/// </summary>
	public void AddPosition(Vector3 pos, Vector3 scale, Color color)
	{
		if (nodeMesh == null) return;
		//�m�[�h�̒ǉ�
		nodes.Add(new Node(pos, scale, color));
		//�G�b�W�̒ǉ�
		if (nodes.Count > 1)
		{
			edges.Add(new Edge());
		}
	}
	/// <summary>
	/// ���W�̒ǉ�
	/// </summary>
	public void AddPosition(Vector3 pos)
	{
		AddPosition(pos, defaultNodeScale, defaultNodeColor);
	}
	/// <summary>
	/// ���W�̑}��
	/// </summary>
	public void InsertPosition(int index, Vector3 pos, Vector3 scale, Color color)
	{
		if (nodeMesh == null) return;
		//�m�[�h�̑}��
		nodes.Insert(index, new Node(pos, scale, color));
		//�G�b�W�̑}��
		if (nodes.Count > 1)
		{
			edges.Insert(index, new Edge());
		}
	}
	/// <summary>
	/// ���W�̑}��
	/// </summary>
	public void InsertPosition(int index, Vector3 pos)
	{
		InsertPosition(index, pos, defaultNodeScale, defaultNodeColor);
	}
	/// <summary>
	/// ���W�̍폜
	/// </summary>
	public void RemoveAtPosition(int index)
	{
		if (index < 0 || nodes.Count <= index) return;
		//�m�[�h�̍폜
		nodes.RemoveAt(index);
		//�G�b�W�̍폜,�C��
		if (edges.Count > 0)
		{
			if (nodes.Count == index)
			{
				edges.RemoveAt(index - 1);
			}
			else
			{
				edges.RemoveAt(index);
			}
		}
	}
	/// <summary>
	/// �����̍��W�̍폜
	/// </summary>
	public void RemoveLastPosition()
	{
		RemoveAtPosition(nodes.Count - 1);
	}
	/// <summary>
	/// ������
	/// </summary>
	public void Clear()
	{
		mf.mesh.Clear();
		nodes.Clear();
		edges.Clear();
	}
	#endregion
	#region Mesh
	/// <summary>
	/// ����̃��b�V�����쐬
	/// </summary>
	private Mesh CreateLineMesh(List<Node> nodes, List<Edge> edges)
	{
		//���b�V���̃��X�g���쐬
		List<Mesh> meshs = new List<Mesh>();
		//�ŏ��̃m�[�h
		meshs.Add(CongigureMesh(CopyMesh(nodeMesh), nodes[0].pos, nodes[0].scale, nodes[0].color));
		for (int i = 1; i < nodes.Count; ++i)
		{
			//�G�b�W
			meshs.Add(CreateEdgeMesh(nodes[i - 1], nodes[i], edgeSize, edgeSquare));
			//�m�[�h
			meshs.Add(CongigureMesh(CopyMesh(nodeMesh), nodes[i].pos, nodes[i].scale, nodes[i].color));
		}
		//���b�V���̌���
		return ConnectMesh(meshs);
	}
	/// <summary>
	/// ���b�V���̌���
	/// </summary>
	private Mesh ConnectMesh(List<Mesh> meshs)
	{
		int vertexCount = meshs.Sum(elem => elem.vertexCount);
		Vector3[] vertices = new Vector3[vertexCount];
		Vector2[] uvs = new Vector2[vertexCount];
		Color[] colors = new Color[vertexCount];
		int triangleCount = meshs.Sum(elem => elem.triangles.Length);
		int[] triangles = new int[triangleCount];

		//��������
		int vertexOffset = 0, triangleOffset = 0, index;
		Vector3[] tempVerts;
		Vector2[] tempUvs;
		Color[] tempColors;
		int[] tempTris;
		for (int i = 0; i < meshs.Count; ++i)
		{
			//vertex, uv, color
			tempVerts = meshs[i].vertices;
			tempUvs = meshs[i].uv;
			tempColors = meshs[i].colors;
			for (int j = 0; j < meshs[i].vertexCount; ++j)
			{
				index = j + vertexOffset;
				vertices[index] = tempVerts[j];
				uvs[index] = tempUvs[j];
				colors[index] = tempColors[j];
			}
			//triangle
			tempTris = meshs[i].triangles;
			for (int j = 0; j < meshs[i].triangles.Length; ++j)
			{
				triangles[j + triangleOffset] = tempTris[j] + vertexOffset;
			}
			vertexOffset += meshs[i].vertexCount;
			triangleOffset += meshs[i].triangles.Length;
		}
		//���b�V���ɗ��Ƃ�����
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.colors = colors;
		mesh.triangles = triangles;
		return mesh;
	}
	/// <summary>
	/// ���b�V���̐ݒ�
	/// </summary>
	private Mesh CongigureMesh(Mesh mesh, Vector3 translate, Vector3 scale, Color color)
	{
		Color[] colors = new Color[mesh.vertexCount];
		Vector3[] vertices = mesh.vertices;
		Matrix4x4 transMat = Matrix4x4.TRS(translate, Quaternion.identity, scale);
		for (int i = 0; i < mesh.vertexCount; ++i)
		{
			vertices[i] = transMat.MultiplyPoint(vertices[i]);
			colors[i] = color;
		}
		mesh.vertices = vertices;
		mesh.colors = colors;
		return mesh;
	}
	/// <summary>
	/// ���b�V���̃R�s�[
	/// </summary>
	private Mesh CopyMesh(Mesh src)
	{
		Mesh dst = new Mesh();
		dst.vertices = src.vertices;
		dst.triangles = src.triangles;
		dst.uv = src.uv;
		dst.normals = src.normals;
		dst.colors = src.colors;
		dst.tangents = src.tangents;
		return dst;
	}
	#endregion
	#region EdgeMesh
	/// <summary>
	/// �G�b�W���b�V���̍쐬
	/// </summary>
	private Mesh CreateEdgeMesh(Vector3 from, Color fromColor, Vector3 to, Color toColor, float size, int square = 4)
	{
		Vector3 dir = (to - from).normalized;                       //�����x�N�g��
																	//Quaternion.LookRotation(dir)���݂�
		Vector3 dirVertical = Quaternion.AngleAxis(90, dir) * (Quaternion.LookRotation(dir) * Vector3.right) * size;    //�����x�N�g���ɐ����ȃx�N�g��

		Vector3[] vertices = new Vector3[square * 4];
		Vector2[] uvs = new Vector2[square * 4];
		Color[] colors = new Color[square * 4];
		int[] triangles = new int[square * 6];
		for (int i = 0; i < square; ++i)
		{
			Vector3 angleDir1 = Quaternion.AngleAxis((360f / square) * i, dir) * dirVertical;
			Vector3 angleDir2 = Quaternion.AngleAxis((360f / square) * (i + 1), dir) * dirVertical;
			//vertex
			vertices[i * 4 + 0] = from + angleDir1;
			vertices[i * 4 + 1] = to + angleDir1;
			vertices[i * 4 + 2] = from + angleDir2;
			vertices[i * 4 + 3] = to + angleDir2;
			//uv
			uvs[i * 4 + 0] = new Vector2(0f, 0f);
			uvs[i * 4 + 1] = new Vector2(1f, 0f);
			uvs[i * 4 + 2] = new Vector2(0f, 1f);
			uvs[i * 4 + 3] = new Vector2(1f, 1f);
			//Color
			colors[i * 4 + 0] = fromColor;
			colors[i * 4 + 1] = toColor;
			colors[i * 4 + 2] = fromColor;
			colors[i * 4 + 3] = toColor;
			//triangles
			triangles[i * 6 + 0] = i * 4 + 0;
			triangles[i * 6 + 1] = i * 4 + 2;
			triangles[i * 6 + 2] = i * 4 + 1;

			triangles[i * 6 + 3] = i * 4 + 2;
			triangles[i * 6 + 4] = i * 4 + 3;
			triangles[i * 6 + 5] = i * 4 + 1;
		}
		Mesh m = new Mesh();
		m.vertices = vertices;
		m.uv = uvs;
		m.colors = colors;
		m.triangles = triangles;
		return m;
	}
	/// <summary>
	/// �G�b�W���b�V���̍쐬
	/// </summary>
	private Mesh CreateEdgeMesh(Node from, Node to, float size, int square = 4)
	{
		return CreateEdgeMesh(from.pos, from.color, to.pos, to.color, size, square);
	}
	#endregion

	/// <summary>
	/// LineRendererEx�̃G�f�B�^�g��
	/// </summary>
	[CustomEditor(typeof(LineRendererEx))]
	public class InspectorLineRendererEx : Editor
	{

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LineRendererEx t = (LineRendererEx)target;
			if (GUILayout.Button("Apply"))
			{
				t.Apply();
			}
		}
	}
}

