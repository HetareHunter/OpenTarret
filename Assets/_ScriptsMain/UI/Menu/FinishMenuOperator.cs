using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMenuOperator : MonoBehaviour,IMenuOperatable
{
    CanvasGroup canvasGroup;
    [SerializeField] GameObject basePanel;
    private void Awake()
    {
        canvasGroup = basePanel.GetComponent<CanvasGroup>();
        CloseMenu();
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void ActiveBlueScreenButton()
    {
        canvasGroup.interactable = true;
    }
}
