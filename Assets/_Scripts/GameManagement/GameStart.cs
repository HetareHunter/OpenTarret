using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStart : MonoBehaviour
{
    public bool onLeftHand = false;
    public bool onRightHand = false;
    public bool onHand = false;
    bool onStart = false;

    /// <summary> 触れた時の振動の大きさ </summary>
    [SerializeField] float touchFrequeency = 0.3f;
    /// <summary> 触れた時の振動の周波数 </summary>
    [SerializeField] float touchAmplitude = 0.3f;

    float touchTime = 0;
    [SerializeField] float touchLimitTime = 2.0f;
    [SerializeField] Image TouchCountImage;

    float startCountTime = 1.0f;
    [SerializeField] int gameStartCount = 3;
    int startTimeCount;
    [SerializeField] Image[] startUIImage;
    [SerializeField] TextMeshProUGUI[] countText;

    Animator gameStartUIAnim;

    private void Start()
    {
        gameStartUIAnim = GetComponent<Animator>();
        startTimeCount = gameStartCount;
    }

    private void Update()
    {
        OnHandJudge();
        if (onHand)
        {
            LoadTouchImage();
            TouchTimeCount();
        }

        if (onStart)
        {
            StartTimeCount();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RHand"))
        {
            onRightHand = true;
            VibrationExtension.Instance.VibrateController(touchLimitTime, touchFrequeency, touchAmplitude, OVRInput.Controller.RTouch);
        }
        else if (other.gameObject.CompareTag("LHand"))
        {
            onLeftHand = true;
            VibrationExtension.Instance.VibrateController(touchLimitTime, touchFrequeency, touchAmplitude, OVRInput.Controller.LTouch);
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
            touchTime = 0; //時間の初期化
            LoadTouchImage(); //UIの初期化
        }
    }

    /// <summary>
    /// タッチパネルに触れてからゲーム開始確定までの時間をカウントする
    /// </summary>
    void TouchTimeCount()
    {
        touchTime += Time.deltaTime;
        if (touchTime > touchLimitTime)
        {
            touchTime = 0;
            //GameStateManager.Instance.ChangeGameState(GameState.Start); //ゲーム開始
            onRightHand = false;
            onLeftHand = false;
            ActiveCollider(false);
            onStart = true;
            ChangeAnim();
        }
    }

    void StartTimeCount()
    {
        startCountTime -= Time.deltaTime;
        LoadCountImage();

        if (startCountTime < 0)
        {
            startCountTime = 1;
            gameStartCount--;
            for (int i = 0; i < countText.Length; i++)
            {
                countText[i].text = gameStartCount.ToString();
            }
            

            if (gameStartCount <= 0)
            {
                GameStateManager.Instance.ChangeGameState(GameState.Start);
                gameStartCount = startTimeCount;
                for (int i = 0; i < countText.Length; i++)
                {
                    countText[i].text = "Start!";
                }
                
                onStart = false;
            }
        }
        
        

    }

    void LoadTouchImage()
    {
        //ロード画面の画像が丸になっていくことでロード時間の可視化をする
        TouchCountImage.fillAmount = touchTime / touchLimitTime;
    }

    void LoadCountImage()
    {
        //毎秒のイメージの変化
        for (int i = 0; i < startUIImage.Length; i++)
        {
            startUIImage[i].fillAmount = startCountTime / 1.0f;
        }
    }

    public void ActiveCollider(bool swicth)
    {
        GetComponent<BoxCollider>().enabled = swicth;
    }

    public void ChangeAnim()
    {
        gameStartUIAnim.SetTrigger("StateChange");
    }
}
