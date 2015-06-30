using UnityEngine;
using System.Collections;

public class EnemyHitDetect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.bullet)
        {
            var zombie = gameObject.transform.parent.gameObject;
            var animator = zombie.GetComponent<Animator>();
            Destroy(other.gameObject);
            //Destroy(zombie);
            animator.SetBool("Walk", false);
            animator.SetBool("Death", true);
        }
    }
}
