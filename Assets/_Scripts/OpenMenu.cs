using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class OpenMenu : MonoBehaviour {
    public GameObject target;
    //
    private static SteamVR_TrackedController controller;

    private static bool reset = false;
    private void OnLevelWasLoaded(int level)
    {
        if (!reset)
        {
            controller = null;
        }
        reset = !reset;
    }

    void Start()
    {
        controller = controller ?? GetComponent<SteamVR_TrackedController>();
        controller.Gripped += OnGripped;
    }

    void OnDisable()
    {
        controller.Gripped -= OnGripped;
    }

    private void OnGripped(object sender, ClickedEventArgs e)
    {
        GotoMainMenu();
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
