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
    private GameObject _starPowerup;

    [SerializeField]
    private int _enemySpawnCount;

    [SerializeField]
    private TMP_Text _endOfWaveText;

    private bool _waveEnd;

    [SerializeField]
    private UIManager _uiManager;
   


    public void StartSpawning()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //StartCoroutine(UpdateWavesCountDownRoutine());

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
            //Wave 1

            for (int i = 10; i > 0; i--)
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

                yield return new WaitForSeconds(3.0f);

                if (i == 0)
                {
                    break;
                }

            }
            
            
            Debug.Log("End of first wave");

            _uiManager.StartUpdateWavesCoroutine();
            /*
            _endOfWaveText.gameObject.SetActive(true);

            _endOfWaveText.text = "END OF WAVE";
            yield return new WaitForSeconds(3.0f);

            _endOfWaveText.text = "NEXT WAVE IN...";
            yield return new WaitForSeconds(3.0f);

            _endOfWaveText.text = "3";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.text = "2";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.text = "1";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.gameObject.SetActive(false);
            
            _waveEnd = true;

            if (_waveEnd == true)
            {
                _uiManager.StartUpdateWavesCoroutine();
            }

            _waveEnd = false;
            


            // StartCoroutine(UpdateWavesCountDownRoutine());
            //_waveEnd = false;


            //_waveEnd = false;
            */

            yield return new WaitForSeconds(9.0f);

            //Wave 2

            for (int i = 20; i > 0; i--)
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

                yield return new WaitForSeconds(2.0f);

                if (i == 0)
                {
                    break;
                }

            }
            Debug.Log("End of second wave");

            _uiManager.StartUpdateWavesCoroutine();
            //StartUpdateWavesCoroutine();

            yield return new WaitForSeconds(9.0f);

            //Wave 3

            for (int i = 30; i > 0; i--)
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

                yield return new WaitForSeconds(1.0f);

                if (i == 0)
                {
                    break;
                }

            }
            Debug.Log("End of thrid wave");

            _uiManager.StartUpdateWavesCoroutine();
            //StartUpdateWavesCoroutine();

            yield return new WaitForSeconds(9.0f);

            //Wave 4

            for (int i = 35; i > 0; i--)
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

                yield return new WaitForSeconds(0.75f);

                if (i == 0)
                {

                    break;
                }

            }
            Debug.Log("End of fourth wave");

            //StartUpdateWavesCoroutine();

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

            Instantiate(_starPowerup, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(30f, 60f));

        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    
    /*
    IEnumerator UpdateWavesCountDownRoutine()
    {
        Debug.Log("WavesCoroutine has begun");

        while (_waveEnd == true)
        {
            _endOfWaveText.gameObject.SetActive(true);

            _endOfWaveText.text = "END OF WAVE";
            yield return new WaitForSeconds(3.0f);

            _endOfWaveText.text = "NEXT WAVE IN...";
            yield return new WaitForSeconds(3.0f);

            _endOfWaveText.text = "3";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.text = "2";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.text = "1";
            yield return new WaitForSeconds(1.0f);

            _endOfWaveText.gameObject.SetActive(false);
        
        }

        _waveEnd = false;


        

        
    }*/
}
