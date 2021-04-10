using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] float deathTime = 4.0f;
    [SerializeField] GameObject gameStartButton;

    bool onLeftHand = false;
    bool onRightHand = false;
    public bool onHand = false;

    private void Update()
    {
        OnHandJudge();
    }

    public void StartGame()
    {
        GameStateManager.Instance.ChangeGameState(GameState.Start);
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 1000));
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameStartButton, deathTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RHand"))
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
}
