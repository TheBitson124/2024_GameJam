using System;
using System.Collections;
using UnityEngine;

public class Orbiting_Enemy : Enemy_Script
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] Waypoints;
    private int NextWaypoint = 0;

    private void Awake()
    {
        StartCoroutine(MovingCoroutine());
    }

    private IEnumerator MovingCoroutine()
    {
        while (true)
        {
            float distance = Vector2.Distance(transform.position, Waypoints[NextWaypoint].transform.position);
            
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[NextWaypoint].transform.position,
                Speed * Time.deltaTime);
            
            if (distance < 0.1f)
            {
                Turn();
            }

            yield return null;
        }
    }

    private void Turn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += Waypoints[NextWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        
        NextWaypoint = (NextWaypoint + 1) % Waypoints.Length;
    }
}