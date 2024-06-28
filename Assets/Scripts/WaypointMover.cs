using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypoint;

    private void Update()
    {
        UpdateCurrentWaypoint();
        MoveTowardsNextWaypoint();
    }

    private void UpdateCurrentWaypoint()
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            if (transform.position == _waypoints[i].position)
            {
                _currentWaypoint = i;
            }
        }
    }

    private void MoveTowardsNextWaypoint()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }
}

