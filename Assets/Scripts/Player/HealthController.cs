// using System;
// using UnityEngine;
// using UnityEngine.Serialization;
//
//
// public class HealthController : MonoBehaviour
// {
//     [FormerlySerializedAs("health")] public int Health;
//
//     [SerializeField] int maxHealth = 3;
//     
//     [SerializeField] WaveController waveController;
//
//     public event Action<int> OnHealthReduced = delegate { };
//
//    public bool UnlimitedHealth;
//
//     void OnEnable()
//     {
//      //   EnemyBullet.OnAnyEnemyBulletHitPlayer += ReduceHealth;
//      //   UIController.OnEnableUnlimitedHealth += EnableUnlimitedHealth;
//         
//       //  GameManager.OnStartingNewTimeTrialGame += EnableUnlimitedHealth;
//         GameManager.OnStartingNewWaveGame += ResetHealth;
//     }
//     
//
//     void OnDisable()
//     {
//      //   EnemyBullet.OnAnyEnemyBulletHitPlayer -= ReduceHealth;
//      //   UIController.OnEnableUnlimitedHealth -= EnableUnlimitedHealth;
//         
//      //   GameManager.OnStartingNewTimeTrialGame -= EnableUnlimitedHealth;
//         GameManager.OnStartingNewWaveGame += ResetHealth;
//     }
//
//     // setting in the menu, ideally this gets saved, but do that later. For now could simply set that in the health SO
//     // okay, better because many need it. 
//     
//     
//     void ResetHealth()
//     {
//         Health = maxHealth;
//     }
//
//
//     void Start()
//     {
//         OnHealthReduced?.Invoke(Health);
//     }
//
//
//     void ReduceHealth()
//     {
//         Health--;
//
//         if (Health <= 0)
//         {
//             if (UnlimitedHealth)
//             {
//                 Health = 3;
//             }
//             else
//             {
//                 Health = 0;
//                 GameManager.WaveFailed();
//             }
//         }
//
//         OnHealthReduced?.Invoke(Health);
//     }
//
//     public void EnableUnlimitedHealth()
//     {
//         UnlimitedHealth = true;
//     }
// }