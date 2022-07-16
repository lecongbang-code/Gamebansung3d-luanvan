using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Setting")]
public class SetUpCammera : ScriptableObject
{
    [Header("Settings")]
    public float speedNoAiming = 0f;

    public float speedAiming = 0f;
}
