using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;

    public GameObject[] playerSelect;

    public GameObject[] CanvasSelect;

    public PlayerController playerController;

    GameObject[] spawnPoint;

    #region  Singleton
    public static PlayerManager instance;

    void Awake()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnEnemy");

        instance = this;

        if(playerController.selectAkm)
        {
            player = playerSelect[0];
            Spawn(player);
            CanvasSelect[0].SetActive(true);
        }
        else if(playerController.selectM4)
        {
            player = playerSelect[1];
            Spawn(player);
            CanvasSelect[1].SetActive(true);
        }
        else if(playerController.selectM24)
        {
            player = playerSelect[2];
            Spawn(player);
            CanvasSelect[2].SetActive(true);
        }
        else if(playerController.selectSmg9)
        {
            player = playerSelect[3];
            Spawn(player);
            CanvasSelect[3].SetActive(true);
        }
        else if(playerController.selectImg)
        {
            player = playerSelect[4];
            Spawn(player);
            CanvasSelect[4].SetActive(true);
        }
        else
        {
            player = playerSelect[5];
            Spawn(player);
            CanvasSelect[5].SetActive(true);
        }
        Time.timeScale = 1;
    }

    void Spawn(GameObject Guns)
    {
        int point = Random.Range(0, spawnPoint.Length);
        player = (Instantiate(Guns, spawnPoint[point].transform.position, Quaternion.identity));
    }

    #endregion
}
