using System;
using UnityEngine;

public class MenuItem : MonoBehaviour {
    public event Action<string> Activated;
    //
    private const string menuItemTag = "MenuItem";
    //
    private static string nameOfSelected = null;
    private Color startColor;
    private static readonly Color hoverColor = Color.red;
    //
    private static SteamVR_LaserPointer pointer;
    private static SteamVR_TrackedController controller;

    private static bool reset = false;
    private void OnLevelWasLoaded(int level)
    {
        if (!reset)
        {
            pointer = null;
            controller = null;
            nameOfSelected = null;
        }
        reset = !reset;
    }

    void OnEnable()
    {
        startColor = GetComponent<Renderer>().material.color;

        pointer = pointer ?? GameObject.FindObjectOfType<SteamVR_LaserPointer>();
        pointer.PointerIn += OnPointerIn;
        pointer.PointerOut += OnPointerOut;

        controller = controller ?? GameObject.FindObjectOfType<SteamVR_TrackedController>();
        controller.TriggerClicked += OnTriggerPressed;
    }

    void OnDisable()
    {
        pointer.PointerIn -= OnPointerIn;
        pointer.PointerOut -= OnPointerOut;

        controller.TriggerClicked -= OnTriggerPressed;
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        if (!IsMenuItem(e.target))
        {
            return;
        }
        else
        {
            if (!TargetsScriptObject(e.target))
            {
                return;
            }
            else
            {
                Select(e.target.name);
            }
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        if (IsMenuItem(e.target) && TargetsScriptObject(e.target))
        {
            DeSelect(e.target.name);
        }
    }

    private bool triggerDebounced = false;
    private void OnTriggerPressed(object sender, ClickedEventArgs e)
    {
        triggerDebounced = !triggerDebounced;
        if (nameOfSelected != null && triggerDebounced) // A MenuItem is currently selected
        {
            Activated(nameOfSelected); // Fire event
        }
    }

    private bool IsMenuItem(Transform target)
    {
        return target.tag == menuItemTag;
    }

    private bool TargetsScriptObject(Transform target)
    {
        return target.name == gameObject.name;
    }

    private void Select(string name)
    {
        nameOfSelected = name;
        GetComponent<Renderer>().material.color = hoverColor;
    }

    private void DeSelect(string name)
    {
        nameOfSelected = null;
        GetComponent<Renderer>().material.color = startColor;
    }
}
