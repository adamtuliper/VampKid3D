using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public Rigidbody Projectile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var projectile = Instantiate(Projectile, transform.position, Quaternion.identity) as Rigidbody;
            projectile.velocity = transform.TransformDirection(Vector3.forward * 10);
            Destroy(projectile.gameObject,5f);
        }
    }
}
