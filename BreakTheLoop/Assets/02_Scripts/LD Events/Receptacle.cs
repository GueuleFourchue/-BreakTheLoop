using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptacle : MonoBehaviour {

    public GameObject Trail_01;
    public GameObject Trail_02;
    public float activationDelay;

    public Animator animDoor;


    //DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Activation());
        }     
    }
    //DEBUG

    IEnumerator Activation()
    {
        Trail_01.SetActive(true);
        Trail_02.SetActive(true);
        Trail_01.GetComponent<TrailMove>().MoveToPoint();
        Trail_02.GetComponent<TrailMove>().MoveToPoint();

        yield return new WaitForSeconds(activationDelay);

        animDoor.enabled = true;
    }
}
