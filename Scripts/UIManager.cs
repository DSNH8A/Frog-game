using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lives = new GameObject[3];
    [SerializeField]
    private GameObject player;
    private Transform playerTransform; 
    private TMP_Text scoreText;
    private TMP_Text highScoreText;
    private Text gameOverScoreText;
    private Image gameOverImage;
    private int score;
    private int highScore;
    private int scoreOnText;
    private event Action textEvent;

    private void Awake()
    {
        playerTransform = player.GetComponent<Transform>();
        scoreText = transform.GetChild(3).GetComponent<TMP_Text>();
        highScoreText = transform.GetChild(4).GetComponent<TMP_Text>();
        gameOverImage = transform.GetChild(5).GetComponent<Image>();
        gameOverScoreText = gameOverImage.transform.GetChild(1).GetComponent<Text>();
    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        score = 0;
        scoreText.SetText("Score: " + score);
        highScoreText.SetText("HighScore: " + highScore);
        textEvent += ChangeScore;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            score = (int)(playerTransform.position.y) + 3;
        }

        if (score > scoreOnText)
        {
            scoreOnText = score;    
            textEvent?.Invoke();
        }

        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

   public void Damage(int livesIndex)
   {
        lives[livesIndex].SetActive(false);
   }

    public void GameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        gameOverScoreText.text = "Score: " + score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu"); 
    }

    private void ChangeScore()
    {
        highScoreText.SetText("HighScore: " + highScore);
        scoreText.SetText("Score: " + score);
    }
}