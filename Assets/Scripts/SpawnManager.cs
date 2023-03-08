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
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

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

            int randomPowerUp = Random.Range(0, 5);

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



}
