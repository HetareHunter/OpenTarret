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

    float time = 0;
    [SerializeField] float startGameTime = 2.0f;

    [SerializeField] Image LoadUIImage;

    private void Update()
    {
        OnHandJudge();
        if (onHand)
        {
            LoadImage();
            StartCount();
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
        }
        else if (other.gameObject.CompareTag("LHand"))
        {
            onLeftHand = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RHand"))
        {
            onRightHand = false;
        }
        else if (other.gameObject.CompareTag("LHand"))
        {
            onLeftHand = false;
        }
    }

    void OnHandJudge()
    {
        if (onLeftHand || onRightHand)
        {
            onHand = true;
        }
    }

    void StartCount()
    {
        time += Time.deltaTime;
        if (time > startGameTime)
        {
            time = 0;
            GameStateManager.Instance.ChangeGameState(GameState.Start); //ゲーム開始
            onHand = false;
            gameObject.SetActive(false);
        }
    }

    void LoadImage()
    {
        //ロード画面の画像が０から１になっていくことでロード時間の可視化をする
        LoadUIImage.fillAmount = time / startGameTime;
    }
}
