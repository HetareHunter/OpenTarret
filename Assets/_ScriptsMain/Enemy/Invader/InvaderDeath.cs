using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderDeath : MonoBehaviour, IEnemyDeath
    {
        [SerializeField] float deathTime = 2.0f;
        InvaderStateManager InvaderStateManager;

        private void Start()
        {
            InvaderStateManager = GetComponent<InvaderStateManager>();
        }
        public void OnDead()
        {
            if (InvaderStateManager == null)
            {
                InvaderStateManager = GetComponent<InvaderStateManager>();
            }
            InvaderStateManager.ChangeInvaderState(InvaderState.Death);
            Destroy(gameObject, deathTime);
        }
        public void AddScore()
        {

        }
    }
}