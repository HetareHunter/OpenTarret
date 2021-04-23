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


    private void Start()
    {
        //timer = transform.GetComponent<GameTimer>();
        //gameStartButtonPosi = gameStartButton.transform.position;
        gameStart = gameStartUI.GetComponent<GameStartManager>();

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
                gameStart.ResetScreen();
                break;
            case GameState.Start:
                ScoreManager.Instance.ResetScore();
                break;
            case GameState.Play:
                insEnemies = Instantiate(Enemies, enemiesInsPosi.transform.position, Quaternion.identity);
                break;
            case GameState.End:
                insEnemies.transform.GetComponent<EnemiesDeathTime>().EnemiesDeath();
                gameStart.ActiveCollider(true);
                gameStart.ChangeAnim();
                gameStart.WriteScreenText("Finish!");
                break;
            default:
                break;
        }
    }

    
}
