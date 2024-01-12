using System;
using System.Collections;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField] SelectableOption flickerOption;

    public GameObject curtain;
    public bool isFlickering = false;
    bool isRunningTest;

    float timeSinceLastSwitch;
    float targetInterval;


    // could also add a test button? will enable it for like 3 seconds via couroutine

    // use test flicker, be remarkable


    // common testing is easy, just have to account for the scenario where test runs and game starts. Safest would be independent

    void OnEnable()
    {
        GameManager.OnEnterShootingMode += StartFlicker;
        GameManager.OnExitShootingMode += StopFlicker;
    }

    void OnDisable()
    {
        GameManager.OnEnterShootingMode -= StartFlicker;
        GameManager.OnExitShootingMode -= StopFlicker;
    }


    void StartFlicker()
    {
        int flickerFraction = flickerOption.Options[flickerOption.SelectedIndex];

        if (flickerFraction == 0)
        {
            return;
        }

        isFlickering = true;
        targetInterval = 1f / flickerOption.Options[flickerOption.SelectedIndex];
        timeSinceLastSwitch = 0;
    }

    public void TestFlicker()
    {
        if (isRunningTest || flickerOption.Options[flickerOption.SelectedIndex] == 0)
        {
            return;
        }

        StartCoroutine(TestFlickerRoutine());
    }


    IEnumerator TestFlickerRoutine()
    {
        targetInterval = 1f / flickerOption.Options[flickerOption.SelectedIndex];
        timeSinceLastSwitch = 0;

        isRunningTest = true;
        yield return new WaitForSeconds(2f);
        isRunningTest = false;
        if(!isFlickering)
        {
            curtain.SetActive(false);
        }
    }


    void StopFlicker()
    {
        isFlickering = false;
        curtain.SetActive(false);
    }

    // the problem is that test flicker will use a coroutine

    void Update()
    {
        if (isFlickering || isRunningTest)
        {
            timeSinceLastSwitch += Time.deltaTime;

            if (timeSinceLastSwitch >= targetInterval)
            {
                curtain.SetActive(!curtain.activeSelf);
                timeSinceLastSwitch = 0;
            }
        }
    }
}