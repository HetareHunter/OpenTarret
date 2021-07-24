using System.Collections;
using System.Collections.Generic;
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
            if (tarret.GetComponent<TarretVitalManager>())
            {
                tarretVitalManager = tarret.GetComponent<TarretVitalManager>();
            }
            guardWallManager = guardWalls.GetComponent<GuardWallManager>();

            ChangeGameState(GameState.Idle);
        }

        public void ChangeGameState(GameState next)
        {
            //�ȑO�̏�Ԃ�ێ�
            //var prev = gameState;
            //���̏�ԂɕύX����
            gameState = next;
            // ���O���o��
            //Debug.Log($"ChangeState {prev} -> {next}");

            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.Idle:
                    spawner.ResetEnemies();
                    if (gameStart.ExistUIText())
                    {
                        gameStart.ResetScreen();
                    }
                    MenuButtonSelecter.IdleInteractive();
                    tarretVitalManager.ResetTarretVital();
                    guardWallManager.ResetAllGuardWalls();
                    invaderMoveCommander.CommenceReset();
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
        /// �Q�[�����I��点��Ƃ��ɌĂяo�������B�����̏ꍇ�A�����̏ꍇ�ł��ꂼ��Ăяo���֐��𕪂���
        /// </summary>
        /// <param name="win"></param>
        public void FinishGame(bool win)
        {
            if (win == true)
            {
                Debug.Log("����!");
            }
            else
            {
                Debug.Log("����!");
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