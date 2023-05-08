using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _battleSpeed = 2;

    [Range(0, 1)]
    private int _movement;

    private bool _isMoving = true;

    private bool _isSideToSide = false;

    [SerializeField]
    private GameObject _missilePrefab;

    [SerializeField]
    private GameObject _bossLaser;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 20;

    [SerializeField]
    private GameObject _bossLaserBeam;

    [SerializeField]
    AudioSource _laserBeamAudio;

    [SerializeField]
    AudioSource _collisionSFX;

    private bool _isBossActive;

    private bool _bossIsAlive;

    [SerializeField]
    private GameObject _explosionPrefab;

    private Vector3 _scaleChange;

    [SerializeField]
    private float _laserFireDelay = 0.1f;

    private float _timeCounter;

    private WaitForSeconds _waitOneSecond = new WaitForSeconds(1);

    private WaitForSeconds _waitPointOneSeconds = new WaitForSeconds(0.1f);

    private WaitForSeconds _waitPointTwoSeconds = new WaitForSeconds(0.2f);

    [SerializeField]
    private float _battleSpeedIncrease = 1.5f;

    [SerializeField]
    private GameObject _visualDamageLeft;

    [SerializeField]
    private GameObject _visualDamageRight;

    [SerializeField]
    private GameObject _visualDamageCenter;

    private Player _player;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;


    void Start()
    {
        _bossIsAlive = true;

        _player = GameObject.Find("Player_Ship").GetComponent<Player>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
        if (transform.position.y >= 2)
        {
            IntroMovement();

        }
        else if (_isMoving == true && _isSideToSide == false)
        {
            StartCoroutine(BossWaitRoutine());
        }
        else if (_isMoving == true && _isSideToSide == true)
        {
            BattleMovement();

            if (_isBossActive == false)
            {
                StartCoroutine(BossBattleRoutine());
            }
        }
    }

    IEnumerator BossBattleRoutine()
    {
        _isBossActive = true;

        while (_bossIsAlive == true)
        {
            _timeCounter = 0;

            while (_timeCounter < 5f)
            {
                LaserFire();
                yield return new WaitForSeconds(_laserFireDelay);

                _timeCounter += _laserFireDelay;
            }

            yield return new WaitForSeconds(3f);

            MissileFire();

            yield return new WaitForSeconds(3f);

            MissileFire();

            yield return new WaitForSeconds(5f);

            LaserBeam();

            yield return new WaitForSeconds(7f);
        }
    }

    IEnumerator BossWaitRoutine()
    {
       _isMoving = false;

        yield return new WaitForSeconds(3f);

        _isMoving = true;

        _isSideToSide = true;
    }

    public void IntroMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public void BattleMovement()
    {
        if (transform.position.x >= 3)
        {
            _movement = 0;
        }
        else if (transform.position.x <= -3)
        {
            _movement = 1;
        }

        switch (_movement)
        {
            case 0:
                LeftMovement();
                break;
            case 1:
                RightMovement();
                break;
            default:
                break;
        }
    }

    
    private void LeftMovement()
    {
        transform.Translate(Vector3.left * _battleSpeed * Time.deltaTime);
    }

    private void RightMovement()
    {
        transform.Translate(Vector3.right * _battleSpeed * Time.deltaTime);
    }

    private void MissileFire()
    {
        Instantiate(_missilePrefab, transform.position + new Vector3(2.26f, -1.32f, 0), Quaternion.identity);
        Instantiate(_missilePrefab, transform.position + new Vector3(-2.45f, -1.32f, 0), Quaternion.identity);
    }

    private void LaserFire()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_bossLaser, transform.position + new Vector3(-1.852f, 0.6f, 0), Quaternion.identity);
        }
    }


    public void LivesManager()
    {
        _lives -= 1;

        if (_lives <= 15)
        {
            _visualDamageRight.SetActive(true);
        }

        if ( _lives <= 10)
        {
            _visualDamageLeft.SetActive(true);
            _battleSpeed = _battleSpeed + _battleSpeedIncrease;
        }

        if (_lives <= 5)
        {
            _visualDamageCenter.SetActive(true);
            _battleSpeed = _battleSpeed + _battleSpeedIncrease;
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }

        if (_lives <= 0)
        {
            _lives = 0;
            _battleSpeed = 0f;
            _bossIsAlive = false;

            StopAllCoroutines();

            CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
            circleCollider.enabled = false;

            _player.AddScore(2500);
            
            StartCoroutine(ExplosionRoutine());
        }
    }

    private void LaserBeam()
    {
        GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

        _laserBeamAudio.Play();
        _bossLaserBeam.SetActive(true);

        StartCoroutine(LaserBeamCooldownRoutine());
    }

    IEnumerator LaserBeamCooldownRoutine()
    {
        yield return new WaitForSeconds(5.3f);
        _bossLaserBeam.SetActive(false);
    }

    IEnumerator ExplosionRoutine()
    {
        Instantiate(_explosionPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return _waitOneSecond;
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-1.66f, 2.66f, 0), Quaternion.identity);
        yield return _waitOneSecond;
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.69f, 1.81f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(2.22f, 0.31f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-2.94f, 0.64f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(0.7f, 2.57f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-0.87f, -0.45f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;
        yield return _waitPointOneSeconds;
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.69f, 1.81f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(0.7f, 2.57f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-1.93f, 0.31f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(2.22f, 0.31f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return _waitPointTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(0.7f, 2.57f, 0), Quaternion.identity);
        yield return _waitPointOneSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-1.93f, 0.31f, 0), Quaternion.identity);
        yield return _waitPointOneSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(2.22f, 0.31f, 0), Quaternion.identity);
        yield return _waitPointOneSeconds;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return _waitPointOneSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.69f, 1.81f, 0), Quaternion.identity);
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;
        yield return _waitPointTwoSeconds;

        _scaleChange = new Vector3(2, 2, 0);
        _explosionPrefab.transform.localScale += _scaleChange;

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        _explosionPrefab.transform.localScale -= _scaleChange;

        BossIsDead();

        Destroy(gameObject);
    }

    public void BossIsDead()
    {
        _bossIsAlive = false;
        _spawnManager.BossHasDied();
        _uiManager.StartUpdateWavesCoroutine();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" && _isBossActive == true)
        {
            Destroy(other.gameObject);

            _collisionSFX.Play();

            LivesManager();
        }

        if (other.tag == "Player" && _isBossActive == true && _player != null)
        {
            _player.Damage();
        }
    }
}
