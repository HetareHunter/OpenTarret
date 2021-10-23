using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MenuUI;
using Tarret;

public enum GameState
{
    None,
    Idle,
    Start,
    Play,
    End,
    Result
}

namespace Manager
{
    public class TutorialGameStateManager : MonoBehaviour, IGameStateChangable
    {
        public GameState gameState = GameState.None;
        [Inject]
        ISpawnable spawner;
        [SerializeField] GameObject gameStartUI;
        [SerializeField] GameObject tarret;
        GameObject SceneMovePanel;
        MenuButtonSelecter MenuButtonSelecter;
        GameStartManager gameStart;
        GameTimer gameTimer;
        BaseTarretAttackManager baseTarretAttackManager;

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

            baseTarretAttackManager = tarret.GetComponent<BaseTarretAttackManager>();
        }

        public void ChangeGameState(GameState next)
        {
            //以前の状態を保持
            var prev = gameState;
            //次の状態に変更する
            gameState = next;
            // ログを出す
            //Debug.Log($"ChangeState {prev} -> {next}");
            if (next == GameState.End && prev != GameState.Play) return;

            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.Idle:
                    gameStart.Reset();
                    MenuButtonSelecter.IdleInteractive();
                    break;
                case GameState.Start:
                    ScoreManager.Instance.ResetScore();
                    MenuButtonSelecter.AllChangeInteractive(false);
                    spawner.SpawnStart();

                    baseTarretAttackManager.IsAttackable(false);
                    break;
                case GameState.Play:
                    gameTimer.CountStart();
                    MenuButtonSelecter.AllChangeInteractive(true);
                    MenuButtonSelecter.GamePlayInteractive(true);

                    baseTarretAttackManager.IsAttackable(true);
                    break;
                case GameState.End:
                    spawner.SpawnEnd();
                    gameTimer.CountEnd();
                    gameStart.GameEnd();
                    MenuButtonSelecter.GamePlayInteractive(false);
                    break;
                default:
                    break;
            }
        }

        public string CurrentGameStateName()
        {
            return gameState.ToString();
        }

        public void StopGame()
        {
            Time.timeScale = 0;
        }
        public void RebootGame()
        {
            Time.timeScale = 1;
        }

        public void FinishGame(bool win)
        {

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
                        //ChangeGameState(GameState.Start);
                        gameStart.GameStart();
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
}

public interface IGameStateChangable
{
    public void ChangeGameState(GameState next);
    public string CurrentGameStateName();
    public void FinishGame(bool win);
}