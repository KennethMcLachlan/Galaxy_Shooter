using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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


    //Prefabs

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldPowerupPrefab;

    //Power-ups

    [SerializeField]
    private bool _isTripleShotActive;

    [SerializeField]
    private bool _isShieldPowerupActive;

    [SerializeField]
    private GameObject _shieldVisualizer;

    //Managers
    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    [SerializeField]
    private int _score;


    //Engine Visuals
    [SerializeField]
    private GameObject _fireRightEngine;
    
    [SerializeField]
    private GameObject _fireLeftEngine;

    //Powerup Audio
    [SerializeField]
    private GameObject _powerupAudio;

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
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        
    }

    void FireLaser()
    {
        
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

    public void Damage()
    {

        if (_isShieldPowerupActive == true)
        {
            _isShieldPowerupActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;

        if (_lives == 2)
        {
            _fireLeftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _fireRightEngine.SetActive(true);
        }

       
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            
            Destroy(gameObject);
        }
    }



    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
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
        _isShieldPowerupActive = true;

        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        
        _uiManager.UpdateScore(_score);
    }
    

    /*
=======
    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 5 * Time.deltaTime);
                        // new Vector3(1, 0, 0)
    }
>>>>>>> 1802f1537308957f353a3704a56201d613dec0f0
    */
}
