using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] float deathTime = 4.0f;
    //[SerializeField] GameObject gameStartButton;

    public bool onLeftHand = false;
    public bool onRightHand = false;
    public bool onHand = false;

    /// <summary> 触れた時の振動の大きさ </summary>
    [SerializeField] float touchFrequeency = 0.3f;
    /// <summary> 触れた時の振動の周波数 </summary>
    [SerializeField] float touchAmplitude = 0.3f;

    float time = 0;
    [SerializeField] float startGameTime = 2.0f;

    [SerializeField] Image LoadUIImage;

    private void Update()
    {
        OnHandJudge();
        if (onHand)
        {
            LoadImage();
            TimeCount();
        }
    }

    //public void StartGame()
    //{
    //    GameStateManager.Instance.ChangeGameState(GameState.Start);
    //    this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 1000));
    //    GetComponent<BoxCollider>().enabled = false;
    //    Destroy(gameStartButton, deathTime);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RHand"))
        {
            onRightHand = true;
            VibrationExtension.Instance.VibrateController(startGameTime, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
        }
        else if (other.gameObject.CompareTag("LHand"))
        {
            onLeftHand = true;
            VibrationExtension.Instance.VibrateController(startGameTime, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
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
            time = 0; //時間の初期化
            LoadImage(); //UIの初期化
        }
    }

    void TimeCount()
    {
        time += Time.deltaTime;
        if (time > startGameTime)
        {
            time = 0;
            GameStateManager.Instance.ChangeGameState(GameState.Start); //ゲーム開始
            onRightHand = false;
            onLeftHand = false;
            gameObject.SetActive(false);
        }
    }

    void LoadImage()
    {
        //ロード画面の画像が丸になっていくことでロード時間の可視化をする
        LoadUIImage.fillAmount = time / startGameTime;
    }
}
