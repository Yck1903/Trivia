using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Trivia.MainScene.UI
{
    public class PopupsManager : MonoBehaviour
    {
        public static PopupsManager Instance { get; private set; }

        [SerializeField] private List<Popup> _popupPrefabs;

        private Dictionary<PopupType, Popup> _popupsDict = new();

        public void Init()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                GeneratePopups();
            }
        }

        private void GeneratePopups()
        {
            for (var i = 0; i < _popupPrefabs.Count; i++)
            {
                var popup = Instantiate(_popupPrefabs[i], transform);
                popup.Init();
                _popupsDict[popup.PopupType] = popup;
            }
        }

        public Popup GetPopupOfType(PopupType popupType)
        {
            if (_popupsDict.TryGetValue(popupType, out Popup popup))
            {
                return popup;
            }
            else
            {
                Debug.LogError("Popup couldn't find. Check popups dict!");
                return null;
            }
        }
    }
}
