using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHandle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //m_preHandlePosi = m_rotateTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Vector3 relativePos = m_rotateTarget.position - transform.position;
        //relativePos.y = 0; //上下方向の回転はしないように制御
        ////relativePos.z = 0;
        //transform.rotation = Quaternion.LookRotation(relativePos,Vector3.right);
    }
}
