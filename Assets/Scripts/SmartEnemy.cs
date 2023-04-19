using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SmartEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject _shieldVisualizer;

    private bool _isShieldActive;

    [SerializeField]
    private int _shieldLives = 2;

    [SerializeField]
    private Animator _explosionAnim;

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private AudioSource _explosionAudioSource;

    private Player _player;
    void Start()
    {
        _isShieldActive = true;

        _player = GameObject.Find("Player_Ship").GetComponent<Player>();

        EnemyShield();
    }

    
    void Update()
    {
        
    }

    private void CalculateMovement()
    {
        //Enemy randomly spawns from the left or right.
        //Waits for seconds
        //Continues movement 

        //or

        //Use Waypoints to determine movement
        //Switch Statement to determine which waypoint is used
        //Random.Range for random waypoint appearances
    }

    private void Dodge()
    {
        //Random.Range 0,1 to determine direction enemy will dodge.
        //Enemy Avoids Shot
        //If dodge == 1 then move left
        //Else if dodge == 2 then move right.
    }

    /*
    IEnumerator EndDodgeRoutine()
    {
        //Coroutine decides how long before the enemy will dodge again?
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        //IF tag is laser, then laser deplets shields
        //Else if Shield ==0 and or null, && laser collides, then destroy game object
        //Instantiate explosion animation
        //Add 500 to score
        //Play.audioSource

        if (gameObject != null)
        {
            Debug.Log("Smart Enemy is not NULL");
            if (other.tag == "Laser" && _isShieldActive == true)
            {
                _shieldLives -= 1;
                Debug.Log("Laser has collided");

            }

            else if (other.tag == "Laser" && _isShieldActive == false)
            {
                _explosionAnim.SetTrigger("OnEnemyDeath");

                _speed = 0.0f;

                _explosionAudioSource.Play();

                Destroy(GetComponent<Collider2D>());

                Destroy(gameObject, 2.8f);

                _player.AddScore(500);
            }

        }

    }

    private void EnemyShield()
    {

        

        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(true);

            if (_shieldLives == 2)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            
            else if (_shieldLives == 1)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
            
        }

        else if (_shieldLives < 1)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
        }

            //_shieldVisualizer.SetActive(false);

        
        //Enemy shield has two lives
        //If Shield life is == 0 then SetActive(false)

    }

    private void EnemyMissileFire()
    {
        //private transform target
        //float _movementSpeed
    }

    /*
    IEnumerator EnemyMissileFireRoutine()
    {
        //Determines when the missile will fire?
        //Maybe CoRoutine is unnecessary?
    }
    */

    
}
