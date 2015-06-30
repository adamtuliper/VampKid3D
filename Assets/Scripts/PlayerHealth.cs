using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;                           // How much health the player has left.
    public float resetAfterDeathTime = 5f;              // How much time from the player dying to the level reseting.
    public AudioClip deathClip;                         // The sound effect of the player dying.


    private Animator _animator;                             // Reference to the animator component.
                                                            //	private PlayerMovement playerMovement;			// Reference to the player movement script.
    private HashIDs _hash;                          // Reference to the HashIDs.
                                                    //	private SceneFadeInOut sceneFadeInOut;			// Reference to the SceneFadeInOut script.
    private LastPlayerSighting _lastPlayerSighting; // Reference to the LastPlayerSighting script.
    private float _timer;                               // A timer for counting to the reset of the level once the player is dead.
    private bool _playerDead;							// A bool to show if the player is dead or not.

    [SerializeField]
    private GameObject DeathParticles;

    public float Health { get { return _health; } }

    void Awake()
    {
        // Setting up the references.
        _animator = GetComponent<Animator>();
        //playerMovement = GetComponent<PlayerMovement>();
        _hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        _lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        // If health is less than or equal to 0...
        if (_health <= 0f)
        {

            // Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
            PlayerDead();
            LevelReset();
        }
    }


    //void PlayerDying()
    //{
    //    // The player is now dead.
    //    _playerDead = true;

    //    // Set the animator's dead parameter to true also.
    //    _animator.SetBool(_hash.deadBool, _playerDead);

    //    // Play the dying sound effect at the player's location.
    //    AudioSource.PlayClipAtPoint(deathClip, transform.position);
    //}


    void PlayerDead()
    {
        if (_playerDead) return;
        _playerDead = true;
        //Disable the animator - actually - create particle.
        // Disable the movement.
        //_animator.SetFloat(_hash.speedFloat, 0f);
        //playerMovement.enabled = false;

       var particles = (GameObject) Instantiate(DeathParticles, transform.position, Quaternion.identity);
       Destroy(particles,7);
        // Reset the player sighting to turn off the alarms.
        _lastPlayerSighting.position = _lastPlayerSighting.resetPosition;

        // Stop the footsteps playing.
        //GetComponent<AudioSource>().Stop();

        //Stop animations (or maybe after death? good use for AnimationStateBehavior)
        _animator.enabled = false;
        //Disable physics
        GetComponent<Rigidbody>().isKinematic = true;

        //Hide vampire
        var vampire = transform.FindChild("vamp_complete");
        if (!vampire)
        {
            Debug.LogError("Couldn't find child vampire object of VampKid character controller");
        }
        vampire.gameObject.SetActive(false);
        var cameraRig = GameObject.FindGameObjectWithTag(Tags.cameraRig);
        cameraRig.GetComponent<FreeLookCam>().PlayerDiedCameraAction();
        //Lerp camera vertical, move up a bit
        //pivot it to 90
        //y to 20
        //Disable input for free look rig
        //or maybe just lerp to look upwards to sky (moon?)

    }


    void LevelReset()
    {
        // Increment the timer.
        _timer += Time.deltaTime;

        //If the timer is greater than or equal to the time before the level resets...
        if (_timer >= resetAfterDeathTime)
        {
            // ... reset the level.
            //	sceneFadeInOut.EndScene();   
        }
    }

    public void Die()
    {
        _health = 0;
    }

    public void TakeDamage(float amount)
    {
        // Decrement the player's health by amount.
        _health -= amount;
    }
}
