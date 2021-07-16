using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameTimer : MonoBehaviour
{
    IGameStateChangable gameStateChangeable;
    /// <summary>
    /// ゲーム時間単位は秒
    /// </summary>
    [SerializeField] float gameTime = 30.0f;
    float playNowTime = 0;
    [SerializeField] float idleTime = 4.0f;
    float idleNowTime = 0;
    [SerializeField] TextMeshProUGUI timeText;
    bool timeStart = false;
    bool gameEnd = false;
    private void Start()
    {
        playNowTime = gameTime;
        gameStateChangeable = GetComponent<IGameStateChangable>();
    }
    // Update is called once per frame
    void Update()
    {
        if (timeStart)
        {
            PlayTimeCounter();
        }
        else if (gameEnd)
        {
            IdleTimeCounter();
        }
    }

    public void CountStart()
    {
        timeStart = true;
    }

    public void CountEnd()
    {
        timeStart = false;
        gameEnd = true;
        ResetTimer();
    }
    void PlayTimeCounter()
    {
        playNowTime -= Time.deltaTime;

        if (playNowTime <= 0)
        {
            gameStateChangeable.ChangeGameState(GameState.End);
        }
        timeText.text = "time:" + playNowTime.ToString("f2");
    }

    public void ResetTimer()
    {
        playNowTime = gameTime;
        timeText.text = "time:" + playNowTime.ToString("f2");
    }

    void IdleTimeCounter()
    {
        idleNowTime += Time.deltaTime;
        if (idleNowTime >= idleTime)
        {
            gameStateChangeable.ChangeGameState(GameState.Idle);
            gameEnd = false;
            idleNowTime = 0;
        }
    }
}
