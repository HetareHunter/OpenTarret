using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class HandFixer : MonoBehaviour
    {
        /// <summary>
        /// hands:l_hand_worldをアタッチする
        /// </summary>
        GameObject leftHandMesh;
        /// <summary>
        /// hands:r_hand_worldをアタッチする
        /// </summary>
        GameObject rightHandMesh;
        [SerializeField] GameObject grabHandMesh;
        private void Start()
        {

        }
        public void FixHand(OVRInput.Controller controller)
        {
            grabHandMesh.SetActive(true);
        }

        public void ReleseHand(OVRInput.Controller controller)
        {
            grabHandMesh.SetActive(false);
        }
    }
}