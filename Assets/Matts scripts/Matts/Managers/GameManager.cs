using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;

    public static GameManager instance;

    private GameState currentState;
    private LevelManager currentLevel;
    private int currentLevelIndex = 0;
    private bool isInputActive = true;


    // singleton pattern
    private void Awake()
    {
        SingltonInstance();
    }

    private void SingltonInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (levels.Length > 0)
        {
            ChangeState(GameState.Briefing, levels[currentLevelIndex]);
        }
    }

    public void ChangeState(GameState state, LevelManager level)
    {
        currentState = state;
        currentLevel = level;

        switch (currentState)
        {
            case GameState.Briefing:
                StartBriefing();
                break;
            case GameState.LevelStart:
                InitiateLevel();
                break;
            case GameState.LevelIn:
                RunLevel();
                break;
            case GameState.LevelEnd:
                CompleteLevel();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
        }
    }

    private void StartBriefing()
    {
        Debug.Log("Breifing Started");
        isInputActive = false;
        ChangeState(GameState.LevelStart, currentLevel);
    }
    private void InitiateLevel()
    {
        Debug.Log("Initiating Level Started");
        isInputActive = true;
        currentLevel.StartLevel();
        ChangeState(GameState.LevelIn, currentLevel);
    }
    private void RunLevel()
    {
        Debug.Log("Running Level " + currentLevel.gameObject.name);
    }
    private void CompleteLevel()
    {
        Debug.Log("Level Complete");
        ChangeState(GameState.LevelStart, levels[++currentLevelIndex]);
    }
    private void GameOver()
    {
        Debug.Log("Game over, you lose");
    }
    private void GameEnd()
    {
        Debug.Log("Game End, Credits");
    }
    public enum GameState
    {
        Briefing, // option 0
        LevelStart,// option 1
        LevelIn,// option 2
        LevelEnd,// option 3
        GameOver,// option 4
        GameEnd// option 5
    }
}
