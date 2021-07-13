using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonOparator : MonoBehaviour
{
    [SerializeField] GameObject finishUI;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }
    public void ActiveFinishUI()
    {
        finishUI.SetActive(true);
    }

    public void DisableButton()
    {
        canvasGroup.interactable = false;
    }
}
