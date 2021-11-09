//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Manager;
//using TMPro;

//namespace Enemy
//{
//    public class InvaderCounter : MonoBehaviour
//    {
//        GameObject gameManager;
//        [SerializeField] TextMeshProUGUI enemyCountText;
//        InvaderGameStateManager InvaderGameStateManager;
//        InvaderMoveCommander invaderMoveCommander;
//        float maxInvaderNum;
//        public int invaderNum = 0;
//        // Start is called before the first frame update
//        void Start()
//        {
//            if (gameManager == null)
//            {
//                gameManager = GameObject.Find("GameManager");
//            }
//            InvaderGameStateManager = gameManager.GetComponent<InvaderGameStateManager>();
//            invaderMoveCommander = GetComponent<InvaderMoveCommander>();
//        }

//        public void CountInvader(int num)
//        {
//            if (invaderMoveCommander == null)
//            {
//                invaderMoveCommander = GetComponent<InvaderMoveCommander>();
//            }
//            invaderMoveCommander.InvaderSpeedCalculate(invaderNum / maxInvaderNum);
//            invaderNum += num;
//            IsCompleteDestruction();

//            if (enemyCountText != null)
//            {
//                enemyCountText.text = "Enemy : " + invaderNum.ToString();
//            }
//        }

//        public int GetInvaderNum()
//        {
//            return invaderNum;
//        }

//        public void InvaderCountZero()
//        {
//            invaderNum = 0;
//            if (enemyCountText != null)
//            {
//                enemyCountText.text = "Enemy : " + invaderNum.ToString();
//            }
//        }

//        public void SetMaxInvaderNum()
//        {
//            maxInvaderNum = invaderNum;
//        }

//        /// <summary>
//        /// �G���S�ł������ǂ������肵�A�S�ł����ꍇ�Q�[���������ŏI������
//        /// </summary>
//        void IsCompleteDestruction()
//        {
//            if (invaderNum <= 0)
//            {
//                InvaderGameStateManager.FinishGame(true);
//            }
//        }
//    }
//}