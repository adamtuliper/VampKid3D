using UnityEngine;
using System.Collections;

public class PickupCoin : MonoBehaviour
{

    //For debugging
    public bool Clicked;


    private PlayerScore _playerScore;
    void Awake()
    {
        var vampKid = GameObject.FindGameObjectWithTag(Tags.player);
        _playerScore = vampKid.GetComponent<PlayerScore>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Clicked)
        {
           Clicked = false;
           Pickup();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //var playerScore = other.gameObject.GetComponent<PlayerScore>();
            Pickup();
        }
    }

    private void Pickup()
    {
        transform.parent = Camera.main.transform;
        StartCoroutine(MoveToScore());
        GetComponent<AudioSource>().Play();
    }

    IEnumerator MoveToScore()
    {
        while (true)
        {
            //Bottom left is 0,0, top right is pixelWidth, pixelHeight or Screen.width/Height 
            //(cameras can take up a portion of the screen and not the entire screen.width)
            var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 5));

            //Let's move towards that position a little bit at a time
            transform.position = Vector3.Lerp(transform.position, worldPoint, 2 * Time.deltaTime);

            //If we're close...
            if ((worldPoint - transform.position).magnitude < 1)
            {
                _playerScore.CoinScore += 1;
                Destroy(this.gameObject);
                //we're all done
                yield break;
            }

            //Wait a frame so we're not stuck in a loop, then we'll continue.
            yield return null;
        }
    }
}
