using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    //Numerical Values
    [SerializeField]
    private float _speed = 5.0f;

    private float _speedMultiplier = 2f;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _shieldLives = 3;

    [SerializeField]
    public int _ammoCount = 15;

    [Range(0, 99)]
    public int _ammoRange;

    [SerializeField]
    private int _maxLives = 3;

    //Thrusters

    [SerializeField]
    private int _maxPercentage = 100;

    [SerializeField]
    private int _minPercentage = 0;

    [SerializeField]
    private GameObject _percentageColor;

    [SerializeField]
    private bool _thrusterBoostActive;

    [SerializeField]
    private float _shiftSpeedMultiplier = 1.5f;

    //Prefabs

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldPowerupPrefab;

    [SerializeField]
    private GameObject _ammoFillPrefab;

    [SerializeField]
    private GameObject _heartPowerupPrefab;

    [SerializeField]
    private GameObject _laserBeamPrefab;

    [SerializeField]
    private GameObject _thrusterPrefab;

    //Power-ups

    [SerializeField]
    private bool _isTripleShotActive;

    [SerializeField]
    public bool _isShieldPowerupActive;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private bool _isStarPowerActive;


    //Managers
    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    [SerializeField]
    private int _score;

    [SerializeField]
    private float _percentage;

    [SerializeField]
    private GameObject _ammoCountText;


    //Engine Visuals
    [SerializeField]
    private GameObject _fireRightEngine;

    [SerializeField]
    private GameObject _fireLeftEngine;

    //Audio
    [SerializeField]
    private GameObject _powerupAudio;

    [SerializeField]
    AudioSource _noAmmoAudio;

    [SerializeField]
    AudioSource _laserBeamAudio;

    [SerializeField]
    AudioSource _antiPowerupAudio;



    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
        }

    }


    void Update()
    {

        CalculateMovement();


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _isStarPowerActive == false)
        {
            if (_ammoCount == 0)
            {
                _noAmmoAudio.Play();
                return;
            }
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x >= 10.5f)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.5f)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }

        if (_percentage > 0)
        {
            _thrusterBoostActive = true;
        }

        if (_percentage <= 25)
        {
            _percentageColor.GetComponent<TMP_Text>().color = Color.red;
        }
        else if (_percentage <= 50)
        {
            _percentageColor.GetComponent<TMP_Text>().color = Color.yellow;
        }

        else if (_percentage <= 99)
        {
            _percentageColor.GetComponent<TMP_Text>().color = Color.green;
        }

        else _percentageColor.GetComponent<TMP_Text>().color = Color.white;

        if (_percentage < _minPercentage)
        {
            _percentage = _minPercentage;
            _thrusterBoostActive = false;
            
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && _thrusterBoostActive == true)
        {
            _percentage -= Time.deltaTime + 0.3f;
            _uiManager.UpdateThrusterPercentage(_percentage);
           
            transform.Translate(direction * (_speed * _shiftSpeedMultiplier) * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && _thrusterBoostActive == false)
        {
            transform.Translate((_speed / _shiftSpeedMultiplier) * Time.deltaTime * direction);
        }
        
        else
        {
            transform.Translate((_speed / _shiftSpeedMultiplier) * Time.deltaTime * direction);
            
            _percentage += Time.deltaTime + 0.05f;

            if (_percentage > _maxPercentage)
            {
                _percentage = _maxPercentage;
            }
            _uiManager.UpdateThrusterPercentage(_percentage);
             
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 2.8f), 0);
    }

    void FireLaser()
    {

        AmmoCount(-1);
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        AudioSource laserAudio = GetComponent<AudioSource>();
        laserAudio.Play();

    }

    public void AmmoCount(int bullets)
    {
        _ammoCount += bullets;

        if (_ammoCount > _ammoRange)
        {
            _ammoCount = _ammoRange;
        }

        
        if (_ammoCount <= 5)
        {
            _ammoCountText.GetComponent<TMP_Text>().color = Color.red;
        }

        else if (_ammoCount <= 30)
        {
            _ammoCountText.GetComponent<TMP_Text>().color = Color.yellow;
        }

        else if (_ammoCount < 50)
        {
            _ammoCountText.GetComponent<TMP_Text>().color = Color.green;
        }

        else _ammoCountText.GetComponent<TMP_Text>().color = Color.white;
        
        
        _uiManager.UpdateAmmoCount(_ammoCount);

    }
    public void Damage()
    {
        GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();


        //Shield Lives
        _shieldLives -= 1;

        if (_isShieldPowerupActive == true)
        {
            if (_shieldLives == 3)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.white;
            }

            else if (_shieldLives == 2)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.green;
            }

            else if (_shieldLives == 1)
            {
                _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.red;
            }

            if (_shieldLives < 1)
            {
                _isShieldPowerupActive = false;
                _shieldVisualizer.SetActive(false);
                _shieldLives = 3;
                return;
            }
        }

        //Player Lives

        if (_isShieldPowerupActive == false)
        {

            _lives -= 1;
            _shieldLives = 3;

            LivesManager();

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();

                Destroy(gameObject);
            }
        }
    }

    //Power-Ups

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {

        _speed *= _speedMultiplier;

        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        _speed /= _speedMultiplier;
    }

    public void ShieldPowerupActive()
    {
        _shieldLives = 3;

        _shieldVisualizer.GetComponent<SpriteRenderer>().material.color = Color.white;

        _isShieldPowerupActive = true;

        _shieldVisualizer.SetActive(true);
    }

    public void AmmoFillPowerupActive()
    {
        AmmoCount(+15);
    }

    public void ExtraLifePowerupActive()
    {
        _lives += 1;

        LivesManager();

        _uiManager.UpdateLives(_lives);
    }

    public void StarPowerupActive()
    {
        _isStarPowerActive = true;

        GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

        _laserBeamAudio.Play();
        _laserBeamPrefab.SetActive(true);

        StartCoroutine(StarPowerDownRoutine());
    }
    IEnumerator StarPowerDownRoutine()
    {
        yield return new WaitForSeconds(4.5f);
        _isStarPowerActive = false;

        _laserBeamPrefab.SetActive(false);
    }

    public void AntiPower()
    {
        _antiPowerupAudio.Play();
        _speed /= _speedMultiplier;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;

        StartCoroutine(AntiSpeedRoutine());
        
    }
    
    IEnumerator AntiSpeedRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed *= _speedMultiplier;
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }
    
    public void LivesManager()
    {
        if (_lives > _maxLives)
        {
            _lives = _maxLives;
        }

        else if (_lives == 3)
        {
            _fireLeftEngine.SetActive(false);
            _fireRightEngine.SetActive(false);
        }

        else if (_lives == 2)
        {
            _fireLeftEngine.SetActive(true);
            _fireRightEngine.SetActive(false);
        }

        else if (_lives == 1)
        {
            _fireLeftEngine.SetActive(true);
            _fireRightEngine.SetActive(true);
        }

    }
    
    public void AddScore(int points)
    {
        _score += points;

        _uiManager.UpdateScore(_score);
    }
}
