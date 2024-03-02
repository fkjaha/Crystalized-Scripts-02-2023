using UnityEngine;
using UnityEngine.Events;

public class AppEvents : MonoBehaviour
{
    public static AppEvents Instance;

    public event UnityAction OnGamePaused;
    public event UnityAction OnGameClosed;

    private void Awake()
    {
        Instance = this;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        OnGamePaused?.Invoke();
    }

    private void OnApplicationQuit()
    {
        OnGameClosed?.Invoke();
    }
}
