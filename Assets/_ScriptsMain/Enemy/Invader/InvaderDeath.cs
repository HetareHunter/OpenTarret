using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderDeath : MonoBehaviour, IEnemyDeath
    {
        [SerializeField] float deathTime = 2.0f;
        InvaderStateManager invaderStateManager;
        Rigidbody m_rb;
        

        private void Start()
        {
            invaderStateManager = GetComponent<InvaderStateManager>();
            m_rb = GetComponent<Rigidbody>();
        }
        public void OnDead()
        {
            if (invaderStateManager == null)
            {
                invaderStateManager = GetComponent<InvaderStateManager>();
            }
            
            invaderStateManager.ChangeInvaderState(InvaderState.Death);

            Invoke("EnemyDestroy", deathTime);
            //Destroy(gameObject, deathTime);
        }
        public void AddScore()
        {

        }

        void EnemyDestroy()
        {
            m_rb.velocity = Vector3.zero;
            m_rb.angularVelocity = Vector3.zero;
            
            gameObject.SetActive(false);
        }
    }
}