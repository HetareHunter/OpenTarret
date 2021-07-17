using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderGenerator : MonoBehaviour,ISpawnable
{
    [SerializeField] GameObject invader;
    /// <summary>
    /// 行列の行。z軸方向の数 1<=row
    /// </summary>
    [SerializeField] int row;
    /// <summary>
    /// 行列の列。x軸方向の数 1<=column
    /// </summary>
    [SerializeField] int column;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnemySpawn()
    {
        
    }

    public void ResetEnemies()
    {

    }
    public void ChangeEnemyNum(int num)
    {

    }
    public void SpawnStart()
    {
        Debug.Log("SpawnStart!");
    }
    public void SpawnEnd()
    {

    }
}
