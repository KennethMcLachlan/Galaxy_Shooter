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


    //Sprites and Images

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Sprite _thrusterSprite;

    


    
    void Start()
    {
        _thrusterText.text = 100 + "%".ToString();
        _thrusterText.material.color = Color.white;

        _ammoCountNumber.text = 15.ToString();

        _scoreText.text = "Score:" + 0;

        _gameOverText.gameObject.SetActive(false);
        
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
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
}
