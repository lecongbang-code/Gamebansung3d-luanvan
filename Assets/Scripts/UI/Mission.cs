using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public static class ButtonExtension
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate(){
            OnClick (param);
        });
    }
}

public class Mission : MonoBehaviour
{
    public MessengerControl messengerControl;

    public PlayerController playerController;

    public MissionControl missionControl;

    public Main_controller main_Controller;

    public GameObject content;

    public Transform contentView;

    [Serializable]
    public struct MissionGame
    {
        public string tenNV;
        public string thuongNV;
        public Sprite hinh;
    }

    [SerializeField] MissionGame[] missionGame = null;

    Button btnNhan;

    void Start()
    {
        MissionG();
    }

    void MissionG()
    {
        GameObject item = content.transform.GetChild(0).gameObject;
        GameObject gameObj;
        int n = missionGame.Length;
        for(int i = 0; i < n; i++)
        {
            gameObj = Instantiate(item, content.transform);
            gameObj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = missionGame[i].hinh;
            gameObj.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = missionGame[i].tenNV;
            gameObj.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = missionControl.zombie[i].ToString();
            gameObj.transform.GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = missionControl.zombieKill[i].ToString();
            gameObj.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = missionGame[i].thuongNV;

            int tiendo = missionControl.zombie[i];
            int mucTieu = missionControl.zombieKill[i];

            if(tiendo >= mucTieu)
            {
                gameObj.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                gameObj.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            } 
            btnNhan = gameObj.transform.GetChild(0).GetChild(2).GetComponent<Button>();
            btnNhan.interactable = true;
            btnNhan.AddEventListener(i, ItemClicked);
        }
        Destroy(item);
    }

    void ItemClicked(int itemIndex)
    {
        // hi???u th??? h???p tho???i th??ng b??o
        messengerControl.Messenger("Nh???n th??nh c??ng !");
        // t??ng m???c ti??u c???a nhi???m v??? th??? "itemIndex" l??n th??m 15
        missionControl.zombieKill[itemIndex] += 15;
        // c???ng ti???n sau khi nh???n th?????ng
        int money = Int32.Parse(missionGame[itemIndex].thuongNV);
        playerController.money += money;
        // c???p nh???t l???i c??c text UI
        main_Controller.CheckTxt();
        // c???p nh???t l???i ti???n ????? v?? m???c ti??u c???a ph???n t??? th??? itemIndex
        int tiendo = missionControl.zombie[itemIndex];
        int mucTieu = missionControl.zombieKill[itemIndex];
        // n???u ti???n ????? nh??? h??n m???c ti??u th?? ???n btn c?? th??? nh???n ??? ph???n t??? itemIndex ????
        if(tiendo < mucTieu)
        {
            contentView.GetChild(itemIndex).GetChild(0).GetChild(2).gameObject.SetActive(false);
            contentView.GetChild(itemIndex).GetChild(0).GetChild(3).gameObject.SetActive(true);
        }
        contentView.GetChild(itemIndex).GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = missionControl.zombieKill[itemIndex].ToString();
    }
}
