using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;
    public float restingSize;
    public float maxSize;
    public float speed;
    bool isShoot;

    float currentSize;

    void Start() 
    {
        reticle = GetComponent<RectTransform>();    
    }

    void Update() 
    {
        if(isShoot || isMove())
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }
        reticle.sizeDelta = new Vector2(currentSize, currentSize);
    }

    bool isMove()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void isReticle(bool getShoot)
    {
        isShoot = getShoot;
    }
}