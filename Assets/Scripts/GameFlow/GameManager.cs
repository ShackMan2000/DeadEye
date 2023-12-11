using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameMode CurrentGameMode { get; private set; }

    public static event Action<GameMode> OnGameStarted = delegate { };
            
    public static event Action OnGameFinished = delegate { };


    // the game mode might be overkill...
    
    // when game starts, UI needs to change. Actually that one needs to know which mode...
    // health counter needs to know game mode too. Though if enemies in time trial don't shoot, doesn't matter if it's in the scene or not
    // 
    // controllers turn into guns, and vice versa when game is over
    // should at least still all route through Game Manager
    
    
    
    // user should first see a button that starts a wave game
    // which triggers to start the next wave. 
    // Just have UI controller check the wave controller if it is above 1, in that case show next wave or quit game button
    
    
    // bigger question is how to make sure that all is hooked up to the prefab... or better have the UI controller above all that.
    
    
    [Button]
    public static void StartGame(GameMode m)
    {
        OnGameStarted?.Invoke(m);
    }
    
    [Button]
    public static void FinishGame()
    {
        OnGameFinished?.Invoke();
    }
    
    
  
    // a wave based mode with lives.
    // a time based mode without lives. Could just spawn every time the player has shot an enemy, and maybe make some disappear? Maybe they go back into the gate



}


public enum GameMode
{
    Waves,
    TimeTrial
}