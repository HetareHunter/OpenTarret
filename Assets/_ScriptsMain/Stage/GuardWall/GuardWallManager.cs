using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWallManager : MonoBehaviour
{
    GameObject[] guradWalls;
    GuardWallResetter[] guardWallResetters;
    // Start is called before the first frame update
    void Start()
    {
        GetAllChildObject();
    }

    void GetAllChildObject()
    {
        guradWalls = new GameObject[transform.childCount];
        guardWallResetters = new GuardWallResetter[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            guradWalls[i] = transform.GetChild(i).gameObject;
            guardWallResetters[i] = guradWalls[i].GetComponent<GuardWallResetter>();
        }
    }

    public void ResetAllGuardWalls()
    {
        foreach (var wall in guardWallResetters)
        {
            wall.ResetGuardWall();
        }
    } 
}
