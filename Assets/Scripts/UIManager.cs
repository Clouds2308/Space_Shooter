using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Sprite[] LivesSprites;
    [SerializeField] private Image livesImage;
    [SerializeField] private Text GameOverText;
    [SerializeField] private Text RestartText;
    private GameManager gamemanager;

    void Start()
    {
        scoreText.text = "Score: " + 0;
        GameOverText.gameObject.SetActive(false);
        //gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (gamemanager == null)
        {
            Debug.LogError("GameManager is Null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = LivesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        GameOverText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(true);
        gamemanager.GameOver();
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.1f);
            GameOverText.text = "";
            yield return new WaitForSeconds(.1f);
        }
    }
}
