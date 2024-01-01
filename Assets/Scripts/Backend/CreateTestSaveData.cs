using System.Collections.Generic;
using Backend;
using Sirenix.OdinInspector;
using UnityEngine;


public class CreateTestSaveData : MonoBehaviour
{

//     [SerializeField] SaveManager saveManager;
//
//     
//     [SerializeField] List<EnemySettings> enemySettings;
//     
//     
//     [Button(ButtonSizes.Large, ButtonStyle.Box)]
//     void UseTestSaveData()
//     {
//         saveManager.SaveData = testSaveData;
//     }
//
//     [Button(ButtonSizes.Large, ButtonStyle.Box), GUIColor(0.8f, 0.8f, 1)]
//     void CreateSaveData(int gamesCount)
//     {
//         testSaveData.StatsForWaveGames = new List<StatsSummary>();
//         testSaveData.StatsForTimeTrialGames = new List<StatsSummary>();
//
//         for (int i = 0; i < gamesCount; i++)
//         {
//             StatsSummary waveGameStats = new StatsSummary();
//             waveGameStats.index = i;
//
//             foreach (var enemySetting in enemySettings)
//             {
//                 AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
//
//                 accuracyPerEnemy.GUID = enemySetting.GUID;
//                 accuracyPerEnemy.Accuracy = UnityEngine.Random.Range(0f, 1f);
//                 
//                 waveGameStats.AccuracyPerEnemy.Add(accuracyPerEnemy);
//             }
//
//             testSaveData.StatsForWaveGames.Add(waveGameStats);
//         }
//             
//         for (int i = 0; i < gamesCount; i++)
//         {
//             StatsSummary timeTrialGameStats = new StatsSummary();
//             timeTrialGameStats.index = i;
//
//             foreach (var enemySetting in enemySettings)
//             {
//                 AccuracyPerEnemy accuracyPerEnemy = new AccuracyPerEnemy();
//
//                 accuracyPerEnemy.GUID = enemySetting.GUID;
//                 accuracyPerEnemy.Accuracy = UnityEngine.Random.Range(0f, 1f);
//                 
//                 timeTrialGameStats.AccuracyPerEnemy.Add(accuracyPerEnemy);
//             }
//
//             testSaveData.StatsForTimeTrialGames.Add(timeTrialGameStats);
//         }
//
// #if UNITY_EDITOR
//         UnityEditor.EditorUtility.SetDirty(testSaveData);
//             
// #endif
//     }
}