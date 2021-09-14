using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private WaveConfig _waveConfig;
    
    private List<Transform> waypoints;

    private int waypointIndex = 0;

    [SerializeField] private float moveSpeed = 3f;
    private void Awake()
    {
        waypoints = _waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }


    void Move()
    {
        if ( waypointIndex <= waypoints.Count -1 )
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position =
                Vector2.MoveTowards(
                    transform.position, 
                    targetPosition, 
                    movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
