using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 試作品　実用性は皆無
/// </summary>
public class Duplication : MonoBehaviour
{
    public GameObject duplicatePrefab;
    public int duplicateNum = 0;
    public Vector3 duplicatePlace;
}


#if UNITY_EDITOR
[CustomEditor(typeof(Duplication))]
public class DuplicationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Duplication dpobj = target as Duplication;
        DrawDefaultInspector();

        if (GUILayout.Button("Duplicate ! "))
        {
            Debug.Log("押した!");
            // 押下時に実行したい処理
            for (int i = 0; i < dpobj.duplicateNum; i++)
            {
                var insobj = Instantiate(dpobj.duplicatePrefab, dpobj.duplicatePlace, Quaternion.identity);
                insobj.name = insobj.name.Replace("(Clone)", "_" + (i + 1).ToString());
            }
        }
    }
}

#endif
