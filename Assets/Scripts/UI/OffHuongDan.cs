using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHuongDan : MonoBehaviour
{
    public GameObject huongDan;

    bool isHuongDan = true;

    void Update()
    {
        if(isHuongDan)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            huongDan.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isHuongDan = false;
        }
    }

    public void TatHuongDan()
    {
        huongDan.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isHuongDan = false;
        Time.timeScale = 1;
    }
}
