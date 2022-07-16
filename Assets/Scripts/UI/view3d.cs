using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view3d : MonoBehaviour
{
    protected Vector3 poslastFame;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            poslastFame = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - poslastFame;
            poslastFame = Input.mousePosition;

            var axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.5f, axis) * transform.rotation;
        }
    }
}
