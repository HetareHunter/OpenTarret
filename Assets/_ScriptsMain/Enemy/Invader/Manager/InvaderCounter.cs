using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderCounter : MonoBehaviour
    {
        public int invaderNum = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        public void CountInvader(int num)
        {
            invaderNum += num;
        }

        public int GetInvaderNum()
        {
            return invaderNum;
        }

        public void ResetInvaderCount()
        {
            invaderNum = 0;
        }
    }
}