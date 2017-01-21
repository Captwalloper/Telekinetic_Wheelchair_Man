using System.Collections;
using UnityEngine;

public class MouseHover : MonoBehaviour {
    private Color startColor;
    private static readonly Color hoverColor = Color.red;

    void Start()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = hoverColor;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }
}
