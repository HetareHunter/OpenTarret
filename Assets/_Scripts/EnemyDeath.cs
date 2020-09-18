using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    bool death = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            Destroy(gameObject);
        }
    }

    public void OnDead()
    {
        death = true;
    }
}