using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void CalculateMovement()
    {
        //Enemy randomly spawns from the left or right.
        //Waits for seconds
        //Continues movement 

        //or

        //Use Waypoints to determine movement
        //Switch Statement to determine which waypoint is used
        //Random.Range for random waypoint appearances
    }

    private void Dodge()
    {
        //Random.Range 0,1 to determine direction enemy will dodge.
        //Enemy Avoids Shot
        //If dodge == 1 then move left
        //Else if dodge == 2 then move right.
    }

    /*
    IEnumerator EndDodgeRoutine()
    {
        //Coroutine decides how long before the enemy will dodge again?
    }
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        //IF tag is laser, then laser deplets shields
        //Else if Shield ==0 and or null, && laser collides, then destroy game object
        //Instantiate explosion animation
        //Add 500 to score
        //Play.audioSource
    }

    private void EnemyShield()
    {
        //Enemy shield has two lives
        //If Shield life is == 0 then SetActive(false)

    }

    private void EnemyMissileFire()
    {
        //private transform target
        //float _movementSpeed
    }

    /*
    IEnumerator EnemyMissileFireRoutine()
    {
        //Determines when the missile will fire?
        //Maybe CoRoutine is unnecessary?
    }
    */
}
