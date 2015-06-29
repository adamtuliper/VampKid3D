using UnityEngine;
using System.Collections;

public class ZombieHit : MonoBehaviour
{

    private PlayerHealth _playerHealth;
    // Use this for initialization
    void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hitting player");
        _playerHealth.TakeDamage(25);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger stay on zombie attack");
    }
}
