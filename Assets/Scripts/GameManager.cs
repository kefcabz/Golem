using UnityEngine;
using System.Collections.Generic;

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
    }

    public void ShowInstructions()
    {
        UpdateGameState(GameState.Instructions);
    }

    public void BackToTitle()
    {
        UpdateGameState(GameState.Title);
    }
}
