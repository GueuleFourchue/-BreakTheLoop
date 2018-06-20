using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrailMove : MonoBehaviour {

    public Transform[] wayPoints;
    public float timeToGoToPoint;

    Transform actualWaypoint;
    int index = 0;

    private void Start()
    {
        actualWaypoint = wayPoints[index];
    }

    public void MoveToPoint()
    {
        transform.DOMove(actualWaypoint.position, timeToGoToPoint).OnComplete(() =>
        {
            if (wayPoints[index + 1] != null)
            {
                index++;
                actualWaypoint = wayPoints[index];
                MoveToPoint();
            }
        });
    }
}
