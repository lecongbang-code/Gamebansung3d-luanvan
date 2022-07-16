using UnityEngine;

public class LoadSG : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject[] scene;

    public GameObject[] sceneUn;

    void Start()
    {
        for(int i = 0; i < playerController.scene.Length; i++)
        {
            if(playerController.scene[i])
            {
                scene[i].SetActive(true);
                sceneUn[i].SetActive(false);
            }
            else
            {
                scene[i].SetActive(false);
                sceneUn[i].SetActive(true);
            }
        }
    }
}
