using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    [SerializeField]
    private float _laserSpeed = 8.0f;
    
    private bool _isEnemyLaser = false;

    private bool _enemyLaser = false;
   
    void Start()
    {
       
    }

    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    void MoveDown()
    {
        _enemyLaser = true;

        transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

        
        if (transform.position.y <= -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            
            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(gameObject);
            }
        }

        if (other.tag == "Enemy" && _isEnemyLaser == false)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.WhenEnemyDiesSequence();

            Player player = GameObject.Find("Player_Ship").GetComponent<Player>();
            player.AddScore(100);

            Destroy(gameObject);

            //BoxCollider2D collider = other.GetComponent<BoxCollider2D>();

            //collider.enabled = false;
            //StartCoroutine(EnemyLaserBypass());
            //collider.enabled = true;
            //Debug.Log("Laser has Collided");

            //Enemy enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            //Physics.IgnoreCollision(enemy.GetComponents<Collider2D>, other.gameObject);
        }
    }
    
    IEnumerator EnemyLaserBypass()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
