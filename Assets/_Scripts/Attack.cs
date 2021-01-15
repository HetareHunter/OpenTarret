using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject muzzleExlodeEffect;
    [SerializeField] GameObject muzzleExplodeEffectInsPosi;
    [SerializeField] GameObject rayOfOrigin;
    RaycastHit m_rayHit;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(rayOfOrigin.transform.position, rayDirection * rayDistance);
        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchFireEffect();
            if (Physics.Raycast(ray, out m_rayHit, rayDistance))
            {
                //Rayが当たったオブジェクトのtagがPlayerだったら
                if (m_rayHit.collider.tag == "Enemy")
                {
                    Debug.Log("RayがEnemyに当たった");
                    m_rayHit.collider.gameObject.GetComponent<EnemyDeath>().OnDead();
                }
            }
        }
    }
    void LaunchFireEffect()
    {
        //onFire = true;
        Instantiate(muzzleExlodeEffect, muzzleExplodeEffectInsPosi.transform);
        //insImpact = Instantiate(impactObj, muzzleExplodeEffectInsPosi.transform);
        
    }

    void BeginAttack()
    {

    }

    void StayAttack()
    {

    }

    void EndAttack()
    {

    }
}