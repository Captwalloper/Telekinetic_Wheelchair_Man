using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class OpenMenu : MonoBehaviour {
    public GameObject target;
    //
    private static SteamVR_TrackedController controller;

    void Start()
    {
        controller = controller ?? GetComponent<SteamVR_TrackedController>();
        controller.Gripped += OnGripped;
    }

    void OnDisable()
    {
        controller.TriggerClicked -= OnGripped;
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
