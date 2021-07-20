using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarretCartMover : MonoBehaviour
{
    [SerializeField] float cartSpeed = 1.0f;
    [SerializeField] float limitMoveRangeX = 8.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void CartMove(Vector2 stick)
    {
        if (stick.x > 0)//右向きの動き
        {
            if (transform.position.x < limitMoveRangeX)
            {
                transform.Translate(Vector3.right * stick.x * cartSpeed * Time.deltaTime);
            }
        }
        else if (stick.x < 0)//左向きの動き
        {
            if (transform.position.x > -limitMoveRangeX)
            {
                transform.Translate(Vector3.right * stick.x * cartSpeed * Time.deltaTime);
            }
        }
    }
}
