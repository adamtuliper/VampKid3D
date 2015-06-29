using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;							// How much health the player has left.
	public float resetAfterDeathTime = 5f;				// How much time from the player dying to the level reseting.
	public AudioClip deathClip;							// The sound effect of the player dying.
	
	
	private Animator _animator;								// Reference to the animator component.
//	private PlayerMovement playerMovement;			// Reference to the player movement script.
	private HashIDs _hash;							// Reference to the HashIDs.
//	private SceneFadeInOut sceneFadeInOut;			// Reference to the SceneFadeInOut script.
	private LastPlayerSighting _lastPlayerSighting;	// Reference to the LastPlayerSighting script.
	private float _timer;								// A timer for counting to the reset of the level once the player is dead.
	private bool _playerDead;							// A bool to show if the player is dead or not.

    public float Health { get {return _health;}}
	
	void Awake ()
	{
		// Setting up the references.
		_animator = GetComponent<Animator>();
		//playerMovement = GetComponent<PlayerMovement>();
		_hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		//sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
		_lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
	}
	
    void Update ()
	{
		// If health is less than or equal to 0...
		if(_health <= 0f)
		{
			// ... and if the player is not yet dead...
			if(!_playerDead)
				// ... call the PlayerDying function.
				PlayerDying();
			else
			{
				// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
				PlayerDead();
				LevelReset();
			}
		}
	}
	
	
	void PlayerDying ()
	{
		// The player is now dead.
		_playerDead = true;
		
		// Set the animator's dead parameter to true also.
		_animator.SetBool(_hash.deadBool, _playerDead);
		
		// Play the dying sound effect at the player's location.
		AudioSource.PlayClipAtPoint(deathClip, transform.position);
	}
	
	
	void PlayerDead ()
	{
		// If the player is in the dying state then reset the dead parameter.
		if(_animator.GetCurrentAnimatorStateInfo(0).nameHash == _hash.dyingState)
			_animator.SetBool(_hash.deadBool, false);
		
		// Disable the movement.
		_animator.SetFloat(_hash.speedFloat, 0f);
		//playerMovement.enabled = false;
		
		// Reset the player sighting to turn off the alarms.
		_lastPlayerSighting.position = _lastPlayerSighting.resetPosition;
		
		// Stop the footsteps playing.
		GetComponent<AudioSource>().Stop();
	}
	
	
	void LevelReset ()
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
	
	
	public void TakeDamage (float amount)
    {
		// Decrement the player's health by amount.
        _health -= amount;
    }
}
