using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCubeDamager : MonoBehaviour
{
    public int maxGuardCubeHP = 4;
    int currentHP;

    private void OnEnable()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        currentHP = maxGuardCubeHP;
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
            currentHP = maxGuardCubeHP;
            gameObject.SetActive(false);
        }
    }
}
