using UnityEngine;
using System.Collections;

public class Navigator : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;

    private Vector3 _destination;

    private Vector3 _startingLocation;
    private Transform _playerPosition;
	// Use this for initialization
	void Start ()
	{
	    _startingLocation = transform.position;

	    _destination = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
	    _playerPosition = GameObject.FindGameObjectWithTag(Tags.player).transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_destination);
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!_navMeshAgent.pathPending 
            &&
            _navMeshAgent.remainingDistance 
            <= 
            _navMeshAgent.stoppingDistance)
        // We arrived, reset the destination
        {
            Debug.Log("Arrived - done navigating, setting location to starting location");
            _navMeshAgent.SetDestination(_playerPosition.position);
            
        }
    }
}
