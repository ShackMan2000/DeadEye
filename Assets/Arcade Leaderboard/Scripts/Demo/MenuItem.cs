using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Text based menu item using an activation indicator.
    /// </summary>
    public class MenuItem : MonoBehaviour
    {
        #region Private Editor Variables
        [SerializeField, Tooltip("The visual text indicator for an activated item.")]
        private Text _selector;
        #endregion

        #region Events
        public UnityEvent OnItemActivated;
        public UnityEvent OnItemSelected;
        #endregion

        #region Public Methods
        /// <summary>
        /// Activate the current menu item.
        /// </summary>
        public void Activate()
        {
            _selector.enabled = true;

            // Notify Listeners
            if (OnItemActivated != null)
                OnItemActivated.Invoke();
        }

        /// <summary>
        /// Deactivate the current menu item.
        /// </summary>
        public void Deactivate()
        {
            _selector.enabled = false;
        }

        /// <summary>
        /// Select the current menu item.
        /// </summary>
        public void Select()
        {
            if (OnItemSelected != null)
                OnItemSelected.Invoke();
        }
        #endregion
    }
}
