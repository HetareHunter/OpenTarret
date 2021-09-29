using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    Dictionary<string, List<GameObject>> _poolObjectLists = new Dictionary<string, List<GameObject>>();
    public List<GameObject> _poolObjList;
    GameObject _poolObj;


    /// <summary>
    /// �I�u�W�F�N�g�v�[�����X�g��o�^����
    /// </summary>
    /// <param name="prefabName">Dictionary�̃L�[</param>
    /// <param name="objList"></param>
    public void CreatePoolLists(string prefabName, List<GameObject> objList)
    {
        _poolObjectLists.Add(prefabName, objList);
    }

    // �I�u�W�F�N�g�v�[�����쐬
    public void CreatePool(GameObject obj, int maxCount)
    {
        _poolObj = obj;
        _poolObjList = new List<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject();
            newObj.SetActive(false);
            _poolObjList.Add(newObj);
        }
        CreatePoolLists(obj.name, _poolObjList);
    }

    GameObject CreateNewObject()
    {
        var newObj = Instantiate(_poolObj);
        newObj.name = _poolObj.name + (_poolObjList.Count + 1);

        return newObj;
    }

    // �I�u�W�F�N�g�v�[�����쐬
    public void CreatePool(GameObject obj, int maxCount, Transform parentObj)
    {
        _poolObj = obj;
        _poolObjList = new List<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject(parentObj);
            newObj.SetActive(false);
            _poolObjList.Add(newObj);
        }
    }

    GameObject CreateNewObject(Transform parentObj)
    {
        var newObj = Instantiate(_poolObj, parentObj);
        newObj.name = _poolObj.name + (_poolObjList.Count + 1);

        return newObj;
    }

    public GameObject GetObject()
    {
        // �g�p���łȂ����̂�T���ĕԂ�
        foreach (var obj in _poolObjList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // �S�Ďg�p����������V��������ĕԂ�
        var newObj = CreateNewObject();
        newObj.SetActive(true);
        _poolObjList.Add(newObj);

        return newObj;
    }

    public GameObject GetObject(Transform parentObj)
    {
        foreach (var obj in _poolObjList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        var newObj = CreateNewObject(parentObj);
        newObj.SetActive(true);
        _poolObjList.Add(newObj);

        return newObj;
    }

    /// <summary>
    /// �v�[������I�u�W�F�N�g���E���グ�A�Ăяo�����ɓn��
    /// </summary>
    /// <param name="name">�v�[���̃��X�g�ɓo�^���Ă��閼�O</param>
    /// <param name="position"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public GameObject GetObject(string name, Vector3 position, Quaternion angle)
    {
        foreach (var obj in _poolObjectLists[name])
        {
            if (obj.activeSelf == false)
            {
                obj.transform.SetPositionAndRotation(position, angle);
                obj.SetActive(true);
                return obj;
            }
        }

        var newObj = CreateNewObject();
        newObj.transform.SetPositionAndRotation(position, angle);
        newObj.SetActive(true);
        _poolObjList.Add(newObj);

        return newObj;
    }
}

