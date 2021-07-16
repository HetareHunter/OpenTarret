using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace MenuUI
{
    /// <summary>
    /// メニューUIの子のcanvasクラスが付いたオブジェクトの表示のオンオフを切り替えるクラス
    /// </summary>
    public class MenuUIActivater : MonoBehaviour
    {
        GameObject gameManager;
        TutorialGameStateManager TutorialGameStateManager;
        GameObject SceneMovePanel;
        [SerializeField] GameObject TutorialFinishPanel;
        GameObject UIhelpers;
        LineRenderer lineRenderer;
        // Start is called before the first frame update
        void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameObject.Find("GameManager");
                TutorialGameStateManager = gameManager.GetComponent<TutorialGameStateManager>();
            }
            if (SceneMovePanel == null)
            {
                SceneMovePanel = GameObject.Find("SceneMovePanel");
                SceneMovePanel.SetActive(false);
            }
            if (UIhelpers == null)
            {
                UIhelpers = GameObject.Find("MyUIHelpers");
            }
            //if (SceneManager.GetActiveScene().name == "GaussShooter_Tutorial")
            //{
            //    TutorialFinishPanel = GameObject.Find("TutorialFinishUIPanal");
            //}
            lineRenderer = UIhelpers.GetComponentInChildren<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Start))
            {
                ActivateMenu();
            }
        }

        public void ActivateMenu()
        {
            if (!SceneMovePanel.activeSelf)
            {
                SceneMovePanel.SetActive(true);
                lineRenderer.enabled = true;
                TutorialGameStateManager.StopGame();
            }
            else
            {
                SceneMovePanel.SetActive(false);
                lineRenderer.enabled = false;
                TutorialGameStateManager.RebootGame();
            }
        }

        public void ActivateTutorialFinishPanel()
        {
            if (!TutorialFinishPanel.activeSelf)
            {
                TutorialFinishPanel.SetActive(true);
            }
            else
            {
                TutorialFinishPanel.SetActive(false);
            }
        }

        //void StopGame()
        //{
        //    Time.timeScale = 0;
        //}
        //void RebootGame()
        //{
        //    Time.timeScale = 1;
        //}
    }
}