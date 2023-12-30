using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGunsToggle : MonoBehaviour
{
    [SerializeField] List<GameObject> guns;
    [SerializeField] List<GameObject> controllers;


    void OnEnable()
    {
        GameManager.OnExitShootingMode += ShowControllers;
        GameManager.OnEnterShootingMode += ShowGuns;
    }

    void OnDisable()
    {
        GameManager.OnExitShootingMode -= ShowControllers;
        GameManager.OnEnterShootingMode -= ShowGuns;
    }


    void Start()
    {
        ShowControllers();
    }

    void ShowControllers()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }

        foreach (GameObject controller in controllers)
        {
            controller.SetActive(true);
        }
    }


    public void ShowGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }

        foreach (GameObject controller in controllers)
        {
            controller.SetActive(false);
        }
    }
}