using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Main_controller : MonoBehaviour
{   
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;
    public PlayerController playerController;
    public TextMeshProUGUI money;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI moneyDetail;
    public TextMeshProUGUI goldDetail;
    public TextMeshProUGUI goldDetailBalo;
    public TextMeshProUGUI moneyDetailBalo;

    public int priceAKM;
    public GameObject itemAkm;

    public int priceM4;
    public GameObject itemM4;

    public int priceM24;
    public GameObject itemM24;

    public int priceSMg9;
    public GameObject itemSmg9;

    public int priceIMG;
    public GameObject itemImg;

    public GameObject baloAKM;
    public GameObject baloM4;
    public GameObject baloM24;
    public GameObject baloSmg9;
    public GameObject baloIMG;

    public GameObject shopAKM;
    public GameObject shopM4;
    public GameObject shopM24;
    public GameObject shopSmg9;
    public GameObject shopIMG;

    public TextMeshProUGUI txtPriceUpdateAkm;
    public TextMeshProUGUI txtPriceUpdateM4;
    public TextMeshProUGUI txtPriceUpdateM24;
    public TextMeshProUGUI txtPriceUpdateSmg9;
    public TextMeshProUGUI txtPriceUpdateImg;

    bool akm;
    bool m4;
    bool m24;
    bool smg9;
    bool img;
    
    public GameObject btnSelectAkm;
    public GameObject btnUnSelectAkm;

    public GameObject btnSelectM4;
    public GameObject btnUnSelectM4;

    public GameObject btnSelectM24;
    public GameObject btnUnSelectM24;

    public GameObject btnSelectSmg9;
    public GameObject btnUnSelectSmg9;

    public GameObject btnSelectImg;
    public GameObject btnUnSelectImg;

    public int priceDcAkm;
    public int priceDcM4;
    public int priceDcM24;
    public int priceDcSmg9;
    public int priceDcImg;

    public GameObject itemDcAkm;
    public TextMeshProUGUI txtTotalBulletAkm;
    public GameObject itemDcM4;
    public TextMeshProUGUI txtTotalBulletM4;
    public GameObject itemDcM24;
    public TextMeshProUGUI txtTotalBulletM24;
    public GameObject itemDcSmg9;
    public TextMeshProUGUI txtTotalBulletSmg9;
    public GameObject itemDcImg;
    public TextMeshProUGUI txtTotalBulletImg;

    public MessengerControl messengerControl;

    public void Start()
    {
        Time.timeScale = 1;

        CheckTxt();
        CheckBtn();

        txtPriceUpdateAkm.text = "" + playerController.priceUpdataAkm;
        txtPriceUpdateM4.text = "" + playerController.priceUpdataM4;
        txtPriceUpdateM24.text = "" + playerController.priceUpdataM24;
        txtPriceUpdateSmg9.text = "" + playerController.priceUpdataSmg9;
        txtPriceUpdateImg.text = "" + playerController.priceUpdataImg;

        shopAKM.SetActive(playerController.akm);
        shopM4.SetActive(playerController.m4);
        shopM24.SetActive(playerController.m24);
        shopSmg9.SetActive(playerController.smg9);
        shopIMG.SetActive(playerController.img);

        itemAkm.SetActive(playerController.akm);
        itemM4.SetActive(playerController.m4);
        itemM24.SetActive(playerController.m24);
        itemSmg9.SetActive(playerController.smg9);
        itemImg.SetActive(playerController.img);

        CheckBoolDc();

        txtTotalBulletAkm.text = "" + weapon[0].totalBullet;
        txtTotalBulletM4.text = "" + weapon[1].totalBullet;
        txtTotalBulletM24.text = "" + weapon[2].totalBullet;
        txtTotalBulletSmg9.text = "" + weapon[3].totalBullet;
        txtTotalBulletImg.text = "" + weapon[4].totalBullet;

        baloAKM.SetActive(false);
        baloM4.SetActive(false);
        baloM24.SetActive(false);
        baloSmg9.SetActive(false);
        baloIMG.SetActive(false);

        if(!playerController.akm)
        {
            baloAKM.SetActive(true);
        }
        if(!playerController.m4)
        {
            baloM4.SetActive(true);
        }
        if(!playerController.m24)
        {
            baloM24.SetActive(true);
        }
        if(!playerController.smg9)
        {
            baloSmg9.SetActive(true);
        }
        if(!playerController.img)
        {
            baloIMG.SetActive(true);
        }

        akm = playerController.selectAkm;
        btnSelectAkm.SetActive(!akm);
        btnUnSelectAkm.SetActive(akm);

        m4 = playerController.selectM4;
        btnSelectM4.SetActive(!m4);
        btnUnSelectM4.SetActive(m4);

        m24 = playerController.selectM24;
        btnSelectM24.SetActive(!m24);
        btnUnSelectM24.SetActive(m24);

        smg9 = playerController.selectSmg9;
        btnSelectSmg9.SetActive(!smg9);
        btnUnSelectSmg9.SetActive(smg9);

        img = playerController.selectImg;
        btnSelectImg.SetActive(!img);
        btnUnSelectImg.SetActive(img);
    }

    public void CheckTxt()
    {
        money.text = playerController.money + " Đ";
        gold.text = playerController.gold + " G";
        moneyDetail.text = playerController.money + " Đ";
        goldDetail.text = playerController.gold + " G";
        moneyDetailBalo.text = playerController.money + " Đ";
        goldDetailBalo.text = playerController.gold + " G";
        txtSoLuongBang.text = "" + playerController.medKit;
    }
    public void CheckBtn()
    {
        for(int i=0; i<btnMuaVatPham.Length; i++)
        {
            if(weapon[i].silencer == !true)
            {
                btnMuaVatPham[i].SetActive(true);
            }
            else
            {
                btnMuaVatPham[i].SetActive(false);
            }
        }
    }
    public void CheckBoolDc()
    {
        itemDcAkm.SetActive(!playerController.akm);
        itemDcM4.SetActive(!playerController.m4);
        itemDcM24.SetActive(!playerController.m24);
        itemDcSmg9.SetActive(!playerController.smg9);
        itemDcImg.SetActive(!playerController.img);
    }
    public void BuyAKM()
    {
        if(playerController.money >= priceAKM)
        {
            playerController.money = playerController.money - priceAKM;
            weapon[0].totalBullet += 90; 
            CheckTxt();
            playerController.akm = false;
            itemAkm.SetActive(playerController.akm);
            shopAKM.SetActive(playerController.akm);
            baloAKM.SetActive(true);

            CheckBoolDc();

            txtTotalBulletAkm.text = "" + weapon[0].totalBullet;

            messengerControl.Messenger("Mua thành công AKM !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để mua AKM !");
        }
    }
    public void BuyM4()
    {
        if(playerController.money >= priceM4)
        {
            playerController.money = playerController.money - priceM4;
            weapon[1].totalBullet += 90; 
            CheckTxt();
            playerController.m4 = false;
            itemM4.SetActive(playerController.m4);
            shopM4.SetActive(playerController.m4);
            baloM4.SetActive(true);

            CheckBoolDc();

            txtTotalBulletM4.text = "" + weapon[1].totalBullet;

            messengerControl.Messenger("Mua thành công M4 !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để mua M4 !");
        }
    }
    public void BuyM24()
    {
        if(playerController.money >= priceM24)
        {
            playerController.money = playerController.money - priceM24;
            weapon[2].totalBullet += 21; 
            CheckTxt();
            playerController.m24 = false;
            itemM24.SetActive(playerController.m24);
            shopM24.SetActive(playerController.m24);
            baloM24.SetActive(true);

            CheckBoolDc();

            txtTotalBulletM24.text = "" + weapon[2].totalBullet;

            messengerControl.Messenger("Mua thành công M24 !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để mua M24 !");
        }
    }
    public void BuySMG9()
    {
        if(playerController.money >= priceSMg9)
        {
            playerController.money = playerController.money - priceSMg9;
            weapon[3].totalBullet += 80; 
            CheckTxt();
            playerController.smg9 = false;
            itemSmg9.SetActive(playerController.smg9);
            shopSmg9.SetActive(playerController.smg9);
            baloSmg9.SetActive(true);

            CheckBoolDc();

            txtTotalBulletSmg9.text = "" + weapon[3].totalBullet;

            messengerControl.Messenger("Mua thành công SMG9 !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để mua SMG9 !");
        }
    }
    public void BuyIMG()
    {
        if(playerController.money >= priceIMG)
        {
            playerController.money = playerController.money - priceIMG;
            weapon[4].totalBullet += 150; 
            CheckTxt();
            playerController.img = false;
            itemImg.SetActive(playerController.img);
            shopIMG.SetActive(playerController.img);
            baloIMG.SetActive(true);

            CheckBoolDc();

            txtTotalBulletImg.text = "" + weapon[4].totalBullet;

            messengerControl.Messenger("Mua thành công IMG !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để mua IMG !");
        }
    }

    public Weapon[] weapon;
    public int[] giaVatPham;
    public int giaBangGac;
    public TextMeshProUGUI txtSoLuongBang;
    public DetailShop detailShopAkm;
    public GameObject[] btnMuaVatPham;

    public void UpdateAKM()
    {
        if(playerController.money >= playerController.priceUpdataAkm)
        {
            if(weapon[0].bulletMax < 40)
            {
                weapon[0].bulletMax += 2;
            }
            weapon[0].speedBullet += 5f;
            weapon[0].damage += 2;
            if(weapon[0].damage == 40)
            {
                weapon[0].recoil -= 0.1f;
            }
            if(weapon[0].damage == 90)
            {
                weapon[0].recoil -= 0.1f;
            }
            if(weapon[0].damage == 130)
            {
                weapon[0].recoil -= 0.1f;
            }
            detailShopAkm.LoadData();

            playerController.money = playerController.money - playerController.priceUpdataAkm;
            playerController.priceUpdataAkm = playerController.priceUpdataAkm + 800;
            txtPriceUpdateAkm.text = "" + playerController.priceUpdataAkm;
            
            CheckTxt();

            messengerControl.Messenger("Nâng cấp thành công !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để nâng cấp AKM !");
        }
    }
    public DetailShop detailShopM4;
    public void UpdateM4()
    {
        if(playerController.money >= playerController.priceUpdataM4)
        {
            if(weapon[1].bulletMax < 40)
            {
                weapon[1].bulletMax += 2;
            }
            weapon[1].speedBullet += 5f;
            weapon[1].damage += 2;
            if(weapon[1].damage == 40)
            {
                weapon[1].recoil -= 0.1f;
            }
            if(weapon[1].damage == 80)
            {
                weapon[1].recoil -= 0.1f;
            }
            if(weapon[1].damage == 100)
            {
                weapon[1].recoil -= 0.1f;
            }

            detailShopM4.LoadData();

            playerController.money = playerController.money - playerController.priceUpdataM4;
            playerController.priceUpdataM4 = playerController.priceUpdataM4 + 800;
            txtPriceUpdateM4.text = "" + playerController.priceUpdataM4;
            
            CheckTxt();

            messengerControl.Messenger("Nâng cấp thành công !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để nâng cấp M4 !");
        }
    }

    public DetailShop detailShopM24;
    public void UpdateM24()
    {
        if(playerController.money >= playerController.priceUpdataM24)
        {
            if(weapon[2].bulletMax < 17)
            {
                weapon[2].bulletMax += 1;
            }
            weapon[2].speedBullet += 5f;
            weapon[2].damage += 5;
            if(weapon[2].damage == 250)
            {
                weapon[2].recoil -= 0.2f;
            }
            if(weapon[2].damage == 300)
            {
                weapon[2].recoil -= 0.2f;
            }
            if(weapon[2].damage == 350)
            {
                weapon[2].recoil -= 0.2f;
            }

            detailShopM24.LoadData();

            playerController.money = playerController.money - playerController.priceUpdataM24;
            playerController.priceUpdataM24 = playerController.priceUpdataM24 + 800;
            txtPriceUpdateM24.text = "" + playerController.priceUpdataM24;
            
            CheckTxt();

            messengerControl.Messenger("Nâng cấp thành công !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để nâng cấp M24 !");
        }
    }

    public DetailShop detailShopSmg9;
    public void UpdateSmg9()
    {
        if(playerController.money >= playerController.priceUpdataSmg9)
        {
            if(weapon[3].bulletMax < 30)
            {
                weapon[3].bulletMax += 2;
            }
            weapon[3].speedBullet += 5f;
            weapon[3].damage += 2;
            if(weapon[3].damage == 50)
            {
                weapon[3].recoil -= 0.1f;
            }
            if(weapon[3].damage == 80)
            {
                weapon[3].recoil -= 0.1f;
            }
            if(weapon[3].damage == 100)
            {
                weapon[3].recoil -= 0.1f;
            }

            detailShopSmg9.LoadData();

            playerController.money = playerController.money - playerController.priceUpdataSmg9;
            playerController.priceUpdataSmg9 = playerController.priceUpdataSmg9 + 800;
            txtPriceUpdateSmg9.text = "" + playerController.priceUpdataSmg9;
            
            CheckTxt();

            messengerControl.Messenger("Nâng cấp thành công !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để nâng cấp SMG9 !");
        }
    }

    public DetailShop detailShopImg;
    public void UpdateImg()
    {
        if(playerController.money >= playerController.priceUpdataImg)
        {
            if(weapon[4].bulletMax < 120)
            {
                weapon[4].bulletMax += 5;
            }
            weapon[4].speedBullet += 2f;
            weapon[4].damage += 2;
            if(weapon[4].damage == 50)
            {
                weapon[4].recoil -= 0.1f;
            }
            if(weapon[4].damage == 80)
            {
                weapon[4].recoil -= 0.1f;
            }
            if(weapon[4].damage == 120)
            {
                weapon[4].recoil -= 0.1f;
            }
            if(weapon[4].damage == 200)
            {
                weapon[4].recoil -= 0.2f;
            }
            detailShopImg.LoadData();

            playerController.money = playerController.money - playerController.priceUpdataImg;
            playerController.priceUpdataImg = playerController.priceUpdataImg + 800;
            txtPriceUpdateImg.text = "" + playerController.priceUpdataImg;
            
            CheckTxt();

            messengerControl.Messenger("Nâng cấp thành công !");
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ để nâng cấp IMG !");
        }
    }

    public void SelectGunAkm()
    {
        if(!m4 && !m24 && !smg9 && !img)
        {
            btnSelectAkm.SetActive(false);
            btnUnSelectAkm.SetActive(true);
            akm = true;
            playerController.selectAkm = akm;
        }
        else
        {
            messengerControl.Messenger("Chỉ được chọn 1 vũ khí !");
        }
    }
    public void UnSelectGunAkm()
    {
        btnSelectAkm.SetActive(true);
        btnUnSelectAkm.SetActive(false);
        akm = false;
        playerController.selectAkm = akm;
    }

    public void SelectGunM4()
    {
        if(!akm && !m24 && !smg9 && !img)
        {
            btnSelectM4.SetActive(false);
            btnUnSelectM4.SetActive(true);
            m4 = true;
            playerController.selectM4 = m4;
        }
        else
        {
            messengerControl.Messenger("Chỉ được chọn 1 vũ khí !");
        }
    }
    public void UnSelectGunM4()
    {
        btnSelectM4.SetActive(true);
        btnUnSelectM4.SetActive(false);
        m4 = false;
        playerController.selectM4 = m4;
    }
    public void SelectGunM24()
    {
        if(!akm && !m4 && !smg9 && !img)
        {
            btnSelectM24.SetActive(false);
            btnUnSelectM24.SetActive(true);
            m24 = true;
            playerController.selectM24 = m24;
        }
        else
        {
            messengerControl.Messenger("Chỉ được chọn 1 vũ khí !");
        }
    }
    public void UnSelectGunM24()
    {
        btnSelectM24.SetActive(true);
        btnUnSelectM24.SetActive(false);
        m24 = false;
        playerController.selectM24 = m24;
    }
    public void SelectGunSmg9()
    {
        if(!akm && !m4 && !m24 && !img)
        {
            btnSelectSmg9.SetActive(false);
            btnUnSelectSmg9.SetActive(true);
            smg9 = true;
            playerController.selectSmg9 = smg9;
        }
        else
        {
            messengerControl.Messenger("Chỉ được chọn 1 vũ khí !");
        }
    }
    public void UnSelectGunSmg9()
    {
        btnSelectSmg9.SetActive(true);
        btnUnSelectSmg9.SetActive(false);
        smg9 = false;
        playerController.selectSmg9 = smg9;
    }
    public void SelectGunImg()
    {
        if(!akm && !m4 && !m24 && !smg9)
        {
            btnSelectImg.SetActive(false);
            btnUnSelectImg.SetActive(true);
            img = true;
            playerController.selectImg = img;
        }
        else
        {
            messengerControl.Messenger("Chỉ được chọn 1 vũ khí !");
        }
    }
    public void UnSelectGunImg()
    {
        btnSelectImg.SetActive(true);
        btnUnSelectImg.SetActive(false);
        img = false;
        playerController.selectImg = img;
    }

    public void BuyDcAkm()
    {
        if(playerController.money >= priceDcAkm)
        {
            if(weapon[0].totalBullet < 400)
            {
                playerController.money = playerController.money - priceDcAkm;
                CheckTxt();
                weapon[0].totalBullet += 30; 
                txtTotalBulletAkm.text = "" + weapon[0].totalBullet;

                messengerControl.Messenger("Mua thành công !");
            }
            else
            {

                messengerControl.Messenger("Lượng băng đạn đã đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }

    public void BuyDcM4()
    {
        if(playerController.money >= priceDcM4)
        {
            if(weapon[1].totalBullet < 400)
            {
                playerController.money = playerController.money - priceDcM4;
                CheckTxt();
                weapon[1].totalBullet += 30; 
                txtTotalBulletM4.text = "" + weapon[1].totalBullet;

                messengerControl.Messenger("Mua thành công !");
            }
            else
            {

                messengerControl.Messenger("Lượng băng đạn đã đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }
    public void BuyDcM24()
    {
        if(playerController.money >= priceDcM24)
        {
            if(weapon[2].totalBullet < 100)
            {
                playerController.money = playerController.money - priceDcM24;
                CheckTxt();
                weapon[2].totalBullet += 7; 
                txtTotalBulletM24.text = "" + weapon[2].totalBullet;

                messengerControl.Messenger("Mua thành công !");
            }
            else
            {

                messengerControl.Messenger("Lượng băng đạn đã đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }
    public void BuyDcSmg9()
    {
        if(playerController.money >= priceDcSmg9)
        {
            if(weapon[3].totalBullet < 500)
            {
                playerController.money = playerController.money - priceDcSmg9;
                CheckTxt();
                weapon[3].totalBullet += 20; 
                txtTotalBulletSmg9.text = "" + weapon[3].totalBullet;

                messengerControl.Messenger("Mua thành công !");
            }
            else
            {

                messengerControl.Messenger("Lượng băng đạn đã đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }
    public void BuyDcImg()
    {
        if(playerController.money >= priceDcImg)
        {
            if(weapon[4].totalBullet < 600)
            {
                playerController.money = playerController.money - priceDcImg;
                CheckTxt();
                weapon[4].totalBullet += 75; 
                txtTotalBulletImg.text = "" + weapon[4].totalBullet;

                messengerControl.Messenger("Mua thành công !");
            }
            else
            {
                messengerControl.Messenger("Lượng băng đạn đã đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }

    public void MuaVatPham(int i)
    {
        if(playerController.gold >= giaVatPham[i])
        {
            playerController.gold -= giaVatPham[i];
            weapon[i].silencer = true;
            CheckTxt();
            CheckBtn();
            messengerControl.Messenger("Mua thành công !");
        }
        else
        {
            messengerControl.Messenger("Số vàng không đủ !");
        }
    }

    public void MuaBangGac()
    {
        if(playerController.money > giaBangGac)
        {
            if(playerController.medKit < 10)
            {
                playerController.money -= giaBangGac;
                playerController.medKit += 1;
                CheckTxt();
                messengerControl.Messenger("Mua thành công !");
            }
            else
            {
                messengerControl.Messenger("Balo Không đủ !");
            }
        }
        else
        {
            messengerControl.Messenger("Số tiền không đủ !");
        }
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progrssText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
