using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWallResetter : MonoBehaviour
{
    GameObject[] guradWalls;
    // Start is called before the first frame update
    void Start()
    {
        GetAllChildObject();
    }

    public void ResetGuardWall()
    {
        foreach (var cube in guradWalls)
        {
            cube.SetActive(true);
        }
    }

    void GetAllChildObject()
    {
        guradWalls = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            guradWalls[i] = transform.GetChild(i).gameObject;
        }
    }
}
