using Enums;
using Trivia.MainScene.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia.MainScene.Buttons
{
    public class OpenLeaderboardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenLeaderboardPopup);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OpenLeaderboardPopup()
        {
            var leaderboardPopup = PopupsManager.Instance.GetPopupOfType(PopupType.Leaderboard);

            if (leaderboardPopup != null)
            {
                leaderboardPopup.gameObject.SetActive(true);
            }
        }
    }
}

