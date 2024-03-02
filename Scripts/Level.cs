using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public static Level Instance;

    public event UnityAction OnLevelCompleted;

    public Crystal GetCrystal => crystal;
    public CrystalBreaker GetCrystalBreaker => crystalBreaker;
    
    [SerializeField] private Crystal crystal;
    [SerializeField] private CrystalBreaker crystalBreaker;

    private void Awake()
    {
        Instance = this;
        if(GameLoader.Instance != null)
            GameLoader.Instance.OnLevelInitialized();
        crystal.OnBroken += CompleteLevel;
    }

    private void CompleteLevel()
    {
        OnLevelCompleted?.Invoke();
    }
}
