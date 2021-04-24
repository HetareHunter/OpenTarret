using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("TarretCollider");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPositon = target.transform.position;
        // 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
        //if (transform.position.y != target.transform.position.y)
        //{
        //    targetPositon = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        //}
        Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
