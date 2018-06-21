using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UseOrb : MonoBehaviour {

    public Player_GrabObjet playerGrabObject;
    public Transform HoldingObjectParent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.transform.CompareTag("Receptacle"))
                {
                    ActivateReceptacle(hit.transform.GetComponent<Receptacle>());
                }
            }
        }
    }

    void ActivateReceptacle(Receptacle receptacle)
    {
        Debug.Log("clickWork");
        receptacle.orb = playerGrabObject.grabbedObject;
        playerGrabObject.grabbedObject = null;
        HoldingObjectParent.GetChild(0).parent = null;
        receptacle.StartCoroutine("Activation");
    }
}
