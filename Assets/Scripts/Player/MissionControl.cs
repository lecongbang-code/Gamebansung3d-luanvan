using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Mission")]
public class MissionControl : ScriptableObject
{
    [Header("Mission Zombie")]
    public int[] zombie;
    [Header("Mission Zombie")]
    public int[] zombieKill;
}