using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    Dictionary<string, List<GameObject>> _poolObjectLists = new Dictionary<string, List<GameObject>>();
    public List<GameObject> _poolObjList;
    GameObject _poolObj;


    /// <summary>
    /// オブジェクトプールリストを登録する
    /// </summary>
    /// <param name="prefabName">Dictionaryのキー</param>
    /// <param name="objList"></param>
    public void CreatePoolLists(string prefabName, List<GameObject> objList)
    {
        _poolObjectLists.Add(prefabName, objList);
    }

    // オブジェクトプールを作成
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

    // オブジェクトプールを作成
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
        // 使用中でないものを探して返す
        foreach (var obj in _poolObjList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 全て使用中だったら新しく作って返す
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
    /// プールからオブジェクトを拾い上げ、呼び出し元に渡す
    /// </summary>
    /// <param name="name">プールのリストに登録している名前</param>
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

