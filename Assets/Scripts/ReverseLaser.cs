using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseLaser : MonoBehaviour
{
    private float _laserSpeed = 8.0f;

    void Start()
    {
        
    }
    
    void Update()
    {
        LaserMovement();
    }

    public void LaserMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(gameObject);
            }
        }
    }
}
