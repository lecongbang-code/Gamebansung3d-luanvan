using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Properties")]
public class Weapon : ScriptableObject
{
    [Header("Gun_Settings")]
    public int bulletMax;
    public int damage;
    public float speedBullet;
    public float recoil;
    public int totalBullet;
    [Header("Silencer")]
    public bool silencer;
    [Header("Recoil_Settings")]
    public float PositionDampTime;
    public float RotationDampTime;
    [Space(10)]
    public float Recoil1;
    public float Recoil2;
    public float Recoil3;
    public float Recoil4;
    [Space(10)]
    public Vector3 RecoilRotation;
    public Vector3 RecoilKickBack;
    public Vector3 RecoilRotation_Aim;
    public Vector3 RecoilKickBack_Aim;
    [Space(10)]
    public Vector3 CurrentRecoil1;
    public Vector3 CurrentRecoil2;
    public Vector3 CurrentRecoil3;
    public Vector3 CurrentRecoil4;
    [Space(10)]
    public Vector3 RotationOutput;
    public bool aim;
    [Space(10)]
    public float rotationSpeed = 0f;
    public float returnSpeed = 0f;
    [Space(10)]
    public Vector3 RecoilRotationNoAiming = new Vector3(0f,0f,0f);
    public Vector3 RecoilRotationAiming = new Vector3(0f,0f,0f);
}