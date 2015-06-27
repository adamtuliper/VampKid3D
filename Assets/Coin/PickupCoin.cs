using UnityEngine;
using System.Collections;

public class PickupCoin : MonoBehaviour
{


    public bool Clicked;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Clicked)
        {
            Clicked = false;
            transform.parent = Camera.main.transform;
            StartCoroutine(MoveToScore());
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent = Camera.main.transform;
            StartCoroutine(MoveToScore());
        }
    }

    IEnumerator MoveToScore()
    {
        while (true)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 5));
            transform.position = Vector3.Lerp(transform.position, worldPoint, 2 * Time.deltaTime);

            if ((worldPoint - transform.position).magnitude < 1)
            {
                Destroy(this.gameObject);
                yield break;
            }
            yield return null;
        }
    }


}
