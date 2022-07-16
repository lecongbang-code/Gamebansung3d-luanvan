using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailShop : MonoBehaviour
{
    public Weapon weapon;

    public TextMeshProUGUI satthuong;
    public TextMeshProUGUI dogiat;
    public TextMeshProUGUI tocdo;
    public TextMeshProUGUI dan;

    public Slider sliderDamage;
    public Slider sliderRecoil;
    public Slider sliderSpeedBullet;
    public Slider sliderBulletMax;

    void Start()
    {
        sliderDamage.value = weapon.damage;
        sliderRecoil.value = weapon.recoil;
        sliderSpeedBullet.value = weapon.speedBullet;
        sliderBulletMax.value = weapon.bulletMax;

        satthuong.text = "" + weapon.damage;
        dogiat.text = "" + weapon.recoil;
        tocdo.text = "" + weapon.speedBullet;
        dan.text = "" + weapon.bulletMax;
    }
    public void LoadData()
    {
        sliderDamage.value = weapon.damage;
        sliderRecoil.value = weapon.recoil;
        sliderSpeedBullet.value = weapon.speedBullet;
        sliderBulletMax.value = weapon.bulletMax;

        satthuong.text = "" + weapon.damage;
        dogiat.text = "" + weapon.recoil;
        tocdo.text = "" + weapon.speedBullet;
        dan.text = "" + weapon.bulletMax;
    }
}
