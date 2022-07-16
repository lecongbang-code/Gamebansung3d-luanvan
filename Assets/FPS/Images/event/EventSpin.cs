using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EventSpin : MonoBehaviour 
{
    public PlayerController playerController;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI keyText;
    public Weapon[] weapon;
    public MessengerControl messengerControl;
	int randVal;
	float timeInterval;
	bool isCoroutine;
	int finalAngle;
	[Space (10)]
    public TextMeshProUGUI winText;
	int section;
	float totalAngle;
	[Space (10)]
	public string[] PrizeName;
	[Space (10)]
	public int[] Prize;
    [Space (10)]
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;

	void Start () 
	{
		isCoroutine = true;
		section = PrizeName.Length;
		totalAngle = 360 / section;
        UpdateTxt () ;
	}

    void UpdateTxt () 
	{
		goldText.text = playerController.gold + " G";
        keyText.text = playerController.key + " K";
	}

	public void ClickSpin()
	{
		if (isCoroutine) 
		{
			if (playerController.key >= 5) 
			{
				winText.text = ".........";
				playerController.key -= 5;
				StartCoroutine (Spin ());
				UpdateTxt () ;
			}
			else
			{
				messengerControl.Messenger("Không đủ chìa khóa !");
			}
		} 
	}

	IEnumerator Spin()
	{
		isCoroutine = false;
		
		randVal = Random.Range (200, 300);

		timeInterval = 0.0001f*Time.deltaTime*2;

		for (int i = 0; i < randVal; i++) 
		{
			transform.Rotate (0, 0, (totalAngle/2));

			if (i > Mathf.RoundToInt (randVal * 0.2f))
			{
				timeInterval = 0.5f*Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.5f))
			{
				timeInterval = 1f*Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.7f))
			{
				timeInterval = 1.5f*Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.8f))
			{
				timeInterval = 2f*Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.9f))
			{
				timeInterval = 2.5f*Time.deltaTime;
			}

			yield return new WaitForSeconds (timeInterval);
		}

		if (Mathf.RoundToInt (transform.eulerAngles.z) % totalAngle != 0)
		{
			transform.Rotate (0, 0, totalAngle/2);
		}
		finalAngle = Mathf.RoundToInt (transform.eulerAngles.z);

		for (int i = 0; i < section; i++) 
		{
			if (finalAngle == i * totalAngle)
			{
				winText.text = PrizeName [i];

                if(PrizeName [i] == "Giảm thanh M24")
                {
                    weapon[0].silencer = true;
                }
                if(PrizeName [i] == "Giảm thanh AKM")
                {
                    weapon[1].silencer = true;
                }
                playerController.gold += Prize[i];
				print (Prize[i]);
                UpdateTxt () ;
			}
		}
		isCoroutine = true;
	}
    
    public void LoadLevel (int sceneIndex)
    {
		if(isCoroutine)
		{
			StartCoroutine(LoadAsynchronously(sceneIndex));
		}
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
}

