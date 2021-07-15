using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSelecter : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] GameObject gameManager;
    
    IGameStateChangable gameStateChangable;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            gameStateChangable = gameManager.GetComponent<IGameStateChangable>();
        }
        
    }

    //private void OnEnable()
    //{
    //    if (gameStateChangable.CurrentGameStateName() == GameState.Play.ToString())
    //    {
    //        ChangeInteractive(false);
    //    }
    //}

    public void ChangeInteractiveAfterPushedButton()
    {
        canvasGroup.interactable = false;
    }

    public void ChangeInteractive(bool activate)
    {
        canvasGroup.interactable = activate;
        
    }
}
