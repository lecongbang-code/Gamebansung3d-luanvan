using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitposition : MonoBehaviour
{
    public EnemyStat enemyStat;
    
    public int maxMultiplication;
    public int minMultiplication;
    int multiplication;

    public void Damage(int damage)
    {
        multiplication = Random.Range(minMultiplication, maxMultiplication);
        enemyStat.TakeAwayHealth(damage * multiplication);
    }
}
