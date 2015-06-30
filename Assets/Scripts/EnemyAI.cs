using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.


    private EnemySight enemySight;                          // Reference to the EnemySight script.
    private NavMeshAgent nav;                               // Reference to the nav mesh agent.
    //private Transform _player;                               // Reference to the player's transform.
    //private PlayerHealth _playerHealth;                      // Reference to the PlayerHealth script.
    //private PlayerScore _playerScore;
    private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
    private float chaseTimer;                               // A timer for the chaseWaitTime.
    private float patrolTimer;                              // A timer for the patrolWaitTime.
    private int wayPointIndex;                              // A counter for the way point array.
    private Animator _animator;


    private Transform _player;                               // Reference to the player's transform.
    private PlayerHealth _playerHealth;                      // Reference to the PlayerHealth script.
    private PlayerScore _playerScore;

    private void Awake()
    {
        // Setting up the references.
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerScore = _player.GetComponent<PlayerScore>();
        _animator = GetComponent<Animator>();
        _playerHealth = _player.GetComponent<PlayerHealth>();
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
//        Debug.Log(_playerScore);
        _playerHealth = _player.GetComponent<PlayerHealth>();
        // If the player is in sight and is alive...
        if (enemySight.playerInSight && _playerHealth.Health > 0f)
        {
//            Debug.Log("Shooting");
            // ... shoot.
            Shooting();
        }
        // If the player has been sighted and isn't dead...
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && _playerHealth.Health > 0f)
        {
//            Debug.Log("Chasing");
            // ... chase.
            Chasing();
        }
        // Otherwise...
        else
        {
            Debug.Log("Patrolling");
            // ... patrol.
            Patrolling();
        }
    }


    void Shooting()
    {
        // Stop the enemy where it is.
        nav.Stop();
    }


    void Chasing()
    {
        // Create a vector from the enemy to the last sighting of the player.
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

        // If the the last personal sighting of the player is not close...
        if (sightingDeltaPos.sqrMagnitude > 4f)
        {
//            Debug.Log("Setting enemy nav destination to last sighting");
            // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
            nav.destination = enemySight.personalLastSighting;
        }
        
        // Set the appropriate speed for the NavMeshAgent.
        nav.speed = chaseSpeed;

        // If near the last personal sighting...
        if (nav.remainingDistance < nav.stoppingDistance)
        {
           // Debug.Log("Our chase has finished. Wait before chasing again");
            // ... increment the timer.
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime)
            {
                Debug.Log("Exceeded chase wait time, lets reset the last player spotted position to empty");
                // ... reset last global sighting, the last personal sighting and the timer.
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
    }


    void Patrolling()
    {
        // Set an appropriate speed for the NavMeshAgent.
        nav.speed = patrolSpeed;

        // If near the next waypoint or there is no destination...
        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            Debug.Log("Near waypoint or no next destination");
            // ... increment the timer.
            patrolTimer += Time.deltaTime;
            //Debug.Log("Patrol timer:" + patrolTimer);
            // If the timer exceeds the wait time...
            if (patrolTimer >= patrolWaitTime)
            {
                Debug.Log("Exceeded wait time.. going to next waypoint");
                // ... increment the wayPointIndex.
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                // Reset the timer.
                patrolTimer = 0;
                //  _animator.SetBool("Walk", true);
            }
            else
            {
                //else we've arrived at waypoint. hang out idle. not time to move yet.
                Debug.Log("Not time to move yet, hanging out waiting for timer");
                _animator.SetBool("Walk", false);
            }

            if (nav.destination != patrolWayPoints[wayPointIndex].position)
            {
                Debug.Log("Setting walk..");
                _animator.SetBool("Walk", true);
                // Set the destination to the patrolWayPoint.
                nav.destination = patrolWayPoints[wayPointIndex].position;
            }
           
        }
        else
        {
            // _animator.SetBool("Walk", true);
            Debug.Log("Patrol time = 0");
            // If not near a destination, reset the timer.
            patrolTimer = 0;
        }

    }
}