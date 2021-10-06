using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, ISpawnable
{
    public void ResetEnemies()
    {

    }
    public void ChangeEnemyNum(int num)
    {

    }
    public void SpawnStart()
    {

    }
    public void SpawnEnd()
    {

    }
}


public interface ISpawnable
{
    //public void EnemySpawn();
    public void ResetEnemies();
    public void ChangeEnemyNum(int num);
    public void SpawnStart();
    public void SpawnEnd();
}