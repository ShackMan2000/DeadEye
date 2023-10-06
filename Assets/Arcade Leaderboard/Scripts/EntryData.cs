namespace GameNative.Leaderboard
{
    /// <summary>
    /// Holds leaderboard serialized information.
    /// </summary>
    [System.Serializable]
    public class EntryData
    {
        public long timestamp;
        public uint score;
        public string name;
    }

    /// <summary>
    /// A simple data wrapper for JsonUtility support.
    /// </summary>
    [System.Serializable]
    public struct EntryDataWrapper { public EntryData[] entries; }
}