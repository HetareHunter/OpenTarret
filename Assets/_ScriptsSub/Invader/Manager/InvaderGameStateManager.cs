//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using Zenject;
//using MenuUI;
//using Enemy;
//using Tarret;
//using UniRx;

//namespace Manager
//{
//    public class InvaderGameStateManager : MonoBehaviour, IGameStateChangable
//    {
//        public GameStateType gameState = GameStateType.None;
//        [Inject]
//        ISpawnable spawner;
//        ITarretState tarretState;
//        [SerializeField] GameObject invaders;
//        InvaderMoveCommander invaderMoveCommander;
//        [SerializeField] GameObject gameStartUI;
//        GameObject SceneMovePanel;
//        MenuButtonSelecter MenuButtonSelecter;
//        GameStartManager gameStart;
//        GameTimer gameTimer;
//        [SerializeField] GameObject tarret;
//        TarretVitalManager tarretVitalManager;
//        [SerializeField] GameObject guardWalls;
//        GuardWallManager guardWallManager;

//        [SerializeField] GameObject winPanel;
//        [SerializeField] GameObject losePanel;

//        [SerializeField] float toGameEndStateTimer = 300;
//        IDisposable gameEndObserber;

//        private void Start()
//        {
//            gameStart = gameStartUI.GetComponent<GameStartManager>();
//            gameTimer = GetComponent<GameTimer>();
//            tarretState = tarret.GetComponent<ITarretState>();

//            invaderMoveCommander = invaders.GetComponent<InvaderMoveCommander>();
//            if (SceneMovePanel == null)
//            {
//                SceneMovePanel = GameObject.Find("SceneMovePanel");
//            }
//            MenuButtonSelecter = SceneMovePanel.GetComponent<MenuButtonSelecter>();
//            if (tarret.GetComponent<TarretVitalManager>())
//            {
//                tarretVitalManager = tarret.GetComponent<TarretVitalManager>();
//            }
//            guardWallManager = guardWalls.GetComponent<GuardWallManager>();

//            ChangeGameState(GameStateType.Idle);

//        }

//        public void ChangeGameState(GameStateType next)
//        {
//            //???O????????????
//            //var prev = gameState;
//            //?????????????X????
//            gameState = next;
//            // ???O???o??
//            //Debug.Log($"ChangeState {prev} -> {next}");

//            switch (gameState)
//            {
//                case GameStateType.None:
//                    break;
//                case GameStateType.Idle:
//                    spawner.Reset();
//                    gameStart.Reset();
//                    tarretState.ChangeTarretState(TarretState.Idle);
//                    MenuButtonSelecter.IdleInteractive();
//                    tarretVitalManager.ResetTarretVital();
//                    guardWallManager.ResetAllGuardWalls();
//                    invaderMoveCommander.CommenceReset();

//                    winPanel.SetActive(false);
//                    losePanel.SetActive(false);
//                    break;
//                case GameStateType.Start:
//                    ScoreManager.Instance.ResetScore();
//                    MenuButtonSelecter.AllChangeInteractive(false);
//                    break;
//                case GameStateType.Play:
//                    spawner.SpawnStart();
//                    //gameTimer.CountStart();
//                    MenuButtonSelecter.AllChangeInteractive(true);
//                    MenuButtonSelecter.GamePlayInteractive(true);
//                    invaderMoveCommander.CommenceMarch();
//                    break;
//                case GameStateType.End:
//                    gameTimer.CountEnd();
//                    gameStart.GameEnd();
//                    MenuButtonSelecter.GamePlayInteractive(false);
//                    break;
//                default:
//                    break;
//            }
//        }

//        public string CurrentGameStateName()
//        {
//            return gameState.ToString();
//        }

//        public void StopGame()
//        {
//            Time.timeScale = 0;
//        }
//        public void RebootGame()
//        {
//            Time.timeScale = 1;
//        }

//        /// <summary>
//        /// ?Q?[?????I???????????????????o???????B???????????A?????????????????????????o??????????????
//        /// </summary>
//        /// <param name="win"></param>
//        public void FinishGame(bool win)
//        {
//            if (gameEndObserber != null)
//            {
//                gameEndObserber.Dispose();
//            }
//            if (gameState != GameStateType.End)
//            {
//                if (win == true)
//                {
//                    Debug.Log("????!");
//                    winPanel.SetActive(true);
//                }
//                else
//                {
//                    Debug.Log("????!");
//                    losePanel.SetActive(true);
//                }
//                gameEndObserber = Observable.Timer(TimeSpan.FromMilliseconds(toGameEndStateTimer))
//                    .Subscribe(_ => ChangeGameState(GameStateType.End)).AddTo(this);
//            }
//        }

//        /// <summary>
//        /// UI???{?^???????????Q?[?????????I??????????
//        /// </summary>
//        public void DrawGame()
//        {
//            ChangeGameState(GameStateType.End);
//        }

//        #region
//#if UNITY_EDITOR
//        private void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.Z))
//            {
//                switch (gameState)
//                {
//                    case GameStateType.None:
//                        break;
//                    case GameStateType.Idle:
//                        ChangeGameState(GameStateType.Start);
//                        break;
//                    case GameStateType.Start:
//                        ChangeGameState(GameStateType.Play);
//                        break;
//                    case GameStateType.Play:
//                        ChangeGameState(GameStateType.End);
//                        break;
//                    case GameStateType.End:
//                        ChangeGameState(GameStateType.Idle);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//#endif
//        #endregion
//    }
//}