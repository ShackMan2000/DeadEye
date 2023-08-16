using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinMaxSlider;
using UnityEngine.AI;
using UnityEngine.Events;
using SpawnFactory.UnityExtensions;
using SpawnFactory.Triangles;
using SpawnFactory.Pooling;
using SpawnFactory.Events;
#if UNITY_EDITOR
using UnityExtensions;
#endif

#if MORE_EFFICIENT_COROUTINES
using MEC;
#endif

namespace SpawnFactory
{
    public class EnemySpawner : SpawnerMask
    {
        #region Serialized Properties
        /// <summary>
        /// Only used when same mobs every wave
        /// </summary>
#if UNITY_EDITOR && !UNITY_2020_2_OR_NEWER
        [ReorderableList(elementsAreSubassets = true)]
#endif
        [SerializeField] private List<Mobs> mobs = new List<Mobs>();
        [SerializeField] private bool showSearchRadius = true;
        [SerializeField] private bool showRadiusOnlyWhenSelected = true;
        /// <summary>
        /// Only used with spawn next wave timer method
        /// </summary>
        [SerializeField] private float spawnTimer = 0;
        [SerializeField] private LayerMask playerLayer = new LayerMask();
        [SerializeField] [Range(1, 1000)] private float playerCheckRadius = 20;
        [SerializeField] private bool detectFromSeperateObject = false;
        /// <summary>
        /// Only used when detecting from seperate object
        /// </summary>
        [SerializeField] private Transform objectToDetectFrom;
        [SerializeField] private bool disableOnPlayerDistance = false;
        [Tooltip("The tag used to find players for distance search")]
        [SerializeField] private string playerTag = "";
        [SerializeField] [Range(1, 5000)] private float playerDistance = 500;
        [SerializeField] private bool sameMobsEveryWave = true;
        [SerializeField] [Range(1, 50)] private int numberOfWaves = 1;
        [SerializeField] private bool sameTimerEveryWave = true;
        [SerializeField] private SpawnNextWave spawnNextWave;
        /// <summary>
        /// Only used with spawn next wave continous kills method
        /// </summary>
        [SerializeField] [Range(1, 100)] private int spawnContinueKillAmount = 1;
        /// <summary>
        /// Only used with spawn next wave continous kills method
        /// </summary>
        [SerializeField] [Range(1, 100)] private int continuousAmountOutAtOnce = 1;
        /// <summary>
        /// Only used with spawn next wave continous kills method
        /// </summary>
        [SerializeField] private float continuousDelayWaves = 0;
        /// <summary>
        /// Only used with spawn next wave After Wave Killed method
        /// </summary>
        [SerializeField] private float delayBetweenWaves = 0;
        [SerializeField] private float delayBetweenSpawns = 0;
        [SerializeField] private bool spawnOnLoad = false;
        [SerializeField] private bool restartSpawnerOnPlayerDistance = false;
        [SerializeField] private bool dontResetMobOnDisable = false;
        #endregion

        #region Public Properties/Structs
        public IntObjDictionary Waves = IntObjDictionary.New<IntObjDictionary>();
#if UNITY_EDITOR
#if !UNITY_2020_2_OR_NEWER
        [ReorderableList(elementsAreSubassets = true)]
#endif
        public List<Mobs> currentWave;
        public int CurWaveTab = 0;
        public IntObjDictionary editorWaves = IntObjDictionary.New<IntObjDictionary>();
#endif

        public enum SpawnNextWave { Timer, After_Wave_Killed, Continuous_Kills };

        [System.Serializable]
        public class Wave
        {
            public List<Mobs> mobs;
            public float timer;
        }

        [System.Serializable]
        public struct Mobs
        {
            public GameObject prefab;
            [MinMaxSlider(0, 100)] public Vector2Int spawnRange;
        }

        public string PlayerTag { get { return playerTag; } set { playerTag = value; } }

        // EVENTS
        public OnWaveStart waveStartEvent;
        public OnWaveEnd waveEndEvent;
        public UnityEvent allWavesCompleted;
        public UnityEvent allEnemiesDefeated;
        #endregion

        #region Private Properties/Structs
        private bool canSpawn = true;
        [SerializeField]
        private List<SpawnedMob> spawnedMobs = new List<SpawnedMob>();
        private bool playerNearby = false;
        private int wavesSpawned = 0;
        private Collider[] hitColliders = new Collider[10];
        private PoolingManager poolingManager;
        private Quaternion startRot;
        // Obstacle avoidance variables
        private LayerMask combinedMask;
        private const int lowPrecisionPoint = 50;
        private const int medPrecisionPoint = 150;
        private const int highPrecisionPoint = 400;
        // Disable on player distance variables
        private GameObject[] players;
        private List<DisabledMob> mobsDisabled = new List<DisabledMob>();
        private int playersDetected = 0;
        private int exitLoop = 0;
        private const string poolTagPrefix = "SpawnFactory";
        private List<ContinousSpawn> mobsForContinuous = new List<ContinousSpawn>();
        private bool isNewWave = false;
        [SerializeField]
        int currentSpawnedCount = 0;
        private List<SpawnedMob> mobsLateDisableKills = new List<SpawnedMob>();
        private string coroutineTag = "EnemySpawner";
        private bool allEnemiesDead = false;

