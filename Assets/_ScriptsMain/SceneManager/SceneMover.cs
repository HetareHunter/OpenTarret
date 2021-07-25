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
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_Tutorial");
        }

        public void ToGame()
        {
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_InvaderGame");
        }

        public void ToTitle()
        {
            TimeScaleChanger(1.0f);
            screenFade.SceneFadeOut("GaussShooter_TitleMenu");
        }

        void TimeScaleChanger(float time)
        {
            Time.timeScale = time;
        }

        void GameStateChange()
        {
            if (gameStateChangeable != null)
            {
                gameStateChangeable.ChangeGameState(GameState.End);
            }
        }
    }
}