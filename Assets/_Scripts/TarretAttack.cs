using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class TarretAttack : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject muzzleExlodeEffect;
    [SerializeField] GameObject muzzleExplodeEffectInsPosi;
    [SerializeField] GameObject rayOfOrigin;
    Ray m_ray;
    RaycastHit m_rayHit;

    BaseTarretBrain baseTarretBrain;

    private void Start()
    {
        baseTarretBrain = GetComponent<BaseTarretBrain>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_ray = new Ray(rayOfOrigin.transform.position, rayOfOrigin.transform.forward * rayDistance + rayOfOrigin.transform.position );
        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(m_ray.origin, m_ray.direction * rayDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchFireEffect();

        }
    }
    void LaunchFireEffect()
    {
        //onFire = true;
        Instantiate(muzzleExlodeEffect, muzzleExplodeEffectInsPosi.transform);
        //insImpact = Instantiate(impactObj, muzzleExplodeEffectInsPosi.transform);

    }

    public void BeginAttack()
    {
        LaunchFireEffect();
        LaunchRay();
        StayAttack();
    }

    void StayAttack()
    {
        EndAttack();
    }

    void EndAttack()
    {
        baseTarretBrain.ChangeTarretCommandIdle();
    }

    void LaunchRay()
    {

        if (Physics.Raycast(m_ray, out m_rayHit, rayDistance))
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