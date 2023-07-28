using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnFactory
{
    public class SpawnerCommunicator : MonoBehaviour
    {
        private EnemySpawner spawner;

        public void InitializeData(EnemySpawner spawner)
        {
            this.spawner = spawner;
        }

        public void EnemyKilled()
        {
            if (spawner != null)
                spawner.EnemyKilledRemoveFromSpawned(gameObject);
        }
    }
}