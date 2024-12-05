using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardData 
{
    public int Page { get; }
    public bool IsLast { get; }
    public List<PlayerLeaderboardData> Data { get; }

    public LeaderboardData(int page, bool is_last, List<PlayerLeaderboardData> data)
    {
        Page = page;
        IsLast = is_last;
        Data = data;
    }

    public override string ToString()
    {
        return $"Page: {Page}\n" +
               $"IsLast: {IsLast}\n" +
               $"Data: {string.Join(", ", Data)}\n";
    }
}

[Serializable]
public class PlayerLeaderboardData
{
    public int Rank { get; }
    public string Nickname { get; }
    public int Score { get; }

    public PlayerLeaderboardData(int rank, string nickname, int score)
    {
        Rank = rank;
        Nickname = nickname;
        Score = score;
    }

    public override string ToString()
    {
        return $"Rank: {Rank}\n" +
               $"Nickname: {Nickname}\n" +
               $"Score: {Score}";
    }
}