using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, ISpawner
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

    }
    public void SpawnEnd()
    {

    }
}


    public interface ISpawner
{
    public void EnemySpawn();
    public void ResetEnemies();
    public void ChangeEnemyNum(int num);
    public void SpawnStart();
    public void SpawnEnd();
}