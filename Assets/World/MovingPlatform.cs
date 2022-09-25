using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    [Range(0.1f, 20f)]
    public float moveSpeed = 5f;

    private int index;
    private Transform current, next;

    private void Start()
    {
        if (waypoints.Count < 2)
            return;

        current = waypoints[index];
        next = waypoints[index + 1];
    }

    void FixedUpdate()
    {
        if (waypoints.Count < 2)
            return;

        float step = Time.fixedDeltaTime * moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, next.position, step);

        if (Vector3.Distance(transform.position, next.position) < 0.001f)
        {
            if (index == waypoints.Count - 1)
            {
                index = 0;
                current = waypoints[index];
                next = waypoints[index + 1];
            }
            else
            {
                index++;
                current = waypoints[index];
                next = waypoints[0];
            }
        }
    }

    [ContextMenu("New Waypoint")]
    public void NewWaypoint()
    {
        GameObject waypoint = new GameObject("Waypoint");
        waypoint.transform.position = transform.position;
        waypoints.Add(waypoint.transform);
    }
}
