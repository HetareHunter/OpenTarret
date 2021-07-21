using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarret
{
    public class TarretCartMover : MonoBehaviour
    {
        [SerializeField] float cartSpeed = 1.0f;
        [SerializeField] float limitMoveRangeX = 8.0f;
        [SerializeField] bool editMode = false;
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

        #region
#if UNITY_EDITOR

        private void FixedUpdate()
        {
            if (editMode)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (transform.position.x < limitMoveRangeX)
                    {
                        transform.Translate(Vector3.right * 1.0f * cartSpeed * Time.deltaTime);
                    }
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    if (transform.position.x > -limitMoveRangeX)
                    {
                        transform.Translate(Vector3.right * -1.0f * cartSpeed * Time.deltaTime);
                    }
                }
            }
            
        }
#endif
        #endregion
    }
}