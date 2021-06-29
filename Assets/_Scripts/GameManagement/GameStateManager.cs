using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    None,
    Idle,
    Start,
    Play,
    End,
}
public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{
    public GameState gameState = GameState.None;
    //[SerializeField] GameObject gameStartButton;
    //Vector3 gameStartButtonPosi;
    [SerializeField] GameObject Enemies;
    GameObject insEnemies;
    [SerializeField] GameObject enemiesInsPosi;
    //GameTimer timer;
    [SerializeField] GameObject gameStartUI;
    GameStartManager gameStart;
    GameTimer gameTimer;


    private void Start()
    {
        //timer = transform.GetComponent<GameTimer>();
        //gameStartButtonPosi = gameStartButton.transform.position;
        gameStart = gameStartUI.GetComponent<GameStartManager>();
        gameTimer = GetComponent<GameTimer>();

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
                break;
            case GameState.Play:
                SpawnerManager.Instance.SpawnStart();
                gameTimer.CountStart();
                break;
            case GameState.End:
                SpawnerManager.Instance.SpawnEnd();
                gameTimer.CountEnd();
                gameStart.GameEnd();
                break;
            default:
                break;
        }
    }
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

}
