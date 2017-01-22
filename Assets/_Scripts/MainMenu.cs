using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    private Dictionary<string, Action> ActionToTake;
    //
    private MenuItem item;

    void OnEnable()
    {
        ActionToTake = new Dictionary<string, Action>()
        {
            { "Start", StartGame }
            , { "Quit", QuitGame }
        };

        item = GetComponent<MenuItem>();
        item.Activated += OnActivated;
    }

    void OnDisable()
    {
        item.Activated -= OnActivated;
    }

    private void OnActivated(string name)
    {
        Action action = ActionToTake[name];
        action();
    }

    private void StartGame()
    {
        return;
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
