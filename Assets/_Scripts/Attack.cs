using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject muzzleExlodeEffect;
    [SerializeField] GameObject muzzleExplodeEffectInsPosi;

    // Update is called once per frame
    void Update()
    {
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, rayDirection * rayDistance);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;


        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchFireEffect();
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                //Rayが当たったオブジェクトのtagがPlayerだったら
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("RayがEnemyに当たった");
                    hit.collider.gameObject.GetComponent<EnemyDeath>().OnDead();
                }
            }
        }
        
    }
    void LaunchFireEffect()
    {
        Instantiate(muzzleExlodeEffect, muzzleExplodeEffectInsPosi.transform);
    }
}