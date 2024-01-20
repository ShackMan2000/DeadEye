using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LeaderBoardEntryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI playerNameText;

    [SerializeField] TextMeshProUGUI scoreText;

    [FormerlySerializedAs("highlight")] [SerializeField] Image background;

    [SerializeField] Color playerHighlightColor;
    [SerializeField] Color defaultColor;

    public void Initialize(LeaderBoard.SimplifiedLeaderBoardEntry entry)
    {
        rankText.text = (entry.Rank +1).ToString();
        playerNameText.text = entry.IsPlayer ? "You" : entry.PlayerName;
        scoreText.text = entry.Score.ToString("F0");
                background.color = entry.IsPlayer ? playerHighlightColor : defaultColor;
    }


 
}