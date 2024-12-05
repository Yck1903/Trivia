using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Trivia.MainScene.Buttons
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(GoToGameScene);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
        }

        private void GoToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
