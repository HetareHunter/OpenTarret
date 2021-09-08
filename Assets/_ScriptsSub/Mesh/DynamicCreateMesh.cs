using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]//MeshRenderer�̗p�ӁB���b�V����\������ɂ�MeshRenderer�܂���SkiningMeshRenderer���K�v
[RequireComponent(typeof(MeshFilter))]//MeshRenderer�Ɠ��l�ɕK�v�BMesh��MeshRenderer�ɓn������
public class DynamicCreateMesh : MonoBehaviour
{
    [SerializeField] Material _mat;
    private void Start()
    {
        //Mesh�C���X�^���X���쐬
        var mesh = new Mesh();
        //mesh�ɒ��_���W�z���n���B�A�^�b�`����GameObject�̃��[�J�����W�����_�ƂȂ�
        mesh.vertices = new Vector3[] {
            new Vector3 (0, 1f),
            new Vector3 (1f, -1f),
            new Vector3 (-1f, -1f),
            new Vector3 (0.2f, 1f),
            new Vector3 (2.2f, 1f),
            new Vector3 (1.2f, -1f),
        };
        //mesh�ɒ��_�̏��Ԕz���n���B���_�̕`�揇�ƂȂ�Bunity�͎��v���ɒ��_�����񂾖ʂ��O�ʂƂȂ�
        mesh.triangles = new int[] {
            0, 1, 2,
            3,4,5
        };
        //�@�������̍Čv�Z���s���B������Ă΂Ȃ��Ɩ@��������(0,0,1)�Œ�ɂȂ��Ă��܂�
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