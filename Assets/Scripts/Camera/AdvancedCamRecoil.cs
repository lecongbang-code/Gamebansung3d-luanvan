using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCamRecoil : MonoBehaviour
{
    float rotationSpeed;
    float returnSpeed;

    Vector3 RecoilRotationNoAiming;
    Vector3 RecoilRotationAiming;

    Vector3 currentRotation;
    Vector3 Rot;

    public Weapon setUpCammera;

    void Awake() 
    {
        rotationSpeed = setUpCammera.rotationSpeed;
        returnSpeed = setUpCammera.returnSpeed;
        RecoilRotationNoAiming = setUpCammera.RecoilRotationNoAiming;
        RecoilRotationAiming = setUpCammera.RecoilRotationAiming;
    }
    void FixedUpdate() 
    {
        currentRotation =Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);    
        Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Rot);
    }

    public void Fire(bool aiming)
    {
        if(aiming)
        {
            currentRotation += new Vector3(-RecoilRotationAiming.x, Random.Range(-RecoilRotationAiming.y, RecoilRotationAiming.y), Random.Range(-RecoilRotationAiming.z, RecoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3(-RecoilRotationNoAiming.x, Random.Range(-RecoilRotationNoAiming.y, RecoilRotationNoAiming.y), Random.Range(-RecoilRotationNoAiming.z, RecoilRotationNoAiming.z));
        }
    }
}
