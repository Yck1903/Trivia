using System.Collections.Generic;
using MainScene;
using UnityEngine;

public class PopupsManager : MonoBehaviour
{
    [SerializeField] private List<Popup> _popupPrefabs;
    
    private List<Popup> _popups = new();

    public void Init()
    {
        GeneratePopups();
    }

    public void GeneratePopups()
    {
        for (var i = 0; i < _popupPrefabs.Count; i++)
        {
            var popup = Instantiate(_popupPrefabs[i], transform);
            popup.Init();
            _popups.Add(popup);
        }
    }
}
