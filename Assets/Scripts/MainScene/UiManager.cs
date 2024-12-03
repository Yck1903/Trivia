using UnityEngine;

namespace Trivia.MainScene.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private PopupsManager _popupsManager;

        private void Awake()
        {
            _popupsManager.Init();
        }
    }
}
