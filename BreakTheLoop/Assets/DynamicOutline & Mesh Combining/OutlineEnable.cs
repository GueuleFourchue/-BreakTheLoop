using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEnable : MonoBehaviour {

    public Camera cam;
    DynamicOutline enabledOutline;

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 8 && enabledOutline == null)
            {
                enabledOutline = hit.transform.GetComponent<DynamicOutline>();
                enabledOutline.ShowOutline(true);
            }
            else if (hit.transform.gameObject.layer != 8 && enabledOutline != null)
            {
                enabledOutline.ShowOutline(false);
                enabledOutline = null;
            }
        }
        else if (enabledOutline != null)
        {
            enabledOutline.ShowOutline(false);
            enabledOutline = null;
        }
    }

}
