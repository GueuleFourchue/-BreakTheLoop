using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {

    public bool standardLoop;
    public LoopTeleport loopTeleport;
    public string GatingObject;
    public Vector3 TpPosition;

    public bool keepXPosition;
    public bool keepYPosition;
    public bool keepZPosition;

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
                if (keepXPosition)
                    loopTeleport.finalPosition = new Vector3 (other.transform.position.x, TpPosition.y, TpPosition.z);
                if (keepYPosition)
                    loopTeleport.finalPosition = new Vector3(TpPosition.x, other.transform.position.y, TpPosition.z);
                if (keepZPosition)
                    loopTeleport.finalPosition = new Vector3(TpPosition.x, TpPosition.y, other.transform.position.z);

                loopTeleport.StartCoroutine("Teleport");
            }
        }
    }
}
