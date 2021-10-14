﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Manager
{
    /// <summary>
    /// ゲームスタートをするまでの時間管理を行うクラス
    /// ゲーム開始までに、手でGameStartCanvasに触れてgameStateがStartになるまでの時間と
    /// gameStateがStartになってからPlayになるまでの時間の2つの時間を管理する
    /// </summary>
    public class GameStartManager : MonoBehaviour
    {
        GameObject gameManager;
        [SerializeField] GameObject spawnerManager;
        IAppearable appearable;
        IGameStateChangable gameStateChangeable;
        bool onLeftHand = false;
        bool onRightHand = false;
        bool onHand = false;

        /// <summary>
        /// ゲームのスタートが確定するフラグ
        /// </summary>
        bool onStart = false;

        /// <summary> 触れた時の振動の大きさ </summary>
        [SerializeField] float touchFrequeency = 0.3f;
        /// <summary> 触れた時の振動の周波数 </summary>
        [SerializeField] float touchAmplitude = 0.3f;

        float toStartTime = 0;
        [SerializeField] float toStartLimitTime = 2.0f;
        [SerializeField] Image TouchCountImage;

        /// <summary>
        /// 1秒固定、1秒ごとにUIのカウントを３，２，１と変化させたいので変数を二つ用意した
        /// </summary>
        float toPlayTime = 1.0f;

        /// <summary>
        /// 一秒を何回カウントするか
        /// </summary>
        [SerializeField] int toPlayTimeCountNum = 3;
        int beginingToPlayTimeCountNum;

        [SerializeField] Image[] startUIImage;
        [SerializeField] TextMeshProUGUI[] countText;//複数のUIに対応するための配列

        Animator gameStartUIAnim;

        [SerializeField] GameObject changeColorObj;
        ColorManager colorManager;
        BoxCollider boxCollider;

        private void Start()
        {
            gameStartUIAnim = GetComponent<Animator>();
            beginingToPlayTimeCountNum = toPlayTimeCountNum;
            colorManager = changeColorObj.GetComponent<ColorManager>();
            gameManager = GameObject.Find("GameManager");
            gameStateChangeable = gameManager.GetComponent<IGameStateChangable>();
            boxCollider = GetComponent<BoxCollider>();
            appearable = spawnerManager.GetComponent<IAppearable>();
        }

        private void Update()
        {
            OnHandJudge();
            if (onHand)
            {
                LoadTouchImage();
                ToStartCount();
            }

            if (onStart)
            {
                if (appearable != null)
                {
                    if (appearable.FinishAppear) 
                    { 
                        ToPlayCount();
                    }
                }
                else
                {
                    ToPlayCount();
                }
            }
        }

        public void Reset()
        {
            ResetScreen();
            ActivateGameStart(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RHand"))
            {
                onRightHand = true;
                VibrationExtension.Instance.VibrateController(toStartLimitTime, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
            }
            else if (other.gameObject.CompareTag("LHand"))
            {
                onLeftHand = true;
                VibrationExtension.Instance.VibrateController(toStartLimitTime, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("RHand"))
            {
                onRightHand = false;
                VibrationExtension.Instance.VibrateStop(OVRInput.Controller.RTouch);
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            }
            else if (other.gameObject.CompareTag("LHand"))
            {
                onLeftHand = false;
                VibrationExtension.Instance.VibrateStop(OVRInput.Controller.LTouch);
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            }
        }

        void OnHandJudge()
        {
            if (onLeftHand || onRightHand)
            {
                onHand = true;
            }
            else
            {
                onHand = false;
                toStartTime = 0; //時間の初期化
                LoadTouchImage(); //UIの初期化
            }
        }

        /// <summary>
        /// タッチパネルに触れてからゲーム開始確定までの時間をカウントする
        /// GameStateの Idle->Start の段階
        /// </summary>
        void ToStartCount()
        {
            toStartTime += Time.deltaTime;
            if (toStartTime > toStartLimitTime)
            {
                toStartTime = 0;
                GameStart();
            }
        }

        /// <summary>
        /// GameStateの Start->Play の段階
        /// </summary>
        void ToPlayCount()
        {
            toPlayTime -= Time.deltaTime;
            LoadCountImage();

            if (toPlayTime < 0)
            {
                toPlayTime = 1;
                toPlayTimeCountNum--;
                for (int i = 0; i < countText.Length; i++)
                {
                    if (!ExistUIText()) continue;
                    countText[i].text = toPlayTimeCountNum.ToString();
                }

                if (toPlayTimeCountNum <= 0)
                {
                    gameStateChangeable.ChangeGameState(GameState.Play);
                    colorManager.ToStartColor();
                    toPlayTimeCountNum = beginingToPlayTimeCountNum;

                    WriteScreenText("Start!");

                    onStart = false;
                }
            }
        }

        public void ResetScreen()
        {
            WriteScreenText("Ready?");
            LoadCountImage();
            ActivateGameStart(true);
        }

        public void WriteScreenText(string input)
        {
            if (!ExistUIText()) return;

            for (int i = 0; i < countText.Length; i++)//登録したUIテキストの全てに変更を加える
            {
                countText[i].text = input;
            }
        }

        void LoadTouchImage()
        {
            //ロード画面の画像が丸になっていくことでロード時間の可視化をする
            TouchCountImage.fillAmount = toStartTime / toStartLimitTime;
        }

        void LoadCountImage()
        {
            if (!ExistUIText()) return;
            //毎秒のイメージの変化
            for (int i = 0; i < startUIImage.Length; i++)
            {
                startUIImage[i].fillAmount = toPlayTime / 1.0f;
            }
        }

        public void ChangeAnim()
        {
            gameStartUIAnim.SetTrigger("StateChange");
        }

        public bool ExistUIText()
        {
            if (countText.Length > 0)
            {
                if (countText[0] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void GameStart()
        {
            gameStateChangeable.ChangeGameState(GameState.Start); //ゲーム開始
            WriteScreenText(toPlayTimeCountNum.ToString());
            onRightHand = false;
            onLeftHand = false;
            boxCollider.enabled = false;
            onStart = true;
            colorManager.ToChangeColor();
            ChangeAnim();
            if (appearable != null)
            {
                appearable.StartSpawn();
            }
        }

        public void GameEnd()
        {
            ChangeAnim();
            WriteScreenText("Finish!");
        }

        void ActivateGameStart(bool isActive)
        {
            boxCollider.enabled = isActive;
        }
    }
}