using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Point scriptPoint;

    public GameObject medKit;
    public int countMedKit = 3;

    GameObject[] spawnPoint;

    public GameObject[] zombie;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnEnemy");

        RandomZombie();

        for(int i = 0; i < countMedKit; i++)
        {
            SpawnMedKit();
        }
    }

    void RandomZombie()
    {
        if(scriptPoint.zombieButcher >= 0)
        {
            for(int j = 1; j <= scriptPoint.zombieButcher; j++)
            {
                Spawn(0);
            }
        }

        if(scriptPoint.zombieCrazy >= 0)
        {
            for(int j = 1; j <= scriptPoint.zombieCrazy; j++)
            {
                Spawn(1);
            }
        }

        if(scriptPoint.zombieFat >= 0)
        {
            for(int j = 1; j <= scriptPoint.zombieFat; j++)
            {
                Spawn(2);
            }
        }

        if(scriptPoint.zombieMechanic >= 0)
        {
            for(int j = 1; j <= scriptPoint.zombieMechanic; j++)
            {
                Spawn(3);
            }
        }

        if(scriptPoint.zombiePrisoner >= 0)
        {
            for(int j = 1; j <= scriptPoint.zombiePrisoner; j++)
            {
                Spawn(4);
            }
        }
    }

    void Spawn(int pointZombie)
    {
        // tạo ngẫu nhiên một số bất kì từ 0 đến độ dài của mảng 
        int point = Random.Range(0, spawnPoint.Length);
        // tạo zombie tại vị trí bất kỳ đó
        Instantiate(zombie[pointZombie], spawnPoint[point].transform.position, Quaternion.identity);
    }

    void SpawnMedKit()
    {
        int point = Random.Range(0, spawnPoint.Length);

        Instantiate(medKit, spawnPoint[point].transform.position, Quaternion.identity);
    }
}
