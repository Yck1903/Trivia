using Enums;
using Trivia.MainScene.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia.MainScene.Buttons
{
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
}
