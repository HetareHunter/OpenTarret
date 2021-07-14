using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSelecter : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void ChangeInteractiveState()
    {
        canvasGroup.interactable = false;
    }
}
