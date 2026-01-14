using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetList;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    private int startingLives = 3;
    private int lives;
    private int score;
    private float spawnRate = 1.0f;
    public bool isGameActive;
    public bool isPaused;
    public GameObject titleScreen;
    public GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;

        lives = startingLives;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetList.Count);
            Instantiate(targetList[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeLives(int livesChange)
    {
        lives += livesChange;
        lives = Mathf.Clamp(lives, 0, startingLives);
        livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            GameOver();
        }
    }

    public void ChangePaused()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            isPaused = false;
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
