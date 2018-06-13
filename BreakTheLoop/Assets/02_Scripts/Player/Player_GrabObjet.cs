using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_GrabObjet : MonoBehaviour {

    public Transform holdingParent;
    public float throwFoce;
    public Transform grabbedObject;
    public Transform hand;
    public LoopTeleport teleport;
    public SoundEffects soundEffects;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && grabbedObject == null)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.transform.CompareTag("Grabable"))
                {
                    GrabObject(hit.transform);
                    soundEffects.PlaySoundOnce(soundEffects.pickup);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && grabbedObject != null)
        {
            //ThrowObject();
        }
    }

    void GrabObject(Transform obj)
    {
        grabbedObject = obj;
        grabbedObject.parent = holdingParent;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<ObjectHandMove>().enabled = true;

        if (obj.name == "Grabable_OrbRed")
        {
            obj.GetComponent<OrbPulseGlow>().enabled = true;
        }
    }

    void ThrowObject()
    {
        grabbedObject.GetComponent<ObjectHandMove>().enabled = false;
        grabbedObject.parent = null;
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Camera.main.transform.forward * throwFoce);
        grabbedObject = null;
    }
}
