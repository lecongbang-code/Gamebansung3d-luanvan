using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Point : MonoBehaviour
{
    public InterFace interFace;
    public GameObject shotKillObj;
    public TextMeshProUGUI textKillTop;
    public TextMeshProUGUI textKillBot;
    public TextMeshProUGUI textPoint;

    public TextMeshProUGUI txtmuctieu;
    public TextMeshProUGUI txthiencon;

    public TextMeshProUGUI txtzombiebiha;

    public TextMeshProUGUI txtzombiebihadead;

    public GameObject tamban;

    public GameObject caidat;

    float timeLiveKill;
    int point;
    public int kill;

    public GameObject option;
    public GameObject canvasGame;
    public GameObject canvasGun;

    public PlayerController playerController;
    public MissionControl missionControl;
    public GameObject youWin;
    public GameObject miniMap;
    public TextMeshProUGUI monney;
    
    int killZombie;

    public int zombieButcher;
    public int zombieCrazy;
    public int zombieFat;
    public int zombieMechanic;
    public int zombiePrisoner;

    int hiencon;

    int pointGame;

    public int scene;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        killZombie = zombieButcher + zombieCrazy + zombieFat + zombieMechanic + zombiePrisoner;
        hiencon = killZombie;
        pointGame = killZombie/2;
        txthiencon.text = hiencon + " : Zombie" ;
    }

    void Update() 
    {
        
        if(timeLiveKill < 5)
        {
            timeLiveKill += Time.deltaTime;
        }

        // test demo

        if(timeLiveKill >= 5)
        {
            shotKillObj.SetActive(false);


            // kết thúc màng chơi nếu đã tiêu diệt hết zombie
            if(kill >= killZombie)
            {
                // hiểu thị con trỏ chuột cho người chơi thao tác
                Cursor.lockState = CursorLockMode.None;
                // hiểu thị UI thông báo
                youWin.SetActive(true);
                // tắt UI không cần thiết
                miniMap.SetActive(false);
                // dừng thời gian của game
                Time.timeScale = 0;
                // cộng các điểm cho nhiệm vụ
                missionControl.zombie[5] += killZombie;
                missionControl.zombie[0] += zombieButcher;
                missionControl.zombie[1] += zombieCrazy;
                missionControl.zombie[2] += zombieFat;
                missionControl.zombie[3] += zombieMechanic;
                missionControl.zombie[4] += zombiePrisoner;
                // đánh dấu màng chơi
                playerController.scene[scene] = true;
                // cộng tiền cho người chơi 
                playerController.money += point*pointGame;
                // cập nhật số tiền cho Text UI
                monney.text = "Tiền thưởng : " + point*pointGame + "đ";
                // cập nhật sô điểm đã hạ zombie 
                txtzombiebiha.text = "Zombie đã bị hạ : " + kill;
                // thủ thật để điều kiện chỉ đúng 1 lần
                kill -=1;
            }
        }

        if(kill >= killZombie)
        {
            tamban.SetActive(false);
            miniMap.SetActive(false);
            caidat.SetActive(false);
            youWin.SetActive(true);
            interFace.timesSeconds += Time.deltaTime;
        }

        // test demo
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            option.SetActive(true);
            canvasGame.SetActive(false);
            canvasGun.SetActive(false);
            miniMap.SetActive(false);
        }

        txtmuctieu.text = "Mục tiêu : " + killZombie;
    }

    public void CountZombieDead()
    {
        txtzombiebihadead.text = "Zombie đã bị hạ : " + kill;
    }
    public void SelectPoint(int getPoint)
    {
        point += getPoint;
        textPoint.text = "Điểm : " + point;
    }

    public void SelectShootKill()
    {
        kill +=1;
        hiencon -= 1;
        txthiencon.text = hiencon + " : Zombie" ;
        textKillBot.text = "Đã Hạ : " + kill;
        textKillTop.text = "Đã Hạ : " + kill;
        shotKillObj.SetActive(true);
        timeLiveKill = 0f;
    }
}
