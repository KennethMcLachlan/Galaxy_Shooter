using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has Penetrated PlayerDetector");
            SmartEnemy smartEnemy = transform.parent.GetComponent<SmartEnemy>();
            smartEnemy.ReverseLaser();
        }
    }
}
