using UnityEngine;
using TMPro;

public class MessengerControl : MonoBehaviour
{
    public Transform MSG;
    GameObject msg;
    public GameObject objMessenger;
    
    public MsgItem msgItem;

    void Start()
    {
        msg = MSG.GetChild(0).gameObject;
    }

    public void Messenger(string txtMsg)
    {
        // gán giá trị mà hàm đã nhận cho đoạn text msg
        msgItem.textMsg.text = "" + txtMsg;
        // tạo một mgs mới ở vị trí MSG 
        msg = Instantiate(objMessenger, MSG);
        // sau đó đối tungowngj này sẽ bị hủy bỏ sao 1 giây
        Destroy(msg, 1f);
    }
}
