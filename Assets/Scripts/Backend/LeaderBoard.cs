using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.Services.Leaderboards.Exceptions;

public class LeaderBoard : MonoBehaviour
{
    [ShowInInspector, ReadOnly] bool isLoaded;

    [SerializeField] List<SimplifiedLeaderBoardEntry> waveEntries;

    [SerializeField] List<SimplifiedLeaderBoardEntry> timeTrialEntries;


    [SerializeField] LeaderBoardEntriesList waveEntriesList;
    [SerializeField] LeaderBoardEntriesList timeTrialEntriesList;

    [SerializeField] GameObject noLeaderBoardPlaceHolder;

    [System.Serializable]
    public class SimplifiedLeaderBoardEntry
    {
        public bool IsPlayer;
        public string PlayerName;
        public int Rank;
        public double Score;
    }


    // retrieve the list and update the UI. disable the place holder, enable the real lists

    // just do that at the start of each game. When the player achieves a new highscore, add the new rank
    // just repeat the process. Add score, get rank. Show the adjacent ones.

    void OnEnable()
    {
        AuthenticationFacade.OnAuthenticated += AddRankAndShowLeaderboards;
        StatsTracker.OnNewHighScore += AddRankAndShowLeaderboards;
    }


    void OnDisable()
    {
        AuthenticationFacade.OnAuthenticated -= AddRankAndShowLeaderboards;
        StatsTracker.OnNewHighScore -= AddRankAndShowLeaderboards;
    }


    async void AddRankAndShowLeaderboards()
    {
        int wavesRank = await AddScoreGetRank(wavesLeaderBoardId, SaveManager.Instance.saveData.HighScoreWaves);
        if (wavesRank != -1)
        {
            waveEntries = await GetPlayerRange(wavesLeaderBoardId);
           // noLeaderBoardPlaceHolder.SetActive(false);
            waveEntriesList.ShowEntries(waveEntries);
        }

        int timeTrialRank = await AddScoreGetRank(timeTrialLeaderBoardId, SaveManager.Instance.saveData.HighScoreTimeTrial);
        if (timeTrialRank != -1)
        {
            timeTrialEntries = await GetPlayerRange(timeTrialLeaderBoardId);
           // noLeaderBoardPlaceHolder.SetActive(false);
            timeTrialEntriesList.ShowEntries(timeTrialEntries);
        }
    }

    [Button]
    public async Task<int> AddScoreGetRank(string leaderBoardID, float score)
    {
        try
        {
            var options = new AddPlayerScoreOptions();

            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderBoardID, score, new AddPlayerScoreOptions());
//            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
            return scoreResponse.Rank;
        }
        catch (LeaderboardsException e)
        {
            Debug.Log(e.Reason);
            return -1;
        }
    }


    // void ShowEntries()
    // {
    //     string playerID = AuthenticationService.Instance.PlayerId;
    //
    //     int indexOfPlayer = fakeAndRealEntries.FindIndex(i => i.PlayerID == playerID);
    //
    //     // aim is to show player in the middle
    //     int range = Mathf.FloorToInt(entriesUI.Count / 2f);
    //
    //     int startIndex = indexOfPlayer - range;
    //
    //     if (indexOfPlayer + range > fakeAndRealEntries.Count - 1)
    //     {
    //         startIndex = fakeAndRealEntries.Count - entriesUI.Count;
    //     }
    //
    //     if (startIndex < 0)
    //     {
    //         startIndex = 0;
    //     }
    //
    //     for (int i = startIndex; i < startIndex + entriesUI.Count; i++)
    //     {
    //         //rank can be determined by player rank
    //         if (entriesUI.Count > i - startIndex)
    //         {
    //             LeaderBoardEntryUI entryUI = entriesUI[i - startIndex];
    //
    //             entryUI.SetInfo(fakeAndRealEntries[i], i == indexOfPlayer);
    //         }
    //         else
    //         {
    //             Debug.LogError("not enough UI elements");
    //         }
    //     }
    // }


    [SerializeField] const string wavesLeaderBoardId = "Waves";
    [SerializeField] const string timeTrialLeaderBoardId = "TimeTrial";


    public async Task<List<SimplifiedLeaderBoardEntry>> GetPlayerRange(string leaderBoardId)
    {
        // will get 11 results with player in the middle
        var rangeLimit = 5;
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(leaderBoardId,
            new GetPlayerRangeOptions { RangeLimit = rangeLimit }
        );

        List<SimplifiedLeaderBoardEntry> simplifiedEntries = new List<SimplifiedLeaderBoardEntry>();

        foreach (var entry in scoresResponse.Results)
        {
            simplifiedEntries.Add(new SimplifiedLeaderBoardEntry
            {
                IsPlayer = entry.PlayerId == AuthenticationService.Instance.PlayerId,
                // remove # and number from player name
                PlayerName = entry.PlayerName.Split('#')[0],
                Rank = entry.Rank,
                Score = entry.Score
            });
        }

        return simplifiedEntries;
    }


// set by Unity
// public LeaderboardEntry(string playerId, string playerName, int rank, double score, string tier = default, DateTime updatedTime = default)
// {
//     PlayerId = playerId;
//     PlayerName = playerName;
//     Rank = rank;
//     Score = score;
//     Tier = tier;
//     UpdatedTime = updatedTime;
// }


//     [ContextMenu("GetScores")]
//     public async Task<LeaderboardScoresPage> GetScores(LeaderBoardSettings settings)
//     {
//         GetScoresOptions options = new GetScoresOptions();
//         options.Limit = 20;
//
//         LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(settings.LeaderBoardId, options);
// //        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
//
//         return scoresResponse;
//     }

    //
    // [Button]
    // public async Task<double?> GetPlayerScore(string leaderBoardId)
    // {
    //     if (!AuthenticationFacade.Instance.IsSignedIn)
    //     {
    //         Debug.Log("services not ready yet, initializing");
    //
    //         try
    //         {
    //             await AuthenticationFacade.Instance.InitializeServices();
    //         }
    //         catch (Exception e)
    //         {
    //             Debug.Log(e);
    //             throw;
    //         }
    //     }
    //
    //     try
    //     {
    //         LeaderboardEntry scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderBoardId);
    //
    //         // debug everything in scoreResponse
    //         Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    //
    //         return scoreResponse.Score;
    //     }
    //     catch (LeaderboardsException e)
    //     {
    //         Debug.Log(e);
    //         return null;
    //     }
    // }
}