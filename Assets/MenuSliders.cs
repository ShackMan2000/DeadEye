using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class MenuSliders : MonoBehaviour
{
    public float waves = 0f;
    public float enemies = 0f;
    public Text WaveText;
    public Text EnemyText;
   // public GameObject WaveSliderObject;
    public Slider waveSlider;
    public Slider EnemySlider;
   // public GameObject EnemySliderObject;

     
    public void Awake()
    {
        WaveText.text = "Waves: " + waveSlider.value;
        EnemyText.text = "Enemies: " + EnemySlider.value;
    }
    
  
    public void WaveUpdate()
    {   
        WaveText.text = "Waves: " + waveSlider.value;
    }
    public void EnemyUpdate()
    {
        EnemyText.text = "Enemies: " + EnemySlider.value;
    }

    

}