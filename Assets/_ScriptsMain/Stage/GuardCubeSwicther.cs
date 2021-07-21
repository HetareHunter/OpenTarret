using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCubeSwicther : MonoBehaviour
{
    public int guardCubeHP = 4;
    int currentHP;

    private void OnEnable()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        currentHP = guardCubeHP;
    }

    void Damaged(int damage)
    {
        currentHP -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damaged(1);
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