        private struct SpawnedMob
        {
            public string poolTag;
            public GameObject obj;
            public GameObject prefab;

            public SpawnedMob(string poolTag, GameObject obj, GameObject prefab)
            {
                this.poolTag = poolTag;
                this.obj = obj;
                this.prefab = prefab;
            }
        }

        private struct DisabledMob
        {
            public string poolTag;
            public Vector3 position;
            public Quaternion rotation;
            public GameObject prefab;
            public GameObject obj;

            public DisabledMob(string poolTag, Vector3 position, Quaternion rotation, GameObject prefab, GameObject obj)
            {
                this.poolTag = poolTag;
                this.position = position;
                this.rotation = rotation;
                this.prefab = prefab;
                this.obj = obj;
            }
        }

        private struct ContinousSpawn
        {
            public Mobs mob;
            public int mobAmount;

            public ContinousSpawn(Mobs mob, int mobAmount)
            {
                this.mob = mob;
                this.mobAmount = mobAmount;
            }
        }
        #endregion

        #region Unity Callbacks
        protected override void Start()
        {
            base.Start();

            if (spawnMethod == SpawnMethod.Pooling)
            {
                if (PoolingManager.instance != null)
                {
                    poolingManager = PoolingManager.instance;
                    AddMobsToPool();
                }
                else
                {
                    spawnMethod = SpawnMethod.Instantiate;
#if UNITY_EDITOR
                    Debug.LogError("No pooling manager found. Spawn Method switched to instantiate.");
#endif
                }
            }

            if (disableOnPlayerDistance && playerTag != "")
                players = GameObject.FindGameObjectsWithTag(playerTag);
            else if (disableOnPlayerDistance && playerTag == "")
            {
                disableOnPlayerDistance = false;
#if UNITY_EDITOR
                Debug.LogError("No Player Tag set for spawner \"" + name + "\" disable on player distance now disabled.");
#endif
            }

            combinedMask = GroundLayerMask + ObstaclesToAvoidLayers;
            startRot = Quaternion.Euler(transform.rotation.eulerAngles + StartRotation);
            InitializeAvoidPrecision();
        }

        public void Update()
        {
            if (!spawnOnLoad && playerNearby && canSpawn || spawnOnLoad && canSpawn)
            {
                if (spawnNextWave != SpawnNextWave.Continuous_Kills)
                {
                    if (wavesSpawned < numberOfWaves)
#if MORE_EFFICIENT_COROUTINES
                        Timing.RunCoroutine(StartSpawnCoroutine(), coroutineTag);
#else
                        StartCoroutine(StartSpawnCoroutine());
#endif
                }
                else
                {
                    if (wavesSpawned <= numberOfWaves)
                    {
#if MORE_EFFICIENT_COROUTINES
                        Timing.RunCoroutine(StartSpawnCoroutine(), coroutineTag);
#else
                        StartCoroutine(StartSpawnCoroutine());
#endif
                    }
                }
            }

            // If there are still mobs alive wait to finish enemies defeated
            if (wavesSpawned > numberOfWaves && currentSpawnedCount <= 0 && !allEnemiesDead)
            {
                Invoke_AllEnemiesDefeated();
                allEnemiesDead = true;
            }
        }

        private void FixedUpdate()
        {
            if (restartSpawnerOnPlayerDistance)
                RestartOnPlayerDistance();

            if (!spawnOnLoad)
            {
                if (spawnNextWave != SpawnNextWave.Continuous_Kills)
                {
                    if (wavesSpawned < numberOfWaves)
                        DetectPlayerNearby();
                }
                else
                {
                    if (wavesSpawned <= numberOfWaves)
                        DetectPlayerNearby();
                }
            }

            if (spawnedMobs.Count > 0)
                HandleSpawnedMobsDisabling();

            // If player is close enough to enemy's last position then respawn the enemy
            if (disableOnPlayerDistance && mobsDisabled.Count > 0)
            {
                foreach (DisabledMob mob in mobsDisabled)
                {
                    bool mobRespawned = RespawnOnPlayerDistance(mob);
                    if (mobRespawned)
                        break;
                }
            }

            if (mobsLateDisableKills.Count > 0 && spawnMethod == SpawnMethod.Pooling)
            {
                foreach (SpawnedMob mob in mobsLateDisableKills)
                    ReturnKilledMobToPool(mob);
            }
        }
        #endregion

