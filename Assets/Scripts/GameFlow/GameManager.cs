using System.Security.AccessControl;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [ShowInInspector]
    public static GameMode SelectedMode;

    
    public void SelectGameMode(GameMode mode)
    {
        SelectedMode = mode;
    }

  
    // a wave based mode with lives.
    // a time based mode without lives. Could just spawn every time the player has shot an enemy, and maybe make some disappear? Maybe they go back into the gate



}


public enum GameMode
{
    Waves,
    TimeTrial
}