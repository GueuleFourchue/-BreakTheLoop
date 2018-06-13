using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {

    public bool standardLoop;
    public LoopTeleport loopTeleport;
    public string GatingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!standardLoop && other.GetComponent<Player_GrabObjet>().grabbedObject != null && other.GetComponent<Player_GrabObjet>().grabbedObject.name == GatingObject)
            {
                return;
            }
            else 
            {
                loopTeleport.StartCoroutine("Teleport");
            }
        }
    }
}