        #region Initialize
        private void AddMobsToPool()
        {
            if (sameMobsEveryWave)
            {
                foreach (Mobs mob in mobs)
                {
                    if (mob.prefab != null)
                        poolingManager.AddObjectToPool(MakeSpawnerPoolTag(mob.prefab), mob.prefab, (mob.spawnRange.y * numberOfWaves), true, null);
                }
            }
            else
            {
                for (int i = 0; i < numberOfWaves; i++)
                {
                    foreach (Mobs mob in Waves.dictionary[i].mobs)
                    {
                        if (mob.prefab != null)
                            poolingManager.AddObjectToPool(MakeSpawnerPoolTag(mob.prefab), mob.prefab, mob.spawnRange.y, true, null);
                    }
                }
            }
        }

        /// <summary>
        /// Initialize how many loops to try avoiding obstacles before quitting
        /// </summary>
        private void InitializeAvoidPrecision()
        {
            exitLoop = 0;
            if (NavMeshSpawn)
                exitLoop += 15;

            switch (ObstacleAvoidPercision)
            {
                case AvoidancePrecision.None:
                    exitLoop += 1;
                    break;
                case AvoidancePrecision.Low:
                    exitLoop += lowPrecisionPoint;
                    break;
                case AvoidancePrecision.Medium:
                    exitLoop += medPrecisionPoint;
                    break;
                case AvoidancePrecision.High:
                    exitLoop += highPrecisionPoint;
                    break;
                default:
                    exitLoop += 1;
                    break;
            }
        }
        #endregion

        #region Player Detection
        /// <summary>
        /// If player is nearby set playerNearby bool to true;
        /// </summary>
        private void DetectPlayerNearby()
        {
            playersDetected = 0;
            if (!detectFromSeperateObject)
                playersDetected = Physics.OverlapSphereNonAlloc(transform.position, playerCheckRadius, hitColliders, playerLayer);
            else
                playersDetected = Physics.OverlapSphereNonAlloc(objectToDetectFrom.position, playerCheckRadius, hitColliders, playerLayer);

            if (playersDetected > 0)
                playerNearby = true;
            else
                playerNearby = false;
        }

        private void RestartOnPlayerDistance()
        {
            bool playersAreTooFar = CheckIfAllPlayersTooFar(transform.position);

            if (playersAreTooFar)
                RestartSpawner();
        }
        #endregion

        #region Handle Spawned Mobs Disabling

