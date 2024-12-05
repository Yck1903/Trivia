using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Trivia.GameScene.Buttons
{
    public class GoToMainSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _goToMainSceneButton;

        private void OnEnable()
        {
            _goToMainSceneButton.onClick.AddListener(GoToMainScene);
        }

        private void OnDisable()
        {
            _goToMainSceneButton.onClick.RemoveAllListeners();
        }

        private void GoToMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
