using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

    float originFOV;
    public float newFOV;

    bool changeToOriginFOV;

    private void Start()
    {
        originFOV = Camera.main.fieldOfView;
    }

    private void Update()
    {
        CheckRun();
    }

    void CheckRun()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetButton("Run"))
            {
                NewFOV();
            }
           
        }
        if (Input.GetButtonUp("Run"))
        {
            changeToOriginFOV = true;
        }    

        if (changeToOriginFOV)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, originFOV, 0.06f);
            if (Camera.main.fieldOfView <= originFOV)
            {
                changeToOriginFOV = false;
                Camera.main.fieldOfView = originFOV;
            }
        }
    }

    void NewFOV()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, newFOV, 0.06f);
    }
}
