using UnityEngine;
using System.Collections;

public class HitDetectTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit" + collision.gameObject.tag);
    }
}
