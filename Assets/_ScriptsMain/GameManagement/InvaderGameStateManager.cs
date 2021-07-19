using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MenuUI;
using Enemy;

namespace Manager
{
    public class InvaderGameStateManager : MonoBehaviour, IGameStateChangable
    {
        public GameState gameState = GameState.None;
        [Inject]
        ISpawnable spawner;
        [SerializeField] GameObject invaders;
        InvaderMoveCommander invaderMoveCommander;
        [SerializeField] GameObject gameStartUI;
        GameObject SceneMovePanel;
        MenuButtonSelecter MenuButtonSelecter;
        GameStartManager gameStart;
        GameTimer gameTimer;
        bool isWin = false;

        private void Start()
        {
            gameStart = gameStartUI.GetComponent<GameStartManager>();
            gameTimer = GetComponent<GameTimer>();

            invaderMoveCommander = invaders.GetComponent<InvaderMoveCommander>();
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
                    isWin = false;
                    spawner.ResetEnemies();
                    if (gameStart.ExistUIText())
                    {
                        gameStart.ResetScreen();
                    }
                    MenuButtonSelecter.IdleInteractive();
                    break;
                case GameState.Start:
                    ScoreManager.Instance.ResetScore();
                    MenuButtonSelecter.AllChangeInteractive(false);
                    break;
                case GameState.Play:
                    spawner.SpawnStart();
                    //gameTimer.CountStart();
                    MenuButtonSelecter.AllChangeInteractive(true);
                    MenuButtonSelecter.GamePlayInteractive(true);
                    invaderMoveCommander.CommenceMarch();
                    break;
                case GameState.End:
                    spawner.SpawnEnd();
                    gameTimer.CountEnd();
                    gameStart.GameEnd();
                    MenuButtonSelecter.GamePlayInteractive(false);
                    invaderMoveCommander.CommenceStandby();
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

        /// <summary>
        /// ゲームを終わらせるときに呼び出す処理。勝ちの場合、負けの場合でそれぞれ呼び出す関数を分ける
        /// </summary>
        /// <param name="win"></param>
        public void FinishGame(bool win)
        {
            if (win == true)
            {
                Debug.Log("勝ち!");
            }
            else
            {
                Debug.Log("負け!");
            }
            ChangeGameState(GameState.End);
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
}