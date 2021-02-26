using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] float deathTime = 4.0f;
    [SerializeField] GameObject gameStartButton;
    public void StartGame()
    {
        GameStateManager.Instance.ChangeGameState(GameState.Start);
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 1000));
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameStartButton, deathTime);
    }
}
