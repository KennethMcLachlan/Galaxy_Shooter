using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SmartEnemy : MonoBehaviour
{
    [SerializeField]
    public GameObject _shieldVisualizer;

    [SerializeField]
    public GameObject _dodgeDetector;

    [SerializeField]
    public GameObject _playerDetector;

    private bool _isShieldActive;

    [SerializeField]
    private int _shieldLives = 2;

    private float _distance;

    [SerializeField]
    private float _ramRange = 3.5f;

    [SerializeField]
    private float _ramSpeed = 1.75f;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private float _speed = 1f;

    private Player _player;

    private int _smartEnemyDirection;

    private int _dodgeState = 2;

    [SerializeField]
    private float _dodgeSpeed = 5f;

    [SerializeField]
    public GameObject _homingBombPrefab;

    [SerializeField]
    private float _fireRate = 3.0f;

    private float _canFire = -1f;

    [SerializeField]
    private GameObject _reverseLaser;

    [SerializeField]
    private bool _playerDetectorPenetrated;

    private int _randomReverseShot;

    void Start()
    {
        _isShieldActive = true;

        _player = GameObject.Find("Player_Ship").GetComponent<Player>();

    }
    
    void Update()
    {
        EnemyMovement();

        SmartEnemyRam();

        EnemyBombFire();
    }

    public void EnemyMovement()
    {

        switch (_dodgeState)
        {
            case 0: // Dodge Right
                transform.Translate(Vector3.right * (_dodgeSpeed) * Time.deltaTime);
                break;
            case 1: // Dodge Left
                transform.Translate(Vector3.left * (_dodgeSpeed) * Time.deltaTime);
                break;
            case 2: //Not Dodging
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
                break;
            default:
                break;

        }
    }
    private void EnemyMovementLeft()
    {
        transform.Translate(Vector3.left * (_speed) * Time.deltaTime);

        if (transform.position.x < -11f)
        {
            
            Destroy(gameObject);
        }

    }

    private void EnemyMovementRight()
    {
        transform.Translate(Vector3.right * (_speed) * Time.deltaTime);

        if (transform.position.x > 11f)
        {
            Destroy(gameObject);
        }

    }

    private void EnemyMovementDownOne()
    {
        transform.Translate(Vector3.down * (_speed) * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }

    private void EnemyMovementDownTwo()
    {
        transform.Translate(Vector3.down * (_speed) * Time.deltaTime);
        
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

    }

    public void SmartEnemyDirection(int direction)
    {
        _smartEnemyDirection = direction;
    }

    public void EnemyBombFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2.0f, 5.5f);
            _canFire = Time.time + _fireRate;

            Instantiate(_homingBombPrefab, transform.position + new Vector3(0.079f, -1.6f, 0), Quaternion.identity);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject != null)
        {
            
            if (other.tag == "Laser" && _isShieldActive == true && _shieldLives > 0)
            {

                EnemyShield();

                Destroy(other.gameObject);

                return;

            }

            if (other.tag == "Laser" && _shieldLives <= 0 && _isShieldActive == false)
            {

                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _speed = 0.0f;

                gameObject.GetComponent<AudioSource>().Play();

                Destroy(other.gameObject);

                GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

                Destroy(GetComponent<Collider2D>());

                Destroy(gameObject, 0.2f);

                _player.AddScore(500);

            }

            if (other.tag == "Laser_Beam")
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _speed = 0.0f;

                gameObject.GetComponent<AudioSource>().Play();

                GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

                Destroy(GetComponent<Collider2D>());

                Destroy(gameObject, 0.2f);

                _player.AddScore(500);
            }

            if (other.tag == "Player")
            {

                Player player = other.transform.GetComponent<Player>();

                if (player != null)
                {
                    player.Damage();
                }

                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

                _speed = 0.0f;

                gameObject.GetComponent<AudioSource>().Play();

                GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

                Destroy(GetComponent<Collider2D>());

                Destroy(gameObject, 0.2f);

            }

        }

    }


    public void EnemyShield()
    {

        _shieldLives -= 1;

        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(true);

            if (_shieldLives >= 2)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            
            else if (_shieldLives == 1)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.red;
            }

            else if (_shieldLives == 0)
            {
                _shieldVisualizer.SetActive(false);
                _isShieldActive = false;
            }
        }

    }
    
    public void SmartEnemyRam()
    {
        
        if (_player != null)
        {
            _distance = Vector3.Distance(_player.transform.position, transform.position);
        }

        if (_distance <= _ramRange && _player != null)
        {
            Vector3 direction = transform.position - _player.transform.position;
            direction = direction.normalized;
            transform.position -= direction * Time.deltaTime * _ramSpeed;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;


        }
        else gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;

    }

    public void DodgeMovement()
    {
        _dodgeState = Random.Range(0, 3);
        StartCoroutine(DodgeCoolDownRoutine());
    }

    IEnumerator DodgeCoolDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        _dodgeState = 2;
    }

    public void ReverseLaser()
    {
        _randomReverseShot = Random.Range(0, 2);

        ReverseLaser reverseLaser = GetComponent<ReverseLaser>();

        if (gameObject != null)
        {
            switch (_randomReverseShot)
            {
                case 0:
                    Instantiate(_reverseLaser, transform.position + new Vector3(1.156f, -0.1f, 0), Quaternion.identity);
                    reverseLaser.LaserMovement();
                    break;
                case 1:
                    //Left Blank to not fire
                    break;
                default:
                    break;

            }
        }
    }
    
}
