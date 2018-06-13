using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandMove : MonoBehaviour {

    public Transform handTransform;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, handTransform.position, 0.2f);
    }

   
}
