using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderboardDataLoader 
{
    private const string _leaderboardUrl = "https://magegamessite.web.app/case1/leaderboard_page_{0}.json";
    private readonly HttpClient _httpClient;

    public LeaderboardDataLoader()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<LeaderboardData> FetchLeaderboardAsync(int page)
    {
        try
        {
            string url = string.Format(_leaderboardUrl, page);
            var response = await _httpClient.GetStringAsync(url);

            var leaderboardData = JsonConvert.DeserializeObject<LeaderboardData>(response);
            return leaderboardData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error fetching leaderboard data: {ex.Message}");
            return null;
        }
    }
}
