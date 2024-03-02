using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public static GameLoader Instance;

    public event UnityAction<int> OnLevelWithIndexPassed;
    public event UnityAction OnLevelPassed;

    public int GetCurrentLevelIndex => _currentLevelIndex;
    
    [SerializeField] private LevelSpawner levelSpawner;
    [SerializeField] private LevelsProgressSaveLoad levelsProgressSaveLoad;

    private int _currentLevelIndex;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        _currentLevelIndex = levelsProgressSaveLoad.GetCurrentLevel();
        levelSpawner.SpawnLevel(_currentLevelIndex);
        Debug.Log("Loaded level " + _currentLevelIndex);
    }

    public void OnLevelInitialized()
    {
        Level.Instance.OnLevelCompleted += LevelPassed;
    }

    private void LevelPassed()
    {
        OnLevelWithIndexPassed?.Invoke(_currentLevelIndex);
        OnLevelPassed?.Invoke();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
