using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Controls the high score UI elements and how they are displayed.
    /// </summary>
    public class HighscoreUI : MonoBehaviour
    {
        #region Private Editor Variables
        [SerializeField, Tooltip("The parent that will contain the high score entries.")]
        private RectTransform _content;
        #endregion

        #region Private Variables
        private LayoutGroup _layout;

        private float _rectOffset = 0;
        private Vector2 _rectPosition = Vector2.zero;
        private float _displayTime = 0;
        private Entry _lastEntry = null;

        private bool _scroll = false;
        private float _scrollDelay = 1.5f;
        private float _scrollSpeed = 1.0f;
        private float _timeout = 15.0f;
        #endregion

        #region Public Properties
        /// <summary>
        /// The time in (seconds) the leaderboard should be displayed, after scrolling completes.
        /// </summary>
        public float Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// The delay in (seconds) before the leaderboard begins scrolling.
        /// </summary>
        public float ScrollDelay
        {
            get { return _scrollDelay; }
            set { _scrollDelay = value; }
        }

        /// <summary>
        /// The speed at which the leaderboard scrolls.
        /// </summary>
        public float ScrollSpeed
        {
            get { return _scrollSpeed; }
            set { _scrollSpeed = value; }
        }

        /// <summary>
        /// Returns the parent that contains the high score entries.
        /// </summary>
        public Transform Content
        {
            get { return _content.transform; }
        }

        /// <summary>
        /// Returns the LayoutGroup that contains the high score entries.
        /// </summary>
        public LayoutGroup Layout
        {
            get
            {
                if(_layout == null)
                    _layout = _content.GetComponent<LayoutGroup>();

                return _layout;
            }
        }
        #endregion

        #region Public Events
        public Action OnDisplayFinished;
        #endregion

        #region Unity Methods
        void Update()
        {
            // Allow for early exit, once the scroll delay has elapsed.
            if(Input.GetKeyDown(KeyCode.Return) && _displayTime + ScrollDelay < Time.time)
            {
                // Notify listeners.
                if (OnDisplayFinished != null)
                    OnDisplayFinished.Invoke();
            }

            if (_scroll)
            {
                // Wait until the delay has elapsed.
                if (_displayTime + ScrollDelay < Time.time)
                {
                    // Update the position of the entries.
                    _rectPosition.y = Mathf.Lerp(_rectOffset, 0, 
                        (Time.time - (_displayTime + ScrollDelay)) / (Layout.preferredHeight / (150 * ScrollSpeed)));

                    _content.anchoredPosition = _rectPosition;

                    // Start the timeout.
                    if (_rectPosition.y == 0)
                    {
                        _displayTime = Time.time;
                        _scroll = false;
                    }
                }
            }
            else if (_displayTime + Timeout < Time.time)
            {
                // Notify listeners once the timeout has elapsed.
                if (OnDisplayFinished != null)
                    OnDisplayFinished.Invoke();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Displays the leaderboard and highlight the new high score.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="color"></param>
        public void Display(Entry entry, Color color)
        {
            if (_lastEntry != null)
                _lastEntry.ResetColor();

            _lastEntry = entry;
            entry.SetColor(color);

            Display();
        }

        /// <summary>
        /// Displays the leaderboard.
        /// </summary>
        public void Display()
        {
            gameObject.SetActive(true);
            Canvas.ForceUpdateCanvases();

            if (Layout != null)
            {
                _rectOffset = Mathf.Max(0, Layout.preferredHeight);
                _content.anchoredPosition = new Vector2(0, _rectOffset);
            }
            
            _displayTime = Time.time;
            _scroll = true;
        }

        /// <summary>
        /// Hides the leaderboard.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
