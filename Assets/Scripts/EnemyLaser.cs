using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f;
    void Start()
    {
        
    }

    
    void Update()
    {
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
}
