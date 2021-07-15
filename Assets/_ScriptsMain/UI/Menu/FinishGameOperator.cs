using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameOperator : MonoBehaviour
{
    TutorialGameStateManager stateManager;

    public void GameEnd()
    {
        stateManager.ChangeGameState(GameState.End);
    }
}
