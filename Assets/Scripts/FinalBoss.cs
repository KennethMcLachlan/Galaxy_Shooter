using System.Collections;
using System.Collections.Generic;
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
    private GameObject _laserBeam;

    private bool _bossIsAlive;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 15;

    [SerializeField]
    private GameObject _bossLaserBeam;

    [SerializeField]
    AudioSource _laserBeamAudio;

    private bool _isLaserFireActive;

    private bool _isBossActive;

    [SerializeField]
    private GameObject _explosionPrefab;

    private Vector3 _scaleChange;

    private float _timeCounter;

    [SerializeField]
    private float _laserFireDelay = 0.1f;

    private WaitForSeconds _waitTwoSeconds = new WaitForSeconds(2);



    void Start()
    {
        _bossIsAlive = true;

        //if (_isBossActive == true)
        //{
        //    StartCoroutine(BossBattleRoutine());
        //}
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

            //while (_isLaserFireActive == true)
            //{
            //    LaserFire();
            //}

            yield return new WaitForSeconds(3f);

            MissileFire();

            yield return new WaitForSeconds(3f);

            MissileFire();

            yield return new WaitForSeconds(5f);

            LaserBeam();

            yield return new WaitForSeconds(5f);
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

        //return;

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

        if (_lives <= 5)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red;
        }

        if (_lives <= 0)
        {
            _bossIsAlive = false;

            _isBossActive = false;

            _battleSpeed = 0f;

            StartCoroutine(ExplosionRoutine());

            Destroy(gameObject);
        }
    }

    private void LaserBeam()
    {
        GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

        _bossLaserBeam.SetActive(true);

        _laserBeamAudio.Play();
        

        StartCoroutine(LaserBeamCooldownRoutine());
    }

    IEnumerator LaserBeamCooldownRoutine()
    {
        yield return new WaitForSeconds(5.4f);
        _laserBeam.SetActive(false);
    }

    IEnumerator ExplosionRoutine()
    {
        Instantiate(_explosionPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        yield return _waitTwoSeconds;
        yield return _waitTwoSeconds;

        Instantiate(_explosionPrefab, transform.position + new Vector3(-1.66f, 2.66f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.8f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.58f, 1.36f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(-2.94f, 0.64f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.2f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(-0.87f, -0.45f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.9f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.69f, 1.81f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(0.7f, 2.57f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(-1.93f, 0.31f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(2.22f, 0.31f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);

        Instantiate(_explosionPrefab, transform.position + new Vector3(1.69f, 1.81f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);

        _scaleChange = new Vector3(2, 2, 0);
        _explosionPrefab.transform.localScale += _scaleChange;

        Instantiate(_explosionPrefab, transform.position + new Vector3(2.22f, 0.57f, 0), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" && gameObject != null)
        {
            LivesManager();
        }
    }
}
