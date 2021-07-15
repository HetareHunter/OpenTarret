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
        [SerializeField] GameObject gripPosi;
        private void Start()
        {
            if (leftHandMesh == null)
            {
                leftHandMesh = GameObject.Find("hands:b_l_hand");
            }
            if (rightHandMesh == null)
            {
                rightHandMesh = GameObject.Find("hands:b_r_hand");
            }
        }
        public void FixHand(OVRInput.Controller controller)
        {
            if (controller == OVRInput.Controller.LTouch)
            {
                leftHandMesh.transform.position = gripPosi.transform.position;
            }
            else if (controller == OVRInput.Controller.RTouch)
            {
                rightHandMesh.transform.position = gripPosi.transform.position;
            }
        }

        public void ReleseHand(OVRInput.Controller controller)
        {
            if (controller == OVRInput.Controller.LTouch)
            {
                leftHandMesh.transform.localPosition = Vector3.zero;
            }
            else if (controller == OVRInput.Controller.RTouch)
            {
                rightHandMesh.transform.localPosition = Vector3.zero;
            }
        }
    }
}