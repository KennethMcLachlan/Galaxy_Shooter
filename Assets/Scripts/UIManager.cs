using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Text

    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TMP_Text _restartText;

    [SerializeField]
    private GameManager _gameManager;
    
    [SerializeField]
    private TMP_Text _ammoCountNumber;

    [SerializeField]
    private TMP_Text _thrusterText;

    [SerializeField]
    private TMP_Text _endOfWaveText;


    //Sprites and Images

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Sprite _thrusterSprite;

    [SerializeField]
    public SpawnManager _spawnManager;

    private bool _waveIsOver;


    
    void Start()
    {
        _thrusterText.text = 100 + "%".ToString();
        _thrusterText.material.color = Color.white;

        _ammoCountNumber.text = 15.ToString();

        _scoreText.text = "Score:" + 0;

        _gameOverText.gameObject.SetActive(false);
        
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateAmmoCount(int playerAmmoCount)
    {
        _ammoCountNumber.text = playerAmmoCount.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateThrusterPercentage(float percentage)
    {
        _thrusterText.text = percentage.ToString("F0") + "%";
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();

        _gameOverText.gameObject.SetActive(true);

        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds (0.5F);
            
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5F);
        }
    }
    
    public void StartUpdateWavesCoroutine()
    {
        Debug.Log("Update Waves Coroutine has Begun");

        //_endOfWaveText.gameObject.SetActive(true);
        StartCoroutine(UpdateWavesCountDownRoutine());
        //_endOfWaveText.gameObject.SetActive(false);
        //return;
        
    }

    IEnumerator UpdateWavesCountDownRoutine()
    {

        _endOfWaveText.gameObject.SetActive(true);

        Debug.Log("End of Wave");

        _endOfWaveText.text = "END OF WAVE";
        yield return new WaitForSeconds(3.0f);

        Debug.Log("Next wave in...");

        _endOfWaveText.text = "NEXT WAVE IN...";
        yield return new WaitForSeconds(3.0f);

        Debug.Log("3");

        _endOfWaveText.text = "3";
        yield return new WaitForSeconds(1.0f);

        Debug.Log("2");

        _endOfWaveText.text = "2";
        yield return new WaitForSeconds(1.0f);

        Debug.Log("1");

        _endOfWaveText.text = "1";
        yield return new WaitForSeconds(1.0f);

        Debug.Log("Start!");

        //_endOfWaveText.gameObject.SetActive(false);


    }
}
