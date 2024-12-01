using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Init(PlayerLeaderboardData playerLeaderboardData)
    {
        _rankText.text = playerLeaderboardData.Rank.ToString();
        _nicknameText.text = playerLeaderboardData.Nickname;
        _scoreText.text = playerLeaderboardData.Score.ToString();
    }

    public void EditUi(PlayerLeaderboardData playerLeaderboardData)
    {
        _rankText.text = playerLeaderboardData.Rank.ToString();
        _nicknameText.text = playerLeaderboardData.Nickname;
        _scoreText.text = playerLeaderboardData.Score.ToString();
    }
    
}
