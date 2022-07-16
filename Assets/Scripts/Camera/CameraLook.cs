using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform playerBody;

    public Transform cammeraMain;

    public Transform tarGetLook;

    public SetUpCammera setUpCammera;
    
    public PlayerController playerController;

    public Weapon weapon;

    float xRotation = 0f;

    float mouseSensitivity = 0f;

    void Update()
    {
        if(!playerController.aiming)
        {
            mouseSensitivity = setUpCammera.speedNoAiming;
        }
        else
        {
            mouseSensitivity = setUpCammera.speedAiming;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        playerBody.Rotate(Vector3.up * mouseX);

        TargetLook();
    }

    public void Advan()
    {
        float recoilMin = weapon.recoil * 0.45f;
        float recoilMax = weapon.recoil * 1.5f;
        float recoil = Random.Range(recoilMin,recoilMax);
        xRotation = Mathf.Clamp(xRotation - recoil, -90f, 90f);
    }

    void TargetLook()
    {
        Ray ray = new Ray(cammeraMain.position, cammeraMain.forward * 2000);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            tarGetLook.position = Vector3.Lerp(tarGetLook.position, hit.point, Time.deltaTime * 40);
        }
        else
        {
            tarGetLook.position = Vector3.Lerp(tarGetLook.position, tarGetLook.transform.forward * 200, Time.deltaTime * 5);
        }
    }
}
