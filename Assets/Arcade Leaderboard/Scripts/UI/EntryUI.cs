using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Controls the entry UI and allows players to enter their initials.
    /// </summary>
    public class EntryUI : MonoBehaviour
    {
        #region Private Editor References
        [SerializeField, Tooltip("The label that displays the current score.")]
        private Text _scoreLabel;

        [SerializeField, Tooltip("The label that displays the current rank.")]
        private Text _rankLabel;

        [SerializeField, Tooltip("The label that displays the countdown timer.")]
        private Text _timeLabel;

        [SerializeField, Tooltip("An array of labels that indicate the active initial.")]
        private Text[] _cursors;

        [SerializeField, Tooltip("An array of labels that display an initial.")]
        private Text[] _initials;

       
        #endregion

        #region Private Variables
        private int _cursorIndex;

        // The allowed characters for the players initials.
        private const string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private int _characterIndex = 0;

        public uint _score;
        private string _rank;
        private ushort _countdown;
        private ushort _timeout = 30;
        private bool _isReset = false;
        #endregion

        #region Public Events
        public Action<string, uint> OnEntryAdded;
        #endregion

        #region Private Properties
        /// <summary>
        /// The combined string that contains the players initials.
        /// </summary>
        private string Initials
        {
            get
            {
                return string.Join("", _initials.Select(x => x.text).ToArray());
            }
        }

        /// <summary>
        /// The players score.
        /// </summary>
        private uint Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                _scoreLabel.text = _score.ToString();
            }
        }

        /// <summary>
        /// The players rank.
        /// </summary>
        private string Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                _rank = value;
                _rankLabel.text = _rank;
            }
        }

        /// <summary>
        /// The current time remaining that a player is allowed to enter their initials.
        /// </summary>
        private ushort Countdown
        {
            get
            {
                return _countdown;
            }
            set
            {
                _countdown = value;
                _timeLabel.text = string.Format("({0})", _countdown);

            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The inactive time in (seconds) a player has to enter their initials.
        /// </summary>
        public ushort Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        #endregion

        #region Unity Methods
        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.LeftArrow) || horizontalInput < -0.5f)
            {
                // Handle left input
                ResetCountdown();

                if (_cursorIndex > 0)
                {
                    _cursorIndex--;
                    _cursors.All(x => x.enabled = true);

                    _characterIndex = _characters.IndexOf(_initials[_cursorIndex].text);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || horizontalInput > 0.5f)
            {
                // Handle right input
                ResetCountdown();

                if (_cursorIndex < _cursors.Length - 1)
                {
                    _cursorIndex++;
                    _cursors.All(x => x.enabled = true);

                    _characterIndex = _characters.IndexOf(_initials[_cursorIndex].text);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || verticalInput > 0.5f)
            {
                // Handle up input
                ResetCountdown();

                if (_characterIndex < _characters.Length - 1)
                    _characterIndex++;
                else
                    _characterIndex = 0;

                _initials[_cursorIndex].text = _characters[_characterIndex].ToString();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || verticalInput < -0.5f)
            {
                // Handle down input
                ResetCountdown();

                if (_characterIndex > 0)
                    _characterIndex--;
                else
                    _characterIndex = _characters.Length - 1;

                _initials[_cursorIndex].text = _characters[_characterIndex].ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Countdown <= 0 || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                // Accept the input and notify the listeners.
                if (OnEntryAdded != null)
                    OnEntryAdded.Invoke(Initials, Score);
            }

            // Cursor Blink
            if (Time.fixedTime % 0.5 < 0.2)
            {
                _cursors[_cursorIndex].enabled = false;
            }
            else
            {
                _cursors[_cursorIndex].enabled = true;
            }
        }

        #endregion

        #region Private Methods
        private void CountdownTimer()
        {
            // When reset, skip one cycle.
            if(_isReset)
            {
                _isReset = false;
                return;
            }

            if (--Countdown <= 0) CancelInvoke("CountdownTimer");
        }

        /// <summary>
        /// Reset the countdown timer to max.
        /// </summary>
        private void ResetCountdown()
        {
            _isReset = true;
            Countdown = Timeout;
        }

        /// <summary>
        /// Reset all initials to the first character.
        /// </summary>
        private void ResetInitials()
        {
            for(int x = 0; x < Initials.Length; x++)
            {
                _initials[x].text = _characters[0].ToString();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Display the entry UI and allow the player to input their initials.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="rank"></param>
        public void Display(uint score, string rank)
        {
            // Reset
            _cursorIndex = 0;
            _characterIndex = 0;
            Countdown = Timeout;

            ResetInitials();

            // Set current values
            Score = score;
            Rank = rank;

            gameObject.SetActive(true);

            // Start the countdown.
            InvokeRepeating("CountdownTimer", 1, 1);
        }

        /// <summary>
        /// Hide the entry UI.
        /// </summary>
        public void Hide()
        {
            // Stop the countdown.
            CancelInvoke("CountdownTimer");

            gameObject.SetActive(false);
        }
        #endregion
    }
}