using UnityEngine;
using UnityEditor;

namespace GameNative.Leaderboard
{
    /// <summary>
    /// Leaderboard editor extension class.
    /// Adds a reset button to the current inspector layout.
    /// </summary>
    [CustomEditor(typeof(Leaderboard))]
    public class LeaderboardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            // Add a button to allow the developer to reset the entries.
            Leaderboard leaderboard = (Leaderboard)target;
            if (GUILayout.Button("Clear Leaderboard Entries"))
            {
                leaderboard.ClearEntries();
            }

            EditorGUILayout.Space();
        }
    }
}
