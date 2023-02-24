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


    //Sprites and Images

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    
    void Start()
    {
        _scoreText.text = "Score:" + 0;

        _gameOverText.gameObject.SetActive(false);
        
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
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
