using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public float gameTime;
    public float timeToStartTimer;
    public AudioSource soundEffects;
    public GameObject gameOverScreen;

    private float timer;
    private bool timerStarted;
    private bool gameOver;

    void Start ()
    {
        timer = 0;
        timerStarted = false;
        gameOverScreen.SetActive(false);
        gameOver = false;
        Invoke("StartTimer", timeToStartTimer);
    }

    void Update ()
    {
        if (!gameOver)
        {
            if (timerStarted)
            {
                timer += Time.deltaTime;
                if (timer > gameTime)
                {
                    gameOver = true;
                    // truck leaves
                    // game over
                }
            }
        }
        else
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void StartTimer()
    {
        timerStarted = true;
    }
}
