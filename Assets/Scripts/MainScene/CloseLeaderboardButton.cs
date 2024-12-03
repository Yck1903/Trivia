using Trivia.MainScene.UI;
using UnityEngine;
using UnityEngine.UI;

public class CloseLeaderboardButton : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseLeaderboardPopup);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }
    
    private void CloseLeaderboardPopup()
    {
        var leaderboardPopup = PopupsManager.Instance.GetPopupOfType(PopupType.Leaderboard);

        if (leaderboardPopup != null)
        {
            leaderboardPopup.gameObject.SetActive(false);
        }
    }
}
