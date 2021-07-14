using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    //[SerializeField] GameObject[] SceneMoveButtonsObj;
    //Button[] SceneMoveButtons;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //SceneMoveButtons = new Button[SceneMoveButtonsObj.Length];
        //for (int i = 0; i < SceneMoveButtonsObj.Length; i++)
        //{
        //    SceneMoveButtons[i] = SceneMoveButtonsObj[i].GetComponent<Button>();
        //}
    }
    public void ChangeInteractiveState()
    {
        //foreach (var item in SceneMoveButtons)
        //{
        //    item.interactable = false;
        //}
        canvasGroup.interactable = false;
    }
}
