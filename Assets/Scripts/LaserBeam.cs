using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
         _enemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the enemy collides with the laser beam
        //enemy gameobject is destroyed

        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            //Enemy enemy = collision.transform.GetComponent<Enemy>();
            //Destroy(enemy);
        }
    }
}
