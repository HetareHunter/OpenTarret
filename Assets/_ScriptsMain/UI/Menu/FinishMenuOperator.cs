using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMenuOperator : MonoBehaviour,IMenuOperatable
{
    CanvasGroup canvasGroup;
    [SerializeField] GameObject blueScreen;
    private void Awake()
    {
        canvasGroup = blueScreen.GetComponent<CanvasGroup>();
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
