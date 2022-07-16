using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKMPlayerAnimation : MonoBehaviour
{
    public Animator animatorAKM;
    public Animator animatorM4;
    public Animator animatorIMG;
    public Animator animatorSMG9;
    public Animator animator;

    public bool isAkmAnim = false;
    public bool isM4Anim = false;
    public bool isImgAnim = false;
    public bool isSmg9Anim = false;

    private void Awake() 
    {
        if(isAkmAnim)
        {
            animator = animatorAKM.GetComponent<Animator>();
        }

        if(isM4Anim)
        {
            animator = animatorM4.GetComponent<Animator>();
        }

        if(isImgAnim)
        {
            animator = animatorIMG.GetComponent<Animator>();
        }

        if(isSmg9Anim)
        {
            animator = animatorSMG9.GetComponent<Animator>();
        }

    }

}
