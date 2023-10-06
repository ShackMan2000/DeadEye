using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Manages the overall leaderboard settings and functionality.
    /// </summary>
    public class Leaderboard : MonoBehaviour
    {
        #region Private Editor Variables
        [Header("Highscore")]

        [SerializeField, Tooltip("A reference to the HighScoreUI in the scene.")]
        public HighscoreUI _highscoreUI;

        [SerializeField, Tooltip("The maximum number of entries for the leaderboard.")]
        private int _maxEntries = 30;

        [SerializeField, Tooltip("Allow duplicate scores to be replaced when the leaderboard is full.")]
        private bool _replaceDuplicates = false;

        [SerializeField, Range(0, 3), Tooltip("The delay in (seconds) before the leaderboard begins scrolling.")]
        private float _scrollDelay = 1.5f;

        [SerializeField, Range(0.1f, 1), Tooltip("The speed at which the leaderboard scrolls.")]
        private float _scrollSpeed = 1.0f;

        [SerializeField, Range(5, 60), Tooltip("The time in (seconds) the leaderboard should be displayed, after scrolling completes.")]
        private float _displayTimeout = 15.0f;

        [Header("Entry")]

        [SerializeField, Tooltip("A reference to the EntryUI in the scene.")]
        public EntryUI _entryUI;

        [SerializeField, Tooltip("The prefab for a high score entry.")]
        private GameObject _entryPrefab;

        [SerializeField, Tooltip("The color of a current high score entry.")]
        private Color _entryColor = Color.yellow;

        [SerializeField, Range(5, 60), Tooltip("The inactive time in (seconds) a player has to enter their initials.")]
        private ushort _entryTimeout;
        #endregion

        #region Private Variables
        private List<Entry> _entries = new List<Entry>();
        #endregion

        #region Public Events
        // Notification once the leaderboard has finished;
        public System.Action OnLeaderboardFinished;
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns the active state of the leaderboard.
        /// </summary>
        public bool IsActive
        {
            get { return _entryUI.isActiveAndEnabled || _highscoreUI.isActiveAndEnabled; }
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            // Set the HighscoreUI default values.
            _highscoreUI.ScrollDelay = _scrollDelay;
            _highscoreUI.ScrollSpeed = _scrollSpeed;
            _highscoreUI.Timeout = _displayTimeout;

            // Set the EntryUI default values.
            _entryUI.Timeout = _entryTimeout;

            // Register Events
            _entryUI.OnEntryAdded += HandleOnEntryAdded;
            _highscoreUI.OnDisplayFinished += HandleOnDisplayFinished;

            // Load any saved entries.
            LoadEntries();
        }

        private void OnDestroy()
        {
            // Unregister Events.
            _entryUI.OnEntryAdded -= HandleOnEntryAdded;
            _highscoreUI.OnDisplayFinished -= HandleOnDisplayFinished;
        }
        #endregion

        #region Event Handelers
        /// <summary>
        /// Accept a new high score entry and display the leaderboard.
        /// </summary>
        /// <param name="initials"></param>
        /// <param name="score"></param>
        private void HandleOnEntryAdded(string initials, uint score)
        {
            _entryUI.Hide();

            Entry entry = AddEntry(initials, score);
            SortEntries();

            _highscoreUI.Display(entry, _entryColor);
        }

        /// <summary>
        /// Hides the leaderboard once the display has finished.
        /// </summary>
        private void HandleOnDisplayFinished()
        {
            _highscoreUI.Hide();

            if (OnLeaderboardFinished != null)
                OnLeaderboardFinished.Invoke();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Find the entry that should be replaced.
        /// </summary>
        /// <returns></returns>
        private Entry FindReplacementEntry()
        {
            SortEntries();

            return _entries.Last();
        }

        /// <summary>
        /// Return the rank, based on the score.
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        private ushort GetRank(uint score)
        {
            Entry entry = _entries.Where(x => x.Score < score).FirstOrDefault();

            if(_replaceDuplicates && entry == null)
            {
                entry = _entries.Where(x => x.Score <= score).FirstOrDefault();
            }

            if (entry != null)
            {
                return entry.Rank;
            }

            return (ushort)(_entries.Count() + 1);
        }

        /// <summary>
        /// Add a new high score.
        /// </summary>
        /// <param name="initials"></param>
        /// <param name="score"></param>
        /// <param name="timestamp"></param>
        /// <param name="save">Save immediately after adding the entry.</param>
        /// <returns>The added entry will be returned.</returns>
        private Entry AddEntry(string initials, uint score, long timestamp, bool save = true)
        {
            Entry entry = null;
            if (_entries.Count < _maxEntries)
            {
                // Append a new entry.
                entry = Instantiate(_entryPrefab).GetComponent<Entry>();
                if (entry != null)
                {
                    entry.transform.SetParent(_highscoreUI.Content);
                    entry.Set(initials, score, timestamp);

                    _entries.Add(entry);
                }
            }
            else if (IsHighScore(score))
            {
                // Replace an existing entry.
                entry = FindReplacementEntry();
                entry.Set(initials, score, timestamp);
            }

            if (save) { SaveEntries(); }

            return entry;
        }

        /// <summary>
        /// Save the current entries to the PlayerPrefs.
        /// </summary>
        private void SaveEntries()
        {
            string t = JsonUtility.ToJson(new EntryDataWrapper { entries = _entries.Select(x => x.Data).ToArray() });
            PlayerPrefs.SetString("entries", JsonUtility.ToJson(new EntryDataWrapper { entries = _entries.Select(x => x.Data).ToArray() }));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Load the entries from the PlayerPrefs.
        /// </summary>
        private void LoadEntries()
        {
            _entries.Clear();

            string saveData = PlayerPrefs.GetString("entries");
            if (saveData != string.Empty)
            {
                EntryDataWrapper data = JsonUtility.FromJson<EntryDataWrapper>(saveData);
                
                for (int x = 0; x < data.entries.Length; x++)
                {
                    AddEntry(data.entries[x].name, data.entries[x].score, data.entries[x].timestamp, false);
                }
            }

            SortEntries();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Destroy all entry GameObjects and clear the array.
        /// </summary>
        public void ClearEntries()
        {
            _entries.ForEach(x => Destroy(x.gameObject));
            _entries.Clear();

            SaveEntries();
        }

        /// <summary>
        /// Displays the leaderboard.
        /// </summary>
        public void Display()
        {
            _highscoreUI.Display();
        }

        /// <summary>
        /// Add a new high score and show the entry dialog.
        /// </summary>
        /// <param name="score"></param>
        /// <returns>Returns true if the entry was added.</returns>
        public bool AddEntry(uint score)
        {
            if (IsHighScore(score))
            {
                _highscoreUI.Hide();
                _entryUI.Display(score, Entry.Ordinal(GetRank(score)));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Add a new high score.
        /// </summary>
        /// <param name="initials"></param>
        /// <param name="score"></param>
        /// <param name="save">Save immediately after adding the entry.</param>
        /// <returns>The added entry will be returned.</returns>
        public Entry AddEntry(string initials, uint score, bool save = true)
        {
            return AddEntry(initials, score, System.DateTime.Now.ToFileTimeUtc(), save);
        }

        /// <summary>
        /// Determine if the score qualifies as a high score.
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool IsHighScore(uint score)
        {
            if (_entries.Count == 0)
                return true;

            if (_entries.Count < _maxEntries)
                return true;

            if (_replaceDuplicates)
            {
                return _entries.FindLast(x => x.Score <= score) != null;
            }
            else
            {
                return _entries.FindLast(x => x.Score < score) != null;
            }
        }

        /// <summary>
        /// Sort the entries from highest to lowest.
        /// </summary>
        public void SortEntries()
        {
            // Update sorted rank order, based on score.
            _entries.Sort((entry1, entry2) =>
            {
                int result = entry2.Score.CompareTo(entry1.Score);
                return result != 0 ? result : entry2.Timestamp.CompareTo(entry1.Timestamp);
            });

            // Update visual rank text & hierarchy order.
            for (int x = 0; x < _entries.Count; x++)
            {
                _entries[x].Set(x + 1);
                _entries[x].transform.SetSiblingIndex(x);
            }
        }
        #endregion
    }
}