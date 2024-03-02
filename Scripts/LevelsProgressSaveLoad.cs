using UnityEngine;

public class LevelsProgressSaveLoad : MonoBehaviour
{
    [SerializeField] private int firstLevelIndex;
    [SerializeField] private string saveKey;

    private void Start()
    {
        GameLoader.Instance.OnLevelWithIndexPassed += SaveLevelWithIndexProgress;
    }

    private void SaveLevelWithIndexProgress(int passedLevelIndex)
    {
        int nextLevelIndex = passedLevelIndex + 1;
        PlayerPrefs.SetInt(saveKey, nextLevelIndex);
        Debug.Log("Saved Level " + nextLevelIndex);
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(saveKey, firstLevelIndex);
    }
}
