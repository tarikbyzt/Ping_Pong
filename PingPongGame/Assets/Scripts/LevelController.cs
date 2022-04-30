using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = false;
    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, currentLevelText, nextLevelText;
    public TextMeshProUGUI finishScoreText;
    public ParticleSystem finishParticle;
    public Slider levelProgressBar;
    public float maxDistance;
    public GameObject finishLine;
    public int currentLevel;
    public float score;
    
    
    void Start()
    {
        Current = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");  
    }
    public void StartLevel()
    {
        //maxDistance = finishLine.transform.position.z - PlayerController.Current.transform.position.z;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if (currentLevel == 2)
        {

            PlayerPrefs.SetInt("currentLevel", currentLevel - 2);
            currentLevel -= 2;
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Level " + (currentLevel + 1));
        }
        
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
    }

    public void FinishMenu()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        finishParticle.Play();
        finishScoreText.text = Ball.Current.score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }

    public void ChangeScore(int increment)
    {
        score += increment;

        scoreText.text = score.ToString();
    }

    public void ChangeMultiplicationScore(float ScoreX)
    {

        score *= ScoreX;
        score = (int)score;
        scoreText.text = score.ToString();


    }
}
