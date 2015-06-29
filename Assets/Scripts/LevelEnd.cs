using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            //Thats it. He's dead jim.
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.Die();
        }
    }
}
