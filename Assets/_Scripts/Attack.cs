using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject muzzleExlodeEffect;
    [SerializeField] GameObject muzzleExplodeEffectInsPosi;
    [SerializeField] GameObject impactObj;
    GameObject insImpact;
    [SerializeField] float impactCoefficient = 1;
    bool onFire = false;
    [SerializeField] float impactDeathTime = 2;


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
        if (onFire)
        {
            ImpactScaleUp(insImpact.transform.localScale.x);
            Destroy(insImpact, impactDeathTime);
            
        }
        if (insImpact == null)
        {
            onFire = false;
        }
    }
    void LaunchFireEffect()
    {
        onFire = true;
        Instantiate(muzzleExlodeEffect, muzzleExplodeEffectInsPosi.transform);
        insImpact = Instantiate(impactObj, muzzleExplodeEffectInsPosi.transform);
        
    }

    void ImpactScaleUp(float insImpactScale)
    {
        insImpact.transform.localScale = new Vector3(insImpactScale += impactCoefficient,
            insImpactScale += impactCoefficient, insImpact.transform.lossyScale.z);
    }
}