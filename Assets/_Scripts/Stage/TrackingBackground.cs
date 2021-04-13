using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景がプレイヤーの座標を追跡していく
/// </summary>
public class TrackingBackground : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 startPosition;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = player.transform.position;
        newPosition.y = startPosition.y;
        transform.position = newPosition;
    }
}
