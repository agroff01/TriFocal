using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Pathway and Waypoint Logic for moving platforms followed from this video: https://www.youtube.com/watch?v=ly9mK0TGJJo&ab_channel=KetraGames

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPathway;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousPoint;
    private Transform _targetPoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private void TargetNextWaypoint()
    {
        _previousPoint = _waypointPathway.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPathway.GetNextWaypointIndex(_targetWaypointIndex);
        _targetPoint = _waypointPathway.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousPoint.position, _targetPoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercent = _elapsedTime / _timeToWaypoint;
        transform.position = Vector3.Lerp(_previousPoint.position, _targetPoint.position, elapsedPercent);

        if (elapsedPercent >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) collision.transform.SetParent(null);
    }

}