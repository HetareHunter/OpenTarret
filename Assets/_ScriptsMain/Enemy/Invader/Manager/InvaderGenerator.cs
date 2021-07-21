using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvaderGenerator : MonoBehaviour, ISpawnable
    {
        [SerializeField] GameObject invaderPrefab;
        /// <summary>
        /// �s��̍s�Bz�������̐� 1<=row
        /// </summary>
        [SerializeField] int m_row = 5;
        /// <summary>
        /// �s��̗�Bx�������̐� 1<=column
        /// </summary>
        [SerializeField] int m_column = 11;
        /// <summary>
        /// �X�|�[�������Ƃ���y���̒l
        /// </summary>
        [SerializeField] float spawnHeight = 1.0f;
        public List<GameObject> invaders = new List<GameObject>();
        InvaderMoveCommander invaderMoveCommander;
        InvaderCounter invaderCounter;
        Quaternion resetQuaternion = new Quaternion(0, 0, 0, 0);

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
            if (m_column % 2 == 1)
            {
                EnemyOddInstantiate();
            }
            else
            {
                EnemyEvenInstantiate();
            }
            SetInvaders();
        }

        void EnemyOddInstantiate()
        {
            var instancePosition = new Vector3(0, spawnHeight, 0);
            var instanceNum = 0;
            for (int row = 1; row <= m_row; row++, instanceNum++)
            {
                instancePosition.x = 0;
                instancePosition.z = row;
                invaders[instanceNum].transform.localPosition = instancePosition;
                invaders[instanceNum].transform.localRotation = resetQuaternion;
                invaders[instanceNum].SetActive(true);
            }
            for (float x = 1.0f; x <= m_column / 2; x *= (-1))
            {
                for (int row = 1; row <= m_row; row++, instanceNum++)
                {
                    instancePosition.x = x;
                    instancePosition.z = row;
                    invaders[instanceNum].transform.localPosition = instancePosition;
                    invaders[instanceNum].transform.localRotation = resetQuaternion;
                    invaders[instanceNum].SetActive(true);
                }
                if (x < 0)
                {
                    x--;
                }
            }
        }

        void EnemyEvenInstantiate()
        {
            var instancePosition = new Vector3(0, spawnHeight, 0);
            var instanceNum = 0;
            for (float x = 0.5f; x <= (float)m_column / 2; x *= (-1))
            {
                for (int row = 1; row <= m_row; row++, instanceNum++)
                {
                    instancePosition.x = x;
                    instancePosition.z = row;
                    invaders[instanceNum].transform.localPosition = instancePosition;
                    invaders[instanceNum].transform.localRotation = resetQuaternion;
                    invaders[instanceNum].SetActive(true);
                }
                if (x < 0)
                {
                    x--;
                }
            }
        }

        public void ResetEnemies()
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