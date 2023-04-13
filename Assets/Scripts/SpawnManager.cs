using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private GameObject _starPower;
   
    [SerializeField]
    private int _enemySpawnCount;

    [SerializeField]
    private TMP_Text _endOfWaveText;

    [SerializeField]
    private int _waveOneCount = 10;

    [SerializeField]
    private int _waveTwoCount = 15;

    [SerializeField]
    private int _waveThreeCount = 20;

    [SerializeField]
    private int _waveFourCount = 25;

    [SerializeField]
    private float _waveOneSpeed = 3.0f;

    [SerializeField]
    private float _waveTwoSpeed = 2.5f;

    [SerializeField]
    private float _waveThreeSpeed = 2.0f;

    [SerializeField]
    private float _waveFourSpeed = 1.5f;

    [SerializeField]
    private UIManager _uiManager;


   


    public void StartSpawning()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerupRoutine());

        StartCoroutine(SpawnStarPowerRoutine());

        StartCoroutine(SpawnAmmoFillRoutine());

        StartCoroutine(SpawnHealthRoutine());
    }
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while (_stopSpawning == false)
        {
            //Wave 1

            for (int waveCount = _waveOneCount; waveCount > 0; waveCount--)
            {
                int _randomSpawnLocation = Random.Range(0, 3);

                switch (_randomSpawnLocation)
                {
                    case 0:
                        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                        newEnemy.GetComponent<Enemy>().AssignDirection(0);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 1:
                        Vector3 spawnGoLeft = new Vector3(11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject leftEnemy = Instantiate(_enemyPrefab, spawnGoLeft, Quaternion.identity);
                        leftEnemy.GetComponent<Enemy>().AssignDirection(1);
                        leftEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 2:
                        Vector3 spawnGoRight = new Vector3(-11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject rightEnemy = Instantiate(_enemyPrefab, spawnGoRight, Quaternion.identity);
                        rightEnemy.GetComponent<Enemy>().AssignDirection(2);
                        rightEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    default:
                        break;

                }

                yield return new WaitForSeconds(_waveOneSpeed);

                if (waveCount == 0)
                {
                    break;
                }

            }
            
            Debug.Log("End of first wave");

            _uiManager.StartUpdateWavesCoroutine();
           

            yield return new WaitForSeconds(9.0f);

            //Wave 2

            for (int waveCount = _waveTwoCount; waveCount > 0; waveCount--)
            {
                int _randomSpawnLocation = Random.Range(0, 3);

                switch (_randomSpawnLocation)
                {
                    case 0:
                        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                        newEnemy.GetComponent<Enemy>().AssignDirection(0);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 1:
                        Vector3 spawnGoLeft = new Vector3(11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject leftEnemy = Instantiate(_enemyPrefab, spawnGoLeft, Quaternion.identity);
                        leftEnemy.GetComponent<Enemy>().AssignDirection(1);
                        leftEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 2:
                        Vector3 spawnGoRight = new Vector3(-11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject rightEnemy = Instantiate(_enemyPrefab, spawnGoRight, Quaternion.identity);
                        rightEnemy.GetComponent<Enemy>().AssignDirection(2);
                        rightEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    default:
                        break;

                }

                yield return new WaitForSeconds(_waveTwoSpeed);

                if (waveCount == 0)
                {
                    break;
                }

            }
            Debug.Log("End of second wave");

            _uiManager.StartUpdateWavesCoroutine();

            yield return new WaitForSeconds(9.0f);

            //Wave 3

            for (int waveCount = _waveThreeCount; waveCount > 0; waveCount--)
            {
                int _randomSpawnLocation = Random.Range(0, 3);

                switch (_randomSpawnLocation)
                {
                    case 0:
                        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                        newEnemy.GetComponent<Enemy>().AssignDirection(0);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 1:
                        Vector3 spawnGoLeft = new Vector3(11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject leftEnemy = Instantiate(_enemyPrefab, spawnGoLeft, Quaternion.identity);
                        leftEnemy.GetComponent<Enemy>().AssignDirection(1);
                        leftEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 2:
                        Vector3 spawnGoRight = new Vector3(-11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject rightEnemy = Instantiate(_enemyPrefab, spawnGoRight, Quaternion.identity);
                        rightEnemy.GetComponent<Enemy>().AssignDirection(2);
                        rightEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    default:
                        break;

                }

                yield return new WaitForSeconds(_waveThreeSpeed);

                if (waveCount == 0)
                {
                    break;
                }

            }
            Debug.Log("End of third wave");

            _uiManager.StartUpdateWavesCoroutine();

            yield return new WaitForSeconds(9.0f);

            //Wave 4

            for (int waveCount = _waveFourCount; waveCount > 0; waveCount--)
            {
                int _randomSpawnLocation = Random.Range(0, 3);

                switch (_randomSpawnLocation)
                {
                    case 0:
                        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                        newEnemy.GetComponent<Enemy>().AssignDirection(0);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 1:
                        Vector3 spawnGoLeft = new Vector3(11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject leftEnemy = Instantiate(_enemyPrefab, spawnGoLeft, Quaternion.identity);
                        leftEnemy.GetComponent<Enemy>().AssignDirection(1);
                        leftEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    case 2:
                        Vector3 spawnGoRight = new Vector3(-11f, Random.Range(-1.0f, 5.5f), 0);
                        GameObject rightEnemy = Instantiate(_enemyPrefab, spawnGoRight, Quaternion.identity);
                        rightEnemy.GetComponent<Enemy>().AssignDirection(2);
                        rightEnemy.transform.parent = _enemyContainer.transform;
                        break;
                    default:
                        break;

                }

                yield return new WaitForSeconds(_waveFourSpeed);

                if (waveCount == 0)
                {

                    break;
                }

            }
            Debug.Log("End of fourth wave");

            yield return new WaitForSeconds(9.0f);

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

            Instantiate(_starPower, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(30f, 60f));

        }
    }

    IEnumerator SpawnAmmoFillRoutine()
    {
        yield return new WaitForSeconds(10f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            Instantiate(_powerups[3], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(15f, 25f));
        }
    }

    IEnumerator SpawnHealthRoutine()
    {
        yield return new WaitForSeconds(30f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            Instantiate(_powerups[4], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(30f, 45f));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
