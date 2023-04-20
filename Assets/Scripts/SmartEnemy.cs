using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SmartEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject _shieldVisualizer;

    [SerializeField]
    public GameObject _dodgeDetector;

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

    private int _smartEnemyDirection;

    // Smart Enemy Dodge

    private int _dodging = 0;

    private bool _canDodge;
    void Start()
    {
        _isShieldActive = true;

        _player = GameObject.Find("Player_Ship").GetComponent<Player>();

    }

    
    void Update()
    {
        //if (_dodging == 0)
        

            switch (_smartEnemyDirection)
            {
                case 0:
                    EnemyMovementDownOne();
                    break;
                case 1:
                    EnemyMovementDownTwo();
                    break;
                case 2:
                    EnemyMovementLeft();
                    break;
                case 3:
                    EnemyMovementRight();
                    break;
                default:
                    break;
            }
        
        /*
        else if (_dodging == 1 || _dodging == 2)
        {
            Dodge();
            StartCoroutine(EndDodgeRoutine());
        }
        */
    }

    IEnumerator EnemyMovementLeft()
    {
        transform.Translate(Vector3.left * (_speed / 1.5f) * Time.deltaTime);

        if (transform.position.x == -4f)
        {
            _speed = 0f;
            yield return new WaitForSeconds(2.5f);
            _speed = 3f;
            transform.Translate(Vector3.down * (_speed * 3.0f) * Time.deltaTime);
        }

        //Enemy randomly spawns from the left or right.
        //Waits for seconds
        //Continues movement 

        //or

        //Use Waypoints to determine movement
        //Switch Statement to determine which waypoint is used
        //Random.Range for random waypoint appearances
    }

    IEnumerator EnemyMovementRight()
    {
        transform.Translate(Vector3.right * (_speed / 1.5f) * Time.deltaTime);

        if (transform.position.x == 4f)
        {
            _speed = 0f;
            yield return new WaitForSeconds(2.5f);
            _speed = 3f;
            transform.Translate(Vector3.down * (_speed * 3.0f) * Time.deltaTime);
        }
    }

    IEnumerator EnemyMovementDownOne()
    {
        transform.Translate(Vector3.down * (_speed / 1.5f) * Time.deltaTime);

        if (transform.position.y == 0 )
        {
            _speed = 0;
            yield return new WaitForSeconds(2.5f);
            _speed = 3.0f;
            transform.Translate(Vector3.left * (_speed * 3.0f) * Time.deltaTime);
        }
    }

    IEnumerator EnemyMovementDownTwo()
    {
        transform.Translate(Vector3.down * (_speed / 1.5f) * Time.deltaTime);

        if (transform.position.y == 0)
        {
            _speed = 0f;
            yield return new WaitForSeconds(2.5f);
            _speed = 3f;
            transform.Translate(Vector3.right * (_speed * 3.0f) * Time.deltaTime);
        }
    }

    public void SmartEnemyDirection(int direction)
    {
        _smartEnemyDirection = direction;
    }
    private void Dodge()
    {
        int randomDodgeDirection = Random.Range(0, 1);

        switch (randomDodgeDirection)
        {
            case 0:
                transform.Translate(Vector3.right * (_speed * 2) * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector3.left * (_speed * 2) * Time.deltaTime);
                break;
        }
        /*

        if (_dodging == 1)
        {
            transform.Translate(Vector3.right * (_speed * 2) * Time.deltaTime);
        }
        
       else if (_dodging == 2)
        {
            transform.Translate(Vector3.left * (_speed * 2) * Time.deltaTime);
        }
        */
        //Random.Range 0,1 to determine direction enemy will dodge.
        //Enemy Avoids Shot
        //If dodge == 1 then move left
        //Else if dodge == 2 then move right.
    }

    
    IEnumerator EndDodgeRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _dodging = (Random.Range(1, 2));
        yield return new WaitForSeconds(0.5f);
        _dodging = 0;
        
        //Coroutine decides how long before the enemy will dodge again?
    }
    

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
                EnemyShield();

                Debug.Log("Laser has collided");

            }

            if (other.tag == "Laser" && _isShieldActive == false)
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


    public void EnemyShield()
    {

        _shieldLives -= 1;

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
