using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    private float timer = 0.0f;
    public float bobbingWalkSpeed = 0.18f;
    public float bobbingRunSpeed = 0.18f;
    public float bobbingWalkAmount = 0.2f;
    public float bobbingRunAmount = 0.2f;
    public float midpoint = 2.0f;

    public bool isRunning;

    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }

        else
        {
            waveslice = Mathf.Sin(timer);
            if (isRunning)
                timer = timer + bobbingRunSpeed;
            else
                timer = timer + bobbingWalkSpeed;

            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange;
            if (isRunning)
                translateChange = waveslice * bobbingRunAmount;
            else 
                translateChange = waveslice * bobbingWalkAmount;

            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            transform.localPosition = new Vector3(transform.localPosition.x, midpoint + translateChange, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, midpoint, transform.localPosition.z);
        }
    }

}

