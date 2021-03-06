//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Enemy
//{
//    public class InvaderMoveCommander : MonoBehaviour
//    {
//        List<InvaderLists> invaderLists = new List<InvaderLists>();
//        public int maxMovePeriodOfFrame = 90;
//        public int minMovePeriodOfFrame = 30;

//        public void SetInvaders(List<GameObject> invaders)
//        {
//            if (invaderLists.Count > 0) return;
//            for (int i = 0; i < invaders.Count; i++)
//            {
//                invaderLists.Add(new InvaderLists(invaders[i].GetComponent<InvaderMover>(),
//                    invaders[i].GetComponent<InvaderStateManager>()));
//            }
//        }

//        public void LimitMove()
//        {
//            if (invaderLists == null)
//            {
//                return;
//            }
//            foreach (var item in invaderLists)
//            {
//                item.invaderMover.LimitMove();
//            }
//        }
//        /// <summary>
//        /// 進軍する命令を出す
//        /// </summary>
//        public void CommenceMarch()
//        {
//            if (invaderLists == null)
//            {
//                return;
//            }
//            foreach (var item in invaderLists)
//            {
//                item.invaderStateManager.ChangeInvaderState(InvaderState.March);
//            }
//        }

//        /// <summary>
//        /// 立ち止まっている命令を出す
//        /// </summary>
//        public void CommenceStandby()
//        {
//            if (invaderLists == null)
//            {
//                return;
//            }
//            foreach (var item in invaderLists)
//            {
//                item.invaderStateManager.ChangeInvaderState(InvaderState.Standby);
//            }

//        }

//        public void CommenceReset()
//        {
//            if (invaderLists == null)
//            {
//                return;
//            }
//            foreach (var item in invaderLists)
//            {
//                item.invaderStateManager.ChangeInvaderState(InvaderState.Reset);
//            }
//        }

//        public void CommenceChangeSpeed(int speed)
//        {
//            if (invaderLists == null)
//            {
//                return;
//            }
//            foreach (var item in invaderLists)
//            {
//                item.invaderMover.ChangeMoveSpeed(speed);
//            }
//        }

//        public void InvaderSpeedCalculate(float invaderAlivePer)
//        {
//            float speed = Mathf.Lerp(minMovePeriodOfFrame, maxMovePeriodOfFrame, invaderAlivePer);
//            CommenceChangeSpeed((int)speed);
//        }
//        #region
//#if UNITY_EDITOR
//        //int currentSpeed;
//        //private void Start()
//        //{
//        //    currentSpeed = movePeriodOfFrame;
//        //}
//        //private void Update()
//        //{
//        //    if (currentSpeed != movePeriodOfFrame)
//        //    {
//        //        currentSpeed = movePeriodOfFrame;
//        //        CommenceChangeSpeed(currentSpeed);
//        //    }
//        //}

//#endif
//        #endregion
//    }

//    public class InvaderLists
//    {
//        public InvaderMover invaderMover;
//        public InvaderStateManager invaderStateManager;

//        public InvaderLists(InvaderMover invaderMover, InvaderStateManager invaderStateManager)
//        {
//            this.invaderMover = invaderMover;
//            this.invaderStateManager = invaderStateManager;
//        }
//    }
//}