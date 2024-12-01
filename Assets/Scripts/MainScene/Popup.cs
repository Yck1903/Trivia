using UnityEngine;

namespace MainScene
{
    public abstract class Popup : MonoBehaviour
    {
        public abstract PopupType PopupType { get; }

        public abstract void Init();
    }
}