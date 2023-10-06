using UnityEngine;
using UnityEngine.UI;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Prefab component that holds information about a single highscore entry.
    /// </summary>
    [System.Serializable]
    public class Entry : MonoBehaviour
    {
        #region Private Editor Variables
        [SerializeField, Tooltip("The label used to display the rank.")]
        private Text _rankLabel;

        [SerializeField, Tooltip("The label used to display the name.")]
        private Text _nameLabel;

        [SerializeField, Tooltip("The label used to display the score.")]
        private Text _scoreLabel;
        #endregion

        #region Private Variables
        private Color _rankColor;
        private Color _nameColor;
        private Color _scoreColor;
        #endregion

        #region Public Properties
        public ushort Rank { get; private set; }

        public uint Score
        {
            get
            {
                return uint.Parse(_scoreLabel.text);
            }
            private set
            {
                Data.score = value;
                _scoreLabel.text = Data.score.ToString().PadLeft(9, '0');
            }
        }

        public string Name
        {
            get
            {
                return Data.name;
            }
            private set
            {
                Data.name = value;
                _nameLabel.text = Data.name;
            }
        }

        public long Timestamp
        {
            get
            {
                return Data.timestamp;
            }
            private set
            {
                Data.timestamp = value;
            }
        }

        public EntryData Data { get; private set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Data = new EntryData();

            // Store the default color of each label.
            _rankColor = _rankLabel.color;
            _nameColor = _nameLabel.color;
            _scoreColor = _scoreLabel.color;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set the entry rank.
        /// </summary>
        /// <param name="rank"></param>
        public void Set(int rank)
        {
            Set((ushort)rank);
        }

        /// <summary>
        /// Set the entry rank.
        /// </summary>
        /// <param name="rank"></param>
        public void Set(ushort rank)
        {
            Rank = rank;
            _rankLabel.text = Ordinal(rank);
        }

        /// <summary>
        /// Set the entry name and score.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="score"></param>
        public void Set(string name, uint score, long timestamp)
        {
            Name = name;
            Score = score;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Set the entry labels color.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            _rankLabel.color = color;
            _nameLabel.color = color;
            _scoreLabel.color = color;
        }

        /// <summary>
        /// Set the entry labels color to original value.
        /// </summary>
        public void ResetColor()
        {
            _rankLabel.color = _rankColor;
            _nameLabel.color = _nameColor;
            _scoreLabel.color = _scoreColor;
        }

        /// <summary>
        /// Convert a numeric value to an ordinal.
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public static string Ordinal(ushort rank)
        {
            if (rank <= 0) return rank.ToString();

            switch (rank % 100)
            {
                case 11:
                case 12:
                case 13:
                    return rank + "TH";
            }

            switch (rank % 10)
            {
                case 1:
                    return rank + "ST";
                case 2:
                    return rank + "ND";
                case 3:
                    return rank + "RD";
                default:
                    return rank + "TH";
            }
        }
        #endregion
    }
}