using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Enemy
{
    public class InvaderCounter : MonoBehaviour
    {
        GameObject gameManager;
        InvaderGameStateManager InvaderGameStateManager;
        int invaderNum = 0;
        // Start is called before the first frame update
        void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameObject.Find("GameManager");
            }
            InvaderGameStateManager = gameManager.GetComponent<InvaderGameStateManager>();
        }

        public void CountInvader(int num)
        {
            invaderNum += num;
            IsCompleteDestruction();
        }

        public int GetInvaderNum()
        {
            return invaderNum;
        }

        public void ResetInvaderCount()
        {
            invaderNum = 0;
        }

        /// <summary>
        /// �G���S�ł������ǂ������肵�A�S�ł����ꍇ�Q�[���������ŏI������
        /// </summary>
        void IsCompleteDestruction()
        {
            if (invaderNum <= 0)
            {
                InvaderGameStateManager.FinishGame(true);
            }
        }
    }
}