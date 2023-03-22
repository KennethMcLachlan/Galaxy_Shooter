using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameObject _starPowerup;

    [SerializeField]
    private GameObject[] _enemyDirections;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerupRoutine());

        StartCoroutine(SpawnStarPowerRoutine());
    }
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        
        

        while (_stopSpawning == false)
        {
            
            //EnemyDirection();

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

            /*

            switch(_enemyDirectionID)
                case 0:
                   
                break;
                case 1:
                enemy.CalculateMovementLeft();
                break;
                case 2:
                enemy.CalculateMovementRight();
                break;
                default;
            */

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 6);

            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    IEnumerator SpawnStarPowerRoutine()
    {
        yield return new WaitForSeconds(60f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            Instantiate(_starPowerup, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(30f, 60f));

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    /*
    public void EnemyDirection()
    {
        Enemy enemy = GetComponent<enemy>;

        int randomSpawnLocation = randomSpawnLocation.Range(0, 2);
        Instantiate(_enemyDirections[randomSpawnLocation], 0, Quaternion.identity);

        switch (_enemyDirectionID)
        {
            case 0:
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                enemy.CalculateMovement();
                newEnemy.transform.parent = _enemyContainer.transform;
                break;
            case 1:
                Vector3 SpawnGoLeft = new Vector3(Random.Range(11f, (-5.5f, 5.5f), 0);
                GameObject leftEnemy = Instantiate(_enemyPrefab, SpawnGoLeft, Quaternion.identity);
                enemy.CalculateMovementLeft();
                leftEnemy.transform.parent = _enemyContainer.transform;
                break;
            case 2:
                Vector3 SpawnGoRight = new Vector3(Random.Range(-11f, (-5.5f, 5.5f), 0);
                GameObject rightEnemy = Instantiate(_enemyPrefab, SpawnGoRight, Quaternion.identity);
                enemy.CalculateMovementRight();
                rightEnemy.transform.parent = _enemyContainer.transform;
                break;
            default:
        }   
    }

    */

}
