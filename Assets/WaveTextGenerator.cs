using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class WaveTextGenerator : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private UnityEvent waveStartEvent;
    [SerializeField] private UnityEvent waveCompleteEvent;
    [SerializeField] private DynamicTextData textData;
    [SerializeField] private Vector3 position;

    private void Start()
    {
       
    }

    public void StartWave(int waveNum)
    {
        
            DynamicTextManager.CreateText(position, "WAVE: " + waveNum.ToString(), textData);
           // waveText.text = "WAVE: " + waveNum.ToString();
        

        // Fire the wave start event
        waveStartEvent.Invoke();
    }

    public void CompleteWave()
    {
        // Fire the wave complete event
        waveCompleteEvent.Invoke();
    }
}
