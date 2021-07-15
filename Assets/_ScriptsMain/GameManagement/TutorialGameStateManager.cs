using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum GameState
{
    None,
    Idle,
    Start,
    Play,
    End,
}
public class TutorialGameStateManager : MonoBehaviour ,IGameStateChangable
{
    public GameState gameState = GameState.None;
    [Inject]
    ISpawnable spawner;
    [SerializeField] GameObject gameStartUI;
    GameObject SceneMovePanel;
    MenuButtonSelecter MenuButtonSelecter;
    GameStartManager gameStart;
    GameTimer gameTimer;

    private void Start()
    {
        gameStart = gameStartUI.GetComponent<GameStartManager>();
        gameTimer = GetComponent<GameTimer>();
        if (SceneMovePanel == null)
        {
            SceneMovePanel = GameObject.Find("SceneMovePanel");
        }
        MenuButtonSelecter = SceneMovePanel.GetComponent<MenuButtonSelecter>();

        ChangeGameState(GameState.Idle);
    }

    public void ChangeGameState(GameState next)
    {
        //以前の状態を保持
        //var prev = gameState;
        //次の状態に変更する
        gameState = next;
        // ログを出す
        //Debug.Log($"ChangeState {prev} -> {next}");

        switch (gameState)
        {
            case GameState.None:
                break;
            case GameState.Idle:
                if (gameStart.ExistUIText())
                {
                    gameStart.ResetScreen();
                }
                break;
            case GameState.Start:
                ScoreManager.Instance.ResetScore();
                MenuButtonSelecter.ChangeInteractive(false);
                break;
            case GameState.Play:
                spawner.SpawnStart();
                gameTimer.CountStart();
                break;
            case GameState.End:
                spawner.SpawnEnd();
                gameTimer.CountEnd();
                gameStart.GameEnd();
                MenuButtonSelecter.ChangeInteractive(true);
                break;
            default:
                break;
        }
    }

    public string CurrentGameStateName()
    {
        return gameState.ToString();
    }


    #region
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.Idle:
                    ChangeGameState(GameState.Start);
                    break;
                case GameState.Start:
                    ChangeGameState(GameState.Play);
                    break;
                case GameState.Play:
                    ChangeGameState(GameState.End);
                    break;
                case GameState.End:
                    ChangeGameState(GameState.Idle);
                    break;
                default:
                    break;
            }
        }
    }

#endif
    #endregion
}


public interface IGameStateChangable
{
    public void ChangeGameState(GameState next);
    public string CurrentGameStateName();
}