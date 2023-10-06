using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Simulates the leaderboard functionality.
    /// </summary>
    public class Demo : MonoBehaviour
    {
        #region Private Editor Variables
        [SerializeField, Tooltip("A reference to the leaderboard in the scene.")]
        private Leaderboard _leaderboard;

        [SerializeField, Tooltip("The information label for the activated menu item.")]
        private Text _information;

        [SerializeField, Tooltip("An array of menu items that are selectable.")]
        private MenuItem[] _menuItems;
        #endregion

        #region Private Variables
        private ushort _menuIndex = 0;
        private bool _acceptInput = true;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _menuItems[_menuIndex].Activate();

            // Register Listeners
            _leaderboard.OnLeaderboardFinished += HandleOnLeaderboardFinished;
        }

        private void OnDestroy()
        {
            // Unregister Listeners
            _leaderboard.OnLeaderboardFinished -= HandleOnLeaderboardFinished;
        }

        private void Update()
        {
            // Input is blocked, when the leaderboard is active.
            if (_acceptInput)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // Menu - Select
                    _menuItems[_menuIndex].Select();
                }
                
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    // Menu - Up
                    if (_menuIndex > 0)
                    {
                        _menuItems[_menuIndex].Deactivate();
                        _menuIndex--;

                        _menuItems[_menuIndex].Activate();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    // Menu Down
                    if (_menuIndex < _menuItems.Length - 1)
                    {
                        _menuItems[_menuIndex].Deactivate();
                        _menuIndex++;

                        _menuItems[_menuIndex].Activate();
                    }
                }
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Notification that the leaderboard has finished.
        /// </summary>
        private void HandleOnLeaderboardFinished()
        {
            // Unblock Input
            _acceptInput = true;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates a random string that is used for the initials.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Range(0, s.Length)]).ToArray());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Display the current message in the information field.
        /// </summary>
        /// <param name="message"></param>
        public void ShowInformation(string message)
        {
            _information.text = message;
        }

        /// <summary>
        /// Prompt for a new entry with a random score.
        /// </summary>
        public void New()
        {
            _acceptInput = false;
            uint score = (uint)Random.Range(900000000, 999999999);

            if(!_leaderboard.AddEntry(score))
            {
                // Not a valid highscore, resume input.
                Debug.Log("This is not a valid highscore.");
                _acceptInput = true;
            }
        }

        /// <summary>
        /// Display the current leaderboard.
        /// </summary>
        public void View()
        {
            _acceptInput = false;
            _leaderboard.Display();
        }

        /// <summary>
        /// Generate up to 30 random leaderboard entries.
        /// </summary>
        public void Generate()
        {
            for (int x = 0; x < 30; x++)
            {
                _leaderboard.AddEntry(RandomString(3), (uint)Random.Range(999999, 999999999));
            }

            _leaderboard.SortEntries();
        }

        /// <summary>
        /// Clear the current leaderboard entries.
        /// </summary>
        public void Clear()
        {
            _leaderboard.ClearEntries();
        }
        #endregion
    }
}