using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "_Scripts/Create PlayerData")]

//現状使ってないです
public class PlayerData : ScriptableObject
{
    public int gameScore = 0;
    public int playerHP = 100;
}
