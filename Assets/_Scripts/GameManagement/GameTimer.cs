using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameTimer : MonoBehaviour
{
    /// <summary>
    /// ゲーム時間単位は秒
    /// </summary>
    [SerializeField] float gameTime = 120.0f;
    float playNowTime = 0;
    [SerializeField] float idleTime = 3.0f;
    float idleNowTime = 0;
    [SerializeField] TextMeshProUGUI timeText;
    private void Start()
    {
        playNowTime = gameTime;
    }
    // Update is called once per frame
    void Update()
    {
        //if (count)
        //{
        //    PlayTimeCounter();
        //}
        switch (GameStateManager.Instance.gameState)
        {
            case GameState.Idle:
                break;
            case GameState.Start:
                break;
            case GameState.Play:
                PlayTimeCounter();
                break;
            case GameState.End:
                IdleTimeCounter();
                break;
            default:
                break;
        }
    }

    void PlayTimeCounter()
    {
        playNowTime -= Time.deltaTime;

        if (playNowTime <= 0)
        {
            //count = false;
            GameStateManager.Instance.ChangeGameState(GameState.End);
            playNowTime = gameTime;
        }
        timeText.text = "time:" + playNowTime.ToString("f2");
    }

    void IdleTimeCounter()
    {
        idleNowTime += Time.deltaTime;
        if (idleNowTime >= idleTime)
        {
            GameStateManager.Instance.ChangeGameState(GameState.Idle);
            idleNowTime = 0;
        }
    }
}
