using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Receptacle : MonoBehaviour {

    public float timeToAttractOrb;

    public GameObject Trail_01;
    public GameObject Trail_02;
    public float activationDelay;

    public Animator animDoor;
    public Player_GrabObjet playerGrabObject;

    [HideInInspector]
    public Transform orb;


    //DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Activation());
        }     
    }
    //DEBUG

    public IEnumerator Activation()
    {
        orb.GetComponent<ObjectHandMove>().enabled = false;
        orb.DOMove(transform.position, timeToAttractOrb);

        yield return new WaitForSeconds(timeToAttractOrb);

        Trail_01.SetActive(true);
        Trail_02.SetActive(true);
        Trail_01.GetComponent<TrailMove>().MoveToPoint();
        Trail_02.GetComponent<TrailMove>().MoveToPoint();

        playerGrabObject.canThrow = true;

        yield return new WaitForSeconds(activationDelay);

        animDoor.enabled = true;
    }
}
