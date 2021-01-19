using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class TarretAttack : MonoBehaviour
{
    [SerializeField] Vector3 rayDirection;
    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject m_razerEffect;
    [SerializeField] GameObject m_razerEffectInsPosi;
    [SerializeField] GameObject m_wasteHeatEffect;
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    [SerializeField] GameObject rayOfOrigin;

    GameObject m_razer;
    GameObject m_wasteHeat;
    [SerializeField] float razerExistTime = 0.5f;
    [SerializeField] float wasteHeatExistTime = 2.0f;
    Ray m_ray;
    RaycastHit m_rayHit;

    BaseTarretBrain baseTarretBrain;

    private void Start()
    {
        baseTarretBrain = GetComponent<BaseTarretBrain>();
    }

    void FixedUpdate()
    {
        m_ray = new Ray(rayOfOrigin.transform.position, rayOfOrigin.transform.forward * rayDistance + rayOfOrigin.transform.position );

        if (Physics.Raycast(m_ray, out m_rayHit, rayDistance))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            if (m_rayHit.collider.tag == "Enemy")
            {
                Debug.Log("RayがEnemyに当たった");
                m_rayHit.collider.gameObject.GetComponent<EnemyDeath>().OnDead();
            }

            Debug.DrawLine(m_ray.origin, m_rayHit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(m_ray.origin, m_ray.direction * rayDistance, Color.red);
        }

    }
    void FireEffectManager()
    {
        m_razer = Instantiate(m_razerEffect, m_razerEffectInsPosi.transform.position,m_razerEffectInsPosi.transform.rotation);
        Destroy(m_razer, razerExistTime);
    }

    void WasteHeatEffectManager()
    {
        m_wasteHeat = Instantiate(m_wasteHeatEffect, m_wasteHeatEffectInsPosi.transform.position,
            m_wasteHeatEffectInsPosi.transform.rotation,m_wasteHeatEffectInsPosi.transform);
        Destroy(m_wasteHeat, wasteHeatExistTime);
    }

    public void BeginAttack()
    {
        FireEffectManager();
        WasteHeatEffectManager();
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

        
    }
}