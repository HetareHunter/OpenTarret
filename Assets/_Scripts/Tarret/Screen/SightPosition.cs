using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightPosition : MonoBehaviour
{
    [SerializeField] GameObject tarret;
    BaseTarretAttack baseTarretAttack;
    // Start is called before the first frame update
    void Start()
    {
        baseTarretAttack = tarret.GetComponent<BaseTarretAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = baseTarretAttack.rayHitFirstPosi;
    }
}
