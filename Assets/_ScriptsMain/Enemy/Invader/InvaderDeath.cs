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
            
            invaderStateManager.ChangeInvaderState(InvaderState.Death);
            Destroy(gameObject, deathTime);
        }
        public void AddScore()
        {

        }
    }
}