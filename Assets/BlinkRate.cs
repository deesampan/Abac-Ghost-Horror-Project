using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkRate : MonoBehaviour
{
    public Image redDot;
    public float blinkRate = 0.5f;
    private bool isVisible = true;

    void Start()
    {
        if (redDot == null)
        {
            Debug.LogError("Assign the red dot image in the inspector!");
            return;
        }

        InvokeRepeating(nameof(ToggleDot), blinkRate, blinkRate);
    }

    void ToggleDot()
    {
        isVisible = !isVisible;
        redDot.enabled = isVisible;
    }
}