        /// <summary>
        /// Handle if a spawned mob should disable with player distance and what happens when it disables
        /// </summary>
        private void HandleSpawnedMobsDisabling()
        {
            switch (spawnMethod)
            {
                case SpawnMethod.Pooling:
                    // If pooling check for mobs disabled to return to pool and also check for disable on distance if they're active
                    foreach (SpawnedMob mob in spawnedMobs)
                    {
                        bool mobReturnedToPool = ReturnKilledMobToPool(mob);
                        if (mobReturnedToPool)
                        {
                            spawnedMobs.Remove(mob);
                            currentSpawnedCount--;
                            HandleSpawnedMobsDisabling();
                            break;
                        }

                        if (disableOnPlayerDistance)
                        {
                            if (!restartSpawnerOnPlayerDistance)
                            {
                                bool mobDisabled = DisableOnPlayerDistance(mob);
                                if (mobDisabled)
                                {
                                    poolingManager.ReturnObjToPool(mob.poolTag, mob.obj);
                                    HandleSpawnedMobsDisabling();
                                    break;
                                }
                            }
                        }
                    }
                    break;

                case SpawnMethod.Instantiate:
                    foreach (SpawnedMob mob in spawnedMobs)
                    {
                        if (mob.obj == null)
                        {
                            spawnedMobs.Remove(mob);
                            currentSpawnedCount--;
                            HandleSpawnedMobsDisabling();
                            break;
                        }

                        // If not pooling, but still disabling on distance then destroy mob on distance
                        if (disableOnPlayerDistance)
                        {
                            if (!restartSpawnerOnPlayerDistance)
                            {
                                bool mobDisabled = DisableOnPlayerDistance(mob);
                                if (mobDisabled)
                                {
                                    if (dontResetMobOnDisable)
                                        mob.obj.SetActive(false);
                                    else
                                        Destroy(mob.obj);
                                    HandleSpawnedMobsDisabling();
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Return mob to pool if it's disabled (and also remove it from spawned mobs)
        /// </summary>
        private bool ReturnKilledMobToPool(SpawnedMob mob)
        {
            if (mob.obj.activeSelf == false)
            {
                poolingManager.ReturnObjToPool(mob.poolTag, mob.obj);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Disable a mob if all players are too far away from it
        /// </summary>
        /// <param name="mob"></param>
        /// <returns>Returns TRUE if the mob was disabled</returns>
        private bool DisableOnPlayerDistance(SpawnedMob mob)
        {
            bool playersAreTooFar = CheckIfAllPlayersTooFar(mob.obj.transform.position);

            if (playersAreTooFar)
            {
                DisabledMob disMob = new DisabledMob(mob.poolTag, mob.obj.transform.position, mob.obj.transform.rotation, mob.prefab, mob.obj);
                mobsDisabled.Add(disMob);
                spawnedMobs.Remove(mob);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Respawn the disabled mob if a player is close enough
        /// </summary>
        /// <param name="mob"></param>
        /// <returns>Returns TRUE if mob is respawned</returns>
        private bool RespawnOnPlayerDistance(DisabledMob mob)
        {
            bool aPlayerIsNear = !CheckIfAllPlayersTooFar(mob.position);
            // If a player is close then respawn enemy
            if (aPlayerIsNear)
            {
                if (dontResetMobOnDisable)
                {
                    mob.obj.SetActive(true);
                    spawnedMobs.Add(new SpawnedMob(mob.poolTag, mob.obj, mob.prefab));
                    mobsDisabled.Remove(mob);
                }
                else
                {
                    SpawnMob(mob.poolTag, mob.prefab, mob.position, mob.rotation);
                    mobsDisabled.Remove(mob);
                }
                return true;
            }
            return false;
        }
        #endregion

        #region Spawn Enemies
#if MORE_EFFICIENT_COROUTINES
        private IEnumerator<float> StartSpawnCoroutine()
        {
            canSpawn = false;

            if (spawnNextWave != SpawnNextWave.Continuous_Kills)
            {
                // If wave has no mobs to spawn, continue to next wave
                if (sameMobsEveryWave || Waves.dictionary.ContainsKey(wavesSpawned) && Waves.dictionary[wavesSpawned].mobs.Count != 0)
                {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(SpawnEnemyWave(), coroutineTag));
                    // Start waiting
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(SpawnEnemies(), coroutineTag));
                }
                else
                {
                    wavesSpawned++;
                    yield break;
                }
            }
            else
            {
                if (CheckContinuousAdvanceWaves())
                    AdvanceWavesInContinuous();

                if (!isNewWave && wavesSpawned <= numberOfWaves)
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(SpawnEnemiesContinuously(), coroutineTag));

                // Start waiting
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(SpawnEnemies(), coroutineTag));
            }

            canSpawn = true;
            yield break;
        }
#else
        private IEnumerator StartSpawnCoroutine()
        {
            canSpawn = false;

            if (spawnNextWave != SpawnNextWave.Continuous_Kills)
            {
                // If wave has no mobs to spawn, continue to next wave
                if (sameMobsEveryWave || Waves.dictionary.ContainsKey(wavesSpawned) && Waves.dictionary[wavesSpawned].mobs.Count != 0)
                {
                    yield return StartCoroutine(SpawnEnemyWave());
                    // Start waiting
                    yield return StartCoroutine(SpawnEnemies());
                }
                else
                {
                    wavesSpawned++;
                    yield break;
                }
            }
            else
            {
                if (CheckContinuousAdvanceWaves())
                    AdvanceWavesInContinuous();

                if (!isNewWave && wavesSpawned <= numberOfWaves)
                    yield return StartCoroutine(SpawnEnemiesContinuously());

                // Start waiting
                yield return StartCoroutine(SpawnEnemies());
            }

            canSpawn = true;
            yield break;
        }
#endif

        /// <summary>
        /// Spawns all enemies
        /// </summary>
        /// <returns></returns>
#if MORE_EFFICIENT_COROUTINES
        private IEnumerator<float> SpawnEnemies()
        {
            // Check how to wait on spawning
            switch (spawnNextWave)
            {
                case SpawnNextWave.Timer:
                    if (sameTimerEveryWave || sameMobsEveryWave)
                        yield return Timing.WaitForSeconds(spawnTimer);
                    else
                        yield return Timing.WaitForSeconds(Waves.dictionary[wavesSpawned].timer);
                    Invoke_WaveEndEvent(wavesSpawned);
                    if (wavesSpawned >= numberOfWaves)
                    {
                        Invoke_AllWavesDoneEvent();
                        wavesSpawned++;
                    }
                    break;
                case SpawnNextWave.After_Wave_Killed:
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(CheckAllSpawnedDead()));
                    Invoke_WaveEndEvent(wavesSpawned);
                    if (wavesSpawned >= numberOfWaves)
                    {
                        Invoke_AllWavesDoneEvent();
                        wavesSpawned++;
                    }
                    if (delayBetweenWaves > 0)
                        yield return Timing.WaitForSeconds(delayBetweenWaves);
                    break;
                case SpawnNextWave.Continuous_Kills:
                    if (wavesSpawned <= numberOfWaves)
                        yield return Timing.WaitUntilDone(Timing.RunCoroutine(CheckCanContinueSpawning()));
                    if (isNewWave && currentSpawnedCount <= 0)
                    {
                        Invoke_WaveEndEvent(wavesSpawned - 1);
                        if (wavesSpawned >= numberOfWaves)
                        {
                            Invoke_WaveEndEvent(wavesSpawned);
                            Invoke_AllWavesDoneEvent();
                        }
                        yield return Timing.WaitForSeconds(continuousDelayWaves);
                        isNewWave = false;
                    }
                    break;
            }
            yield break;
        }

        private IEnumerator<float> CheckAllSpawnedDead()
        {
            while (currentSpawnedCount > 0)
                yield return Timing.WaitForSeconds(0.1f);
            yield break;
        }

        private IEnumerator<float> CheckCanContinueSpawning()
        {
            while (((continuousAmountOutAtOnce - currentSpawnedCount) < spawnContinueKillAmount)
                || (isNewWave && currentSpawnedCount > 0 && wavesSpawned <= numberOfWaves))
            {
                yield return Timing.WaitForSeconds(0.1f);
            }

            yield break;
        }
#else
        private IEnumerator SpawnEnemies()
        {
            // Check how to wait on spawning
            switch (spawnNextWave)
            {
                case SpawnNextWave.Timer:
                    if (sameTimerEveryWave || sameMobsEveryWave)
                        yield return new WaitForSeconds(spawnTimer);
                    else
                        yield return new WaitForSeconds(Waves.dictionary[wavesSpawned].timer);
                    Invoke_WaveEndEvent(wavesSpawned);
                    if (wavesSpawned >= numberOfWaves)
                    {
                        Invoke_AllWavesDoneEvent();
                        wavesSpawned++;
                    }
                    break;
                case SpawnNextWave.After_Wave_Killed:
                    yield return new WaitWhile(() => currentSpawnedCount > 0);
                    Invoke_WaveEndEvent(wavesSpawned);
                    if (wavesSpawned >= numberOfWaves)
                    {
                        Invoke_AllWavesDoneEvent();
                        wavesSpawned++;
                    }
                    if (delayBetweenWaves > 0)
                        yield return new WaitForSeconds(delayBetweenWaves);
                    break;
                case SpawnNextWave.Continuous_Kills:
                    if (wavesSpawned <= numberOfWaves)
                        yield return new WaitWhile(() => ((continuousAmountOutAtOnce - currentSpawnedCount) < spawnContinueKillAmount) || (isNewWave && currentSpawnedCount > 0 && wavesSpawned <= numberOfWaves));
                    if (isNewWave && currentSpawnedCount <= 0)
                    {
                        Invoke_WaveEndEvent(wavesSpawned - 1);
                        if (wavesSpawned >= numberOfWaves)
                        {
                            Invoke_WaveEndEvent(wavesSpawned);
                            Invoke_AllWavesDoneEvent();
                        }
                        yield return new WaitForSeconds(continuousDelayWaves);
                        isNewWave = false;
                    }
                    break;
            }
            yield break;
        }
#endif

        /// <summary>
        /// Spawns a wave of enemies
        /// </summary>
#if MORE_EFFICIENT_COROUTINES
        private IEnumerator<float> SpawnEnemyWave()
        {
            List<Mobs> mobWave;
            if (sameMobsEveryWave)
                mobWave = mobs;
            else
                mobWave = Waves.dictionary[wavesSpawned].mobs;

            if (mobWave.Count > 0)
            {
                Invoke_WaveStartEvent(wavesSpawned + 1);
                foreach (Mobs mob in mobWave)
                {
                    if (mob.prefab != null)
                    {
                        int amount = Random.Range(mob.spawnRange.x, mob.spawnRange.y + 1);
                        for (int i = 0; i < amount; i++)
                        {
                            bool mobSpawned = SpawnWaveMob(mob);
                            currentSpawnedCount++;
                            if (mobSpawned)
                                yield return Timing.WaitForSeconds(delayBetweenSpawns);
                        }
                    }
                }
            }
            wavesSpawned++;
            yield break;
        }
#else
        private IEnumerator SpawnEnemyWave()
        {
            List<Mobs> mobWave;
            if (sameMobsEveryWave)
                mobWave = mobs;
            else
                mobWave = Waves.dictionary[wavesSpawned].mobs;

            if (mobWave.Count > 0)
            {
                Invoke_WaveStartEvent(wavesSpawned + 1);
                foreach (Mobs mob in mobWave)
                {
                    int amount = Random.Range(mob.spawnRange.x, mob.spawnRange.y + 1);
                    for (int i = 0; i < amount; i++)
                    {
                        bool mobSpawned = SpawnWaveMob(mob);
                        currentSpawnedCount++;
                        if (mobSpawned)
                            yield return new WaitForSeconds(delayBetweenSpawns);
                    }
                }
            }
            wavesSpawned++;
            yield break;
        }
#endif

        /// <summary>
        /// Spawns a mob when using After wave Killed or Timer method for spawn next wave
        /// </summary>
        /// <param name="mob">the mob to spawn</param>
        /// <returns>Returns TRUE if a mob is spawned</returns>
        private bool SpawnWaveMob(Mobs mob)
        {
            int loopCount = 0;
            Vector3 spawnPosition = RaycastPosObstacleAvoid(loopCount);
            if (spawnPosition != Vector3.zero)
            {
                SpawnMob(MakeSpawnerPoolTag(mob.prefab), mob.prefab, spawnPosition, startRot);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Spawns as many enemies as specified in the continuous spawning (Only for Continuous Kills Spawn Type)
        /// </summary>
#if MORE_EFFICIENT_COROUTINES
    private IEnumerator<float> SpawnEnemiesContinuously()
    {
        for (int i = currentSpawnedCount; i < continuousAmountOutAtOnce; i++)
        {
            bool mobSpawned = SpawnMobInContinuous();
            if (mobSpawned)
            {
                currentSpawnedCount++;
                // Causes a delay before the next mob can spawn
                yield return Timing.WaitForSeconds(delayBetweenSpawns);
            }
            else
            {
                // If not last wave revert i to spawn previous mob
                if (wavesSpawned != numberOfWaves)
                    i--;
                else
                    break;

                // If a mob isn't spawned advance to next wave
                AdvanceWavesInContinuous();

                if (continuousDelayWaves > 0)
                    break;
            }
        }
        yield break;
    }
#else
        private IEnumerator SpawnEnemiesContinuously()
        {
            for (int i = currentSpawnedCount; i < continuousAmountOutAtOnce; i++)
            {
                bool mobSpawned = SpawnMobInContinuous();
                if (mobSpawned)
                {
                    currentSpawnedCount++;
                    // Causes a delay before the next mob can spawn
                    yield return new WaitForSeconds(delayBetweenSpawns);
                }
                else
                {
                    // If not last wave revert i to spawn previous mob
                    if (wavesSpawned != numberOfWaves)
                        i--;
                    else
                        break;

                    // If a mob isn't spawned advance to next wave
                    AdvanceWavesInContinuous();

                    if (continuousDelayWaves > 0)
                        break;
                }
            }
            yield break;
        }
#endif

        /// <summary>
        /// Spawn a mob when using Continuous Kills as spawn next wave method 
        /// </summary>
        /// <returns>Returns TRUE if a mob was spawned</returns>
        private bool SpawnMobInContinuous()
        {
            int loopCount = 0;
            Vector3 spawnPosition = RaycastPosObstacleAvoid(loopCount);
            bool mobSpawned = false;
            if (spawnPosition != Vector3.zero)
            {
                for (int j = 0; j < mobsForContinuous.Count; j++)
                {
                    if (mobsForContinuous[j].mobAmount > 0)
                    {
                        SpawnMob(MakeSpawnerPoolTag(mobsForContinuous[j].mob.prefab), mobsForContinuous[j].mob.prefab, spawnPosition, startRot);
                        ContinousSpawn spawn = mobsForContinuous[j];
                        spawn.mobAmount--;
                        mobsForContinuous[j] = spawn;
                        mobSpawned = true;
                        break;
                    }
                }
            }
            return mobSpawned;
        }

        /// <summary>
        /// Raycasts downward and decides if it should spawn or avoid obstacle
        /// </summary>
        /// <param name="loopCount"> Number of times the raycast has run (used as recursive check if running too much).</param>
        /// <returns>A clear position to spawn (after obstacle avoidance)</returns>
        private Vector3 RaycastPosObstacleAvoid(int loopCount)
        {
            loopCount++;
            if (CheckAvoidancePrecisionFail(loopCount))
            {
#if UNITY_EDITOR
                Debug.Log("Spawner \"" + name + "\" could not avoid obstacles when spawning.");
#endif
                return Vector3.zero;
            }

            Vector3 position = GetSpawnPosition();

            if (spawnType == SpawnType.Point)
                return position;

            RaycastHit obHit;
            if (Physics.Raycast(position, Vector3.down, out obHit, 1000f, combinedMask))
            {
                if (GroundLayerMask.Contains(obHit.collider.gameObject.layer))
                {
                    Vector3 spawnPos = new Vector3(position.x, obHit.point.y, position.z);
                    if (spawnPos != Vector3.zero)
                    {
                        if (!NavMeshSpawn)
                            return spawnPos;
                        else if (CheckOnNavMesh(spawnPos))
                            return spawnPos;
                        else
                            return RaycastPosObstacleAvoid(loopCount);
                    }
                    else
                        return RaycastPosObstacleAvoid(loopCount);
                }
                else
                    return RaycastPosObstacleAvoid(loopCount);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Spawner \"" + name + "\" has no layer to spawn under it.");
#endif
                return Vector3.zero;
            }
        }

        /// <summary>
        /// Spawn an enemy of given prefab
        /// </summary>
        /// <param name="poolTag">The tag to reference a specific pool in pooling manager</param>
        /// <param name="prefab">The prefab used to spawn an object</param>
        /// <param name="spawnPosition">The position to place the spawned object</param>
        /// <param name="rotation">The rotation of the spawned object</param>
        private void SpawnMob(string poolTag, GameObject prefab, Vector3 spawnPosition, Quaternion rotation)
        {
            if (UseRandomStartRotation)
                rotation = CalculateRandomSpawnRotation();

            GameObject spawn = null;

            switch (spawnMethod)
            {
                case SpawnMethod.Instantiate:
                    spawn = Instantiate(prefab, spawnPosition, rotation, transform);
                    break;

                case SpawnMethod.Pooling:
                    spawn = poolingManager.GetPooledObject(poolTag);
                    spawn.transform.position = spawnPosition;
                    spawn.transform.rotation = rotation;
                    NavMeshAgent nva = spawn.GetComponent<NavMeshAgent>();
                    if (nva != null)
                        nva.Warp(spawnPosition);
                    break;
            }

            if (spawn != null)
            {
                if (spawn.GetComponent<SpawnerCommunicator>() != null)
                    spawn.GetComponent<SpawnerCommunicator>().InitializeData(this);
                spawnedMobs.Add(new SpawnedMob(poolTag, spawn, prefab));
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Enemy to spawn is null somehow (should never happen).");
#endif
            }
        }
        #endregion

        #region Calculating Spawn Position & Rotation
        /// <summary>
        /// Calculate position within the spawner to spawn
        /// </summary>
        /// <returns>The position to spawn</returns>
        private Vector3 GetSpawnPosition()
        {
            switch (spawnType)
            {
                case SpawnType.Point:
                    return transform.position;

                default:
                    Triangle randomTri = PickRandomTriangle();
                    Vector2 point = RandomPointInTriangle(randomTri);
                    return new Vector3(point.x, randomTri.a.y + 1, point.y);
            }
        }

        /// <summary>
        /// Calculate a random Y rotation between 0-360
        /// </summary>
        /// <returns>Returns a Quaternion with Eulers of (0, randomY, 0)</returns>
        private Quaternion CalculateRandomSpawnRotation()
        {
            float randomYRot = Random.Range(0, 361);
            Vector3 randomRot = new Vector3(0, randomYRot, 0);
            return Quaternion.Euler(randomRot);
        }

        /// <summary>
        /// Find a random point within a triangle in the spawner
        /// </summary>
        /// <param name="tri"> The triangle to find point within</param>
        /// <returns>Returns random point within given triangle</returns>
        private Vector2 RandomPointInTriangle(Triangle tri)
        {
            var r1 = Mathf.Sqrt(Random.Range(0f, 1f));
            var r2 = Random.Range(0f, 1f);
            var m1 = 1 - r1;
            var m2 = r1 * (1 - r2);
            var m3 = r2 * r1;

            Vector2 p1 = new Vector2(tri.a.x, tri.a.z);
            Vector2 p2 = new Vector2(tri.b.x, tri.b.z);
            Vector2 p3 = new Vector2(tri.c.x, tri.c.z);
            return (m1 * p1) + (m2 * p2) + (m3 * p3);
        }

        /// <summary>
        /// Find a random triangle within spawner area
        /// </summary>
        /// <returns>Return a random triangle from spawner area</returns>
        private Triangle PickRandomTriangle()
        {
            float rng = Random.Range(0f, TotalArea);
            for (int i = 0; i < Triangles.Count; ++i)
            {
                if (rng < Triangles[i].area)
                {
                    return Triangles[i];
                }
                rng -= Triangles[i].area;
            }
            // Should normally not get here
            return Triangles[0];
        }
        #endregion

        #region Checking Functions

        /// <summary>
        /// Check if continuous kills spawning needs to advance waves
        /// </summary>
        /// <returns></returns>
        private bool CheckContinuousAdvanceWaves()
        {
            if (mobsForContinuous.Count > 0)
            {
                foreach (ContinousSpawn mob in mobsForContinuous)
                {
                    if (mob.mobAmount > 0)
                        return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns TRUE if all players are further than set distance from mobPos
        /// </summary>
        /// <param name="mobPos"> The position of the enemy</param>
        /// <returns></returns>
        private bool CheckIfAllPlayersTooFar(Vector3 mobPos)
        {
            if (players.Length > 0)
            {
                foreach (GameObject player in players)
                {
                    Vector3 offset = player.transform.position - mobPos;
                    float dis = offset.sqrMagnitude;

                    if (dis < playerDistance * playerDistance)
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks it should stop trying to avoid obstacles (for performance sake)
        /// </summary>
        /// <param name="loopCount">Number of times the obstacle avoidance has run</param>
        /// <returns> True if avoidance should stop, False if it can keep running</returns>
        private bool CheckAvoidancePrecisionFail(int loopCount)
        {
            if (loopCount > exitLoop)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines if pos is on navmash
        /// </summary>
        /// <param name="pos">Position to check if is on navmesh</param>
        /// <returns>True if it's on navmesh, False if it is not </returns>
        private bool CheckOnNavMesh(Vector3 pos)
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(pos, out navHit, 0.05f, NavMesh.AllAreas))
                return true;
            else
                return false;
        }
        #endregion

        #region More Functions
        /// <summary>
        /// Only used to advance to next wave in continuous kills spawning mode
        /// </summary>
        private void AdvanceWavesInContinuous()
        {
            mobsForContinuous.Clear();
            if (wavesSpawned != numberOfWaves)
            {
                Invoke_WaveStartEvent(wavesSpawned + 1);
                if (sameMobsEveryWave)
                {
                    foreach (Mobs mob in mobs)
                    {
                        if (mob.prefab != null)
                        {
                            int amount = Random.Range(mob.spawnRange.x, mob.spawnRange.y + 1);
                            mobsForContinuous.Add(new ContinousSpawn(mob, amount));
                        }
                    }
                }
                else
                {
                    foreach (Mobs mob in Waves.dictionary[wavesSpawned].mobs)
                    {
                        if (mob.prefab != null)
                        {
                            int amount = Random.Range(mob.spawnRange.x, mob.spawnRange.y + 1);
                            mobsForContinuous.Add(new ContinousSpawn(mob, amount));
                        }
                    }
                }
            }

            if (wavesSpawned != 0)
                isNewWave = true;

            wavesSpawned++;
        }

        /// <summary>
        /// Makes a unique pool tag for spawner mobs
        /// </summary>
        /// <param name="name">Mob's Name</param>
        /// <returns>Returns pool tag to spawn with</returns>
        private string MakeSpawnerPoolTag(GameObject prefab)
        {
            return string.Concat(poolTagPrefix, prefab.GetInstanceID().ToString());
        }

        /// <summary>
        /// Resets Spawner (sets groups spawned to 0)
        /// </summary>
        public void RestartSpawner()
        {
            foreach (SpawnedMob mob in spawnedMobs)
            {
                switch (spawnMethod)
                {
                    case SpawnMethod.Pooling:
                        mob.obj.SetActive(false);
                        break;

                    case SpawnMethod.Instantiate:
                        Destroy(mob.obj);
                        break;
                }
            }

            mobsDisabled.Clear();
            mobsForContinuous.Clear();

#if MORE_EFFICIENT_COROUTINES
            Timing.KillCoroutines(coroutineTag);
#else
            StopCoroutine(StartSpawnCoroutine());
            if (spawnNextWave != SpawnNextWave.Continuous_Kills)
                StopCoroutine(SpawnEnemyWave());
            else
                StopCoroutine(SpawnEnemiesContinuously());
            StopCoroutine(SpawnEnemies());
#endif
            wavesSpawned = 0;
            allEnemiesDead = false;
            canSpawn = true;
        }

        /// <summary>
        /// If enemy is killed and not immediately disabling/destroying this can be called
        /// </summary>
        /// <param name="enemy"></param>
        public void EnemyKilledRemoveFromSpawned(GameObject enemy)
        {
            foreach (SpawnedMob mob in spawnedMobs)
            {
                if (mob.obj != null && mob.obj.activeSelf && mob.obj == enemy)
                {
                    spawnedMobs.Remove(mob);
                    currentSpawnedCount--;
                    if(spawnMethod == SpawnMethod.Pooling)
                        mobsLateDisableKills.Add(mob);
                    break;
                }
            }
        }
        #endregion

        #region Unity Events
        public void Invoke_WaveStartEvent(int waveNum)
        {
            if (waveStartEvent != null)
                waveStartEvent.Invoke(waveNum);
        }

        public void Invoke_WaveEndEvent(int waveNum)
        {
            if (waveEndEvent != null)
                waveEndEvent.Invoke(waveNum);
        }

        public void Invoke_AllWavesDoneEvent()
        {
            if (allWavesCompleted != null)
                allWavesCompleted.Invoke();
        }

        public void Invoke_AllEnemiesDefeated()
        {
            if (allEnemiesDefeated != null)
                allEnemiesDefeated.Invoke();
        }
        #endregion

        #region Gizmos
#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (ShowBoundsAlways && !showRadiusOnlyWhenSelected)
                DrawGizmos();
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            if (!ShowBoundsAlways || showRadiusOnlyWhenSelected)
                DrawGizmos();
        }

        private void DrawGizmos()
        {
            if (showSearchRadius)
            {
                Gizmos.color = Color.green;
                if (!detectFromSeperateObject)
                    Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
                else if (objectToDetectFrom != null)
                    Gizmos.DrawWireSphere(objectToDetectFrom.position, playerCheckRadius);
            }
        }
#endif
        #endregion
    }
}