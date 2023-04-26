using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            SmartEnemy smartEnemy = GetComponentInParent<SmartEnemy>();
            smartEnemy.DodgeMovement();
        }
    }


}
