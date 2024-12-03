using UnityEngine;

namespace Trivia.MainScene.UI
{
    public abstract class Popup : MonoBehaviour
    {
        public abstract PopupType PopupType { get; }

        public abstract void Init();
    }
}