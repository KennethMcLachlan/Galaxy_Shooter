using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;

    
    [SerializeField]
    private float _fireRate = 3.0f;

    private float _canFire = -1f;

    [SerializeField]
    private GameObject _enemyLaserPrefab;


    [SerializeField]
    private bool _shieldActive;

    private int _enemyDirection;

    private float _distance;

    [SerializeField]
    private float _ramSpeed = 2.0f;

    [SerializeField]
    private float _ramRange = 3.5f;

    private bool _enemyIsDead;

    
    


    void Start()
    {
        _player = GameObject.Find("Player_Ship").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
       
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }
        
    }

    void Update()
    {
        switch (_enemyDirection)
        {
            case 0:
                CalculateMovement();
                break;
            case 1:
                CalculateMovementLeft();
                break;
            case 2:
                CalculateMovementRight();
                break;
            default:
                break;
        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;

            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();

            }

        }

        if (_player != null)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);

        }
        

        if (_distance <= _ramRange)
        {
            Vector3 direction = transform.position - _player.transform.position;
            direction = direction.normalized;
            transform.position -= direction * Time.deltaTime * _ramSpeed;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;

            
        }
        else gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;

        
    }

    public void AssignDirection(int direction)
    {
        _enemyDirection = direction;
    }

    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    
    public void CalculateMovementLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < -11f)
        {
            float randomY = Random.Range(-1.0f, 5.5f);
            transform.position = new Vector3(11f, randomY, 0);
        }
    }

    public void CalculateMovementRight()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (transform.position.x > 11f)
        {
            float randomY = Random.Range(-1.0f, 5.5f);
            transform.position = new Vector3(-11f, randomY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Player")
        { 
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                
                player.Damage();
            }

            _enemyIsDead = true;

            if (_enemyIsDead == true)
            {
                _ramRange = 0;
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                _canFire = 0;
            }

            _anim.SetTrigger("OnEnemyDeath");
            
            _speed = 0.0f;

            _audioSource.Play();
            
            Destroy(GetComponent<Collider2D>());

            Destroy(gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(100);
            }

            _enemyIsDead = true;

            if (_enemyIsDead == true)
            {
                _ramRange = 0;
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                _canFire = 0;
            }

            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0.0f;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(gameObject, 2.8f);
            
        }

        if (other.tag == "Laser_Beam")
        {
            if (_player != null)
            {
                _player.AddScore(100);
            }

            _enemyIsDead = true;

            if (_enemyIsDead == true)
            {
                _ramRange = 0;
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                _canFire = 0;
            }

            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0.0f;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(gameObject, 2.8f);
        }
    }

}
