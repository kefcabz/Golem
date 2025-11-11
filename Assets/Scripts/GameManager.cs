using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Title,
    Instructions,
    Playing
}

public class GameManager : MonoBehaviour
{
    public MonoBehaviour playerControllerScript;

    public GameObject titleScreenPanel;
    public GameObject instructionsPanel;
    public GameObject gameHUDPanel;

    public int oreMaxHealth = 100;
    public int oreCurrentHealth;
    public Slider oreHealthBar;
    public GameObject oreObject;
    public static float scoreBonus=0;
    public float totalScore = 0;

    public GameTimer gameTimer;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverTimeText;

    public GameState currentState = GameState.Title;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateGameState(GameState.Title);

        oreCurrentHealth = oreMaxHealth;
        if (oreHealthBar != null)
        {
            oreHealthBar.maxValue = oreMaxHealth;
            oreHealthBar.value = oreCurrentHealth;
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        bool isPaused = (newState != GameState.Playing);

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = !isPaused;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {

        titleScreenPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        gameHUDPanel.SetActive(false);

        switch (currentState)
        {
            case GameState.Title:
                titleScreenPanel.SetActive(true);
                break;

            case GameState.Instructions:
                instructionsPanel.SetActive(true);
                break;

            case GameState.Playing:
                gameHUDPanel.SetActive(true);
                break;
        }
    }

    public void StartGame()
    {
        UpdateGameState(GameState.Playing);

        if (gameTimer != null)
            gameTimer.StartTimer();
    }

    public void ShowInstructions()
    {
        UpdateGameState(GameState.Instructions);
    }

    public void BackToTitle()
    {
        UpdateGameState(GameState.Title);
    }

    public void DamageOre(int amount)
    {
        oreCurrentHealth -= amount;
        oreCurrentHealth = Mathf.Clamp(oreCurrentHealth, 0, oreMaxHealth);

        if (oreHealthBar != null)
            oreHealthBar.value = oreCurrentHealth;

        if (oreCurrentHealth <= 0)
            DestroyOre();
    }

    private void DestroyOre()
    {
        Debug.Log("Ore destroyed!");
        if (oreObject != null)
            Destroy(oreObject);

        if (gameTimer != null)
            gameTimer.StopTimer();

        ShowGameOver();
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverTimeText != null && gameTimer != null)
            totalScore = gameTimer.GetElapsedTime() + scoreBonus;
            gameOverTimeText.text = "Time Protected: " + gameTimer.GetElapsedTime().ToString("F2") + "s\nDistance Bonus: " + scoreBonus.ToString("F2") + "\nTotal Score: " + totalScore.ToString("F2") + "pts";

        // Pause the game
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
