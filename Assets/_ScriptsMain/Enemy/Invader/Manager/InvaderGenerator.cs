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
        [SerializeField] float spaceToGenerate = 0.5f;
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
                //for (int row = 1; row <= m_row; row++, instanceNum++)
                //{
                //    instancePosition.x = 0;
                //    instancePosition.z = row;
                //    invaders[instanceNum].transform.localPosition = instancePosition;
                //    invaders[instanceNum].transform.localRotation = resetQuaternion;
                //    invaders[instanceNum].SetActive(true);
                //}
                SetInstancePosition(0);
                for (float x = 1.0f; x <= m_column / 2; x *= (-1))
                {
                    //for (int row = 1; row <= m_row; row++, instanceNum++)
                    //{
                    //    instancePosition.x = x;
                    //    instancePosition.z = row;
                    //    invaders[instanceNum].transform.localPosition = instancePosition;
                    //    invaders[instanceNum].transform.localRotation = resetQuaternion;
                    //    invaders[instanceNum].SetActive(true);
                    //}
                    SetInstancePosition(x);
                    if (x < 0)
                    {
                        x--;
                    }
                }
            }
            else
            {
                for (float x = 0.5f; x <= (float)m_column / 2; x *= (-1))
                {
                    //for (int row = 1; row <= m_row; row++, instanceNum++)
                    //{
                    //    instancePosition.x = x;
                    //    instancePosition.z = row;
                    //    invaders[instanceNum].transform.localPosition = instancePosition;
                    //    invaders[instanceNum].transform.localRotation = resetQuaternion;
                    //    invaders[instanceNum].SetActive(true);
                    //}
                    SetInstancePosition(x);
                    if (x < 0)
                    {
                        x--;
                    }
                }
            }
        }

        //void EnemyOddInstantiate()
        //{
        //    var instancePosition = new Vector3(0, spawnHeight, 0);
        //    var instanceNum = 0;
        //    SetInstancePosition(instancePosition, 0, instanceNum);
        //    for (int row = 1; row <= m_row; row++, instanceNum++)
        //    {

        //        instancePosition.x = 0;
        //        instancePosition.z = row;
        //        invaders[instanceNum].transform.localPosition = instancePosition;
        //        invaders[instanceNum].transform.localRotation = resetQuaternion;
        //        invaders[instanceNum].SetActive(true);
        //    }
        //    for (float x = 1.0f; x <= m_column / 2; x *= (-1))
        //    {
        //        SetInstancePosition(instancePosition, x, instanceNum);
        //        for (int row = 1; row <= m_row; row++, instanceNum++)
        //        {
        //            instancePosition.x = x;
        //            instancePosition.z = row;
        //            invaders[instanceNum].transform.localPosition = instancePosition;
        //            invaders[instanceNum].transform.localRotation = resetQuaternion;
        //            invaders[instanceNum].SetActive(true);
        //        }
        //        if (x < 0)
        //        {
        //            x--;
        //        }
        //    }
        //}

        //void EnemyEvenInstantiate()
        //{
        //    var instancePosition = new Vector3(0, spawnHeight, 0);
        //    var instanceNum = 0;
        //    for (float x = 0.5f; x <= (float)m_column / 2; x *= (-1))
        //    {
        //        SetInstancePosition(instancePosition, x, instanceNum);
        //        for (int row = 1; row <= m_row; row++, instanceNum++)
        //        {
        //            instancePosition.x = x;
        //            instancePosition.z = row;
        //            invaders[instanceNum].transform.localPosition = instancePosition;
        //            invaders[instanceNum].transform.localRotation = resetQuaternion;
        //            invaders[instanceNum].SetActive(true);
        //        }
        //        if (x < 0)
        //        {
        //            x--;
        //        }
        //    }
        //}

        void SetInstancePosition(float column)
        {
            for (int row = 1; row <= m_row; row++, m_instanceNum++)
            {
                m_instancePosition.x = column;
                m_instancePosition.z = row + spaceToGenerate;
                invaders[m_instanceNum].transform.localPosition = m_instancePosition;
                invaders[m_instanceNum].transform.localRotation = resetQuaternion;
                invaders[m_instanceNum].SetActive(true);
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