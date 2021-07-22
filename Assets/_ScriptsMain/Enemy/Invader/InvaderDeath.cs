using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderDeath : MonoBehaviour, IEnemyDeath
    {
        [SerializeField] float deathTime = 2.0f;
        InvaderStateManager invaderStateManager;
        
        

        private void Start()
        {
            invaderStateManager = GetComponent<InvaderStateManager>();
            
        }
        public void OnDead()
        {
            if (invaderStateManager == null)
            {
                invaderStateManager = GetComponent<InvaderStateManager>();
            }
            if (invaderStateManager.invaderState != InvaderState.Death)
            {
                invaderStateManager.ChangeInvaderState(InvaderState.Death);
            }

            Invoke("EnemyDestroy", deathTime);
        }
        public void AddScore()
        {

        }

        void EnemyDestroy()
        {
            gameObject.SetActive(false);
        }

        
    }
}