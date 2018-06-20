using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_CloseDoorBehind : MonoBehaviour {

    public GameObject popObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popObject.SetActive(true);
        }
    }

}
