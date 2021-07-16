using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuUI
{
    public class SceneMover : MonoBehaviour
    {
        GameObject gameManager;
        IGameStateChangable gameStateChangeable;
        [SerializeField] GameObject centerEyeAnchor;
        OVRScreenFade screenFade;
        // Start is called before the first frame update
        void Start()
        {
            screenFade = centerEyeAnchor.GetComponent<OVRScreenFade>();
            gameManager = GameObject.Find("GameManager");
            if (gameManager != null)
            {
                gameStateChangeable = gameManager.GetComponent<IGameStateChangable>();
            }
        }

        public void ToTutorial()
        {
            gameStateChangeable.ChangeGameState(GameState.End);
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_Tutorial");
        }

        public void ToGame()
        {
            gameStateChangeable.ChangeGameState(GameState.End);
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_Game");
        }

        public void ToTitle()
        {
            gameStateChangeable.ChangeGameState(GameState.End);
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_TitleMenu");
        }

        void TimeScaleChanger(float time)
        {
            Time.timeScale = time;
        }
    }
}