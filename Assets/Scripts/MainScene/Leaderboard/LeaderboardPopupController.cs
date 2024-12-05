using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia.MainScene.UI
{
    public class LeaderboardPopupController : Popup
    {
        public override PopupType PopupType => PopupType.Leaderboard;
    
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private LeaderboardItem _leaderboardItemPrefab;
        [SerializeField] private Transform _loadingImage;

        [SerializeField] private Button _nextPageButton;
        [SerializeField] private Button _previousPageButton;
    
        private LeaderboardDataLoader _leaderboardDataLoader;
    
        private List<LeaderboardItem> _leaderboardItems = new();
        private Dictionary<int, LeaderboardData> _leaderboardPageData = new();
    
        private const int _openingPageNumber = 0;

        private int _currentPageNumber;

        public override void Init()
        {
            _leaderboardDataLoader = new LeaderboardDataLoader();
        }

        private async void OnEnable()
        {
            BumpEffect();
            
            AddButtonListeners();
        
            await OpenPage(_openingPageNumber);
        }

        private void OnDisable()
        {
            ResetLeaderboardPageData();

            ResetButtons();
        
            RemoveButtonListeners();
        }

        private void AddButtonListeners()
        {
            _nextPageButton.onClick.AddListener(() => OpenPage(_currentPageNumber + 1));
            _previousPageButton.onClick.AddListener(() => OpenPage(_currentPageNumber - 1));
        }

        private void RemoveButtonListeners()
        {
            _nextPageButton.onClick.RemoveAllListeners();
            _previousPageButton.onClick.RemoveAllListeners();
        }
    
        private async Task OpenPage(int pageNumber)
        {
            if (!HasPageData(pageNumber))
            {
                ShowLoadingPanel();
                await LoadPageData(pageNumber);
                HideLoadingPanel();
            }
        
            EditLeaderboardItemsForPageNumber(pageNumber);

            CheckPageButtons(pageNumber);

            _currentPageNumber = pageNumber;
        }
    
        private bool HasPageData(int pageNumber)
        {
            if (_leaderboardPageData.ContainsKey(pageNumber))
            {
                return true;
            }

            return false;
        }

        private async Task LoadPageData(int pageNumber)
        {
            var leaderboardData = await _leaderboardDataLoader.FetchLeaderboardAsync(pageNumber);

            _leaderboardPageData[leaderboardData.Page] = leaderboardData;
        }
    
        private void EditLeaderboardItemsForPageNumber(int pageNumber)
        {
            if (!_leaderboardItems.Any())
            {
                GenerateLeaderboardItems();
                return;
            }
        
            for (int i = 0; i < _leaderboardItems.Count; i++)
            {
                _leaderboardItems[i].EditUi(GetDataFromLeaderboardDict(pageNumber, i));
            }
        }
    
        private void GenerateLeaderboardItems()
        {
            foreach (var playerLeaderboardData in _leaderboardPageData[_openingPageNumber].Data)
            {
                LeaderboardItem leaderboardItem = Instantiate(_leaderboardItemPrefab, _contentTransform);
                leaderboardItem.Init(playerLeaderboardData);
                _leaderboardItems.Add(leaderboardItem);
            }
        }
    
        private PlayerLeaderboardData GetDataFromLeaderboardDict(int pageNumber, int index)
        {
            return _leaderboardPageData[pageNumber].Data[index];
        }
    
        private void CheckPageButtons(int pageNumber)
        {
            if (_leaderboardPageData[pageNumber].IsLast)
            {
                _nextPageButton.interactable = false;
                _previousPageButton.interactable = true;
                return;
            }
        
            if (pageNumber == 0)
            {
                _nextPageButton.interactable = true;
                _previousPageButton.interactable = false;
                return;
            }

            _nextPageButton.interactable = true;
            _previousPageButton.interactable = true;
        }

        private void ShowLoadingPanel()
        {
            _loadingImage.gameObject.SetActive(true);
        }

        private void HideLoadingPanel()
        {
            _loadingImage.gameObject.SetActive(false);
        }

        private void ResetButtons()
        {
            _previousPageButton.interactable = false;
            _nextPageButton.interactable = true;
        }

        private void ResetLeaderboardPageData()
        {
            _leaderboardPageData.Clear();
        }
        
        private void BumpEffect()
        {
            transform.localScale = Vector3.one * 0.85f;
            transform.DOScale(Vector3.one * 1.05f, 0.10f).OnComplete(() => { transform.DOScale(Vector3.one, .065f); });
        }
    }
}
