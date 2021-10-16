using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderGenerator : MonoBehaviour, ISpawnable
    {
        [SerializeField] GameObject invaderPrefab;
        /// <summary>
        /// 行列の行。z軸方向の数 1<=row
        /// </summary>
        [SerializeField] int m_row = 5;
        /// <summary>
        /// 行列の列。x軸方向の数 1<=column
        /// </summary>
        [SerializeField] int m_column = 11;
        /// <summary>
        /// スポーンしたときのy軸の値
        /// </summary>
        [SerializeField] float spawnHeight = 1.0f;
        [SerializeField] float spaceToGenerate_x = 1.4f;
        [SerializeField] float spaceToGenerate_z = 1.5f;
        public List<GameObject> invaders = new List<GameObject>();
        InvaderMoveCommander invaderMoveCommander;
        InvaderCounter invaderCounter;
        Quaternion resetQuaternion = new Quaternion(0, 0, 0, 0);

        Vector3 m_instancePosition;
        int m_instanceNum = 0;

        // Start is called before the first frame update
        void Start()
        {
            invaderMoveCommander = GetComponent<InvaderMoveCommander>();
            invaderCounter = GetComponent<InvaderCounter>();
            foreach (Transform child in transform)
            {
                invaders.Add(child.gameObject);
            }
        }

        public void EnemySpawn()
        {
            EnemyInstantiate();
            SetInvaders();
        }

        void EnemyInstantiate()
        {
            m_instancePosition = new Vector3(0, spawnHeight, 0);
            m_instanceNum = 0;
            if (m_column % 2 == 1)
            {
                SetInstancePosition(0);
                float x = spaceToGenerate_x;
                for (float i = 1.0f; i <= m_column / 2; i *= (-1))
                {
                    SetInstancePosition(x);
                    if (i < 0)
                    {
                        x -= spaceToGenerate_x;
                        i--;
                    }
                    x *= (-1);
                }
            }
            else
            {
                float x = spaceToGenerate_x / 2;
                for (float i = 0.5f; i <= (float)m_column / 2; i *= (-1))
                {
                    SetInstancePosition(x);
                    if (i < 0)
                    {
                        x -= spaceToGenerate_x;
                        i--;
                    }
                    x *= (-1);
                }
            }
        }

        void SetInstancePosition(float column)
        {
            m_instancePosition.x = column;
            for (int row = 1; row <= m_row; row++, m_instanceNum++)
            {
                m_instancePosition.z += spaceToGenerate_z;
                invaders[m_instanceNum].transform.localPosition = m_instancePosition;
                invaders[m_instanceNum].transform.localRotation = resetQuaternion;
                invaders[m_instanceNum].SetActive(true);
            }
            m_instancePosition.z = 0;
        }

        public void Reset()
        {
            foreach (var item in invaders)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
            invaderCounter.InvaderCountZero();
        }
        public void ChangeEnemyNum(int num)
        {

        }
        public void SpawnStart()
        {
            //Debug.Log("SpawnStart!");
            EnemySpawn();
            SpawnEnd();
        }
        public void SpawnEnd()
        {
            invaderCounter.SetMaxInvaderNum();
        }

        void SetInvaders()
        {
            invaderMoveCommander.SetInvaders(invaders);
        }
    }
}