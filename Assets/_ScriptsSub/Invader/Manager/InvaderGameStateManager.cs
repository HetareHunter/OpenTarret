using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using MenuUI;
using Enemy;
using Tarret;
using UniRx;

namespace Manager
{
    public class InvaderGameStateManager : MonoBehaviour, IGameStateChangable
    {
        public GameState gameState = GameState.None;
        [Inject]
        ISpawnable spawner;
        ITarretState tarretState;
        [SerializeField] GameObject invaders;
        InvaderMoveCommander invaderMoveCommander;
        [SerializeField] GameObject gameStartUI;
        GameObject SceneMovePanel;
        MenuButtonSelecter MenuButtonSelecter;
        GameStartManager gameStart;
        GameTimer gameTimer;
        [SerializeField] GameObject tarret;
        TarretVitalManager tarretVitalManager;
        [SerializeField] GameObject guardWalls;
        GuardWallManager guardWallManager;

        [SerializeField] GameObject winPanel;
        [SerializeField] GameObject losePanel;

        [SerializeField] float toGameEndStateTimer = 300;
        IDisposable gameEndObserber;

        private void Start()
        {
            gameStart = gameStartUI.GetComponent<GameStartManager>();
            gameTimer = GetComponent<GameTimer>();
            tarretState = tarret.GetComponent<ITarretState>();

            invaderMoveCommander = invaders.GetComponent<InvaderMoveCommander>();
            if (SceneMovePanel == null)
            {
                SceneMovePanel = GameObject.Find("SceneMovePanel");
            }
            MenuButtonSelecter = SceneMovePanel.GetComponent<MenuButtonSelecter>();
            if (tarret.GetComponent<TarretVitalManager>())
            {
                tarretVitalManager = tarret.GetComponent<TarretVitalManager>();
            }
            guardWallManager = guardWalls.GetComponent<GuardWallManager>();

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
                    spawner.Reset();
                    gameStart.Reset();
                    tarretState.ChangeTarretState(TarretState.Idle);
                    MenuButtonSelecter.IdleInteractive();
                    tarretVitalManager.ResetTarretVital();
                    guardWallManager.ResetAllGuardWalls();
                    invaderMoveCommander.CommenceReset();

                    winPanel.SetActive(false);
                    losePanel.SetActive(false);
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

        /// <summary>
        /// ゲームを終わらせるときに呼び出す処理。勝ちの場合、負けの場合でそれぞれ呼び出す関数を分ける
        /// </summary>
        /// <param name="win"></param>
        public void FinishGame(bool win)
        {
            if (gameEndObserber != null)
            {
                gameEndObserber.Dispose();
            }
            if (gameState != GameState.End)
            {
                if (win == true)
                {
                    Debug.Log("勝ち!");
                    winPanel.SetActive(true);
                }
                else
                {
                    Debug.Log("負け!");
                    losePanel.SetActive(true);
                }
                gameEndObserber = Observable.Timer(TimeSpan.FromMilliseconds(toGameEndStateTimer))
                    .Subscribe(_ => ChangeGameState(GameState.End)).AddTo(this);
            }
        }

        /// <summary>
        /// UIのボタンによってゲームを強制終了する場合
        /// </summary>
        public void DrawGame()
        {
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