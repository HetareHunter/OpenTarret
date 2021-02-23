using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    //public bool death = false;
    [SerializeField] float deathTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (death)
        //{
        //    Destroy(gameObject, deathTime);
        //}
    }

    public void OnDead()
    {
        Destroy(gameObject, deathTime);
    }
}