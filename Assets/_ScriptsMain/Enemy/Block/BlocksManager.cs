using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    List<GameObject> _enemyBlocks = new List<GameObject>();
    List<BlockDeath> _enemyBlockDeath = new List<BlockDeath>();
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            _enemyBlocks.Add(child);
            _enemyBlockDeath.Add(child.GetComponent<BlockDeath>());
        }
    }
    private void Start()
    {
        NonActivateBlocks();
        //gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        foreach (var item in _enemyBlocks)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void ResetBlocks()
    {
        foreach (var item in _enemyBlockDeath)
        {
            item.Reset();
        }
    }

    public void ActivateBlocks()
    {
        foreach (var item in _enemyBlockDeath)
        {
            item.DoActivate();
        }
    }

    public void NonActivateBlocks()
    {
        foreach (var item in _enemyBlockDeath)
        {
            item.DoNonActivate();
        }
    }
}
