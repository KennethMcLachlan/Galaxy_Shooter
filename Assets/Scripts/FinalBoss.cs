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




    void Start()
    {
        _bossIsAlive = true;
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
            LaserFire();
        }
    }

    IEnumerator BossWaitRoutine()
    {
       _isMoving = false;

        yield return new WaitForSeconds(3f);

        _isMoving = true;

        _isSideToSide = true;

    }

    //IEnumerator AttackRoutine()
    //{

    //}
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
    

    IEnumerator WaitingPeriod()
    {
        yield return new WaitForSeconds(5f);
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

    //private void LaserBeam()
    //{
    //    Instantiate(_laserBeam, transform.position + new Vector3())
    //}

    private void Lives()
    {

    }

}
