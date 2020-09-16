using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    [SerializeField] GameObject rotationTarget;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
        }
        var q = Quaternion.AngleAxis(0, Vector3.right);
        var direction = rotationTarget.transform.position - this.transform.position;

        var targetQuatanion = q*Quaternion.LookRotation(direction);
        //targetQuatanion.y = transform.parent.rotation.y;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQuatanion, Time.deltaTime);
    }
}
