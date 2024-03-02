using MoreMountains.Feedbacks;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private MMFeedbacks endScreenFeedbacks;
        
    private void Start()
    {
        GameLoader.Instance.OnLevelPassed += ActivateEndScreen;
    }

    private void ActivateEndScreen()
    {
        endScreenFeedbacks.PlayFeedbacks();
    }

    public void ContinueButton()
    {
        GameLoader.Instance.ResetScene();
    }
}
