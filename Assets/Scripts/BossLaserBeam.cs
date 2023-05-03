using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBeam : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
    }
}
