using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GenerateImpulseView : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;
    //[SerializeField] GameObject impulseObj;
    [SerializeField] GameObject impulseCamera;
    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            impulseSource.GenerateImpulseAt(impulseCamera.transform.position, Vector3.back);
        }
    }
}
