using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Controller")]
public class PlayerController : ScriptableObject
{
    [Header("PlayerAsset")]
    public int money = 10000;
    public int gold = 100;
    public int key = 0;
    [Space(10)]
    public int medKit = 5;
    public float speedMove = 15f;
    public int health = 100;
    [Space(10)]
    [Header("Status")]
    public bool aiming;
    public bool reload;
    public bool run;
    [Space(10)]
    [Header("StatusBalo")]
    public bool akm;
    public bool m4;
    public bool m24;
    public bool smg9;
    public bool img;
    [Space(10)]
    [Header("StatusUpdate")]
    public int priceUpdataAkm = 5500;
    public int priceUpdataM4 = 5500;
    public int priceUpdataM24 = 5500;
    public int priceUpdataSmg9 = 5500;
    public int priceUpdataImg = 5500;
    [Space(10)]
    [Header("StatusSelect")]
    public bool selectAkm;
    public bool selectM4;
    public bool selectM24;
    public bool selectSmg9 = true;
    public bool selectImg;
    [Space(10)]
    [Header("StatusSceneGame")]
    public bool[] scene;
}
