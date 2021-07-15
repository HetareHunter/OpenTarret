using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIActivater : MonoBehaviour
{
    GameObject SceneMovePanel;
    GameObject UIhelpers;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneMovePanel == null) 
        {
            SceneMovePanel = GameObject.Find("SceneMovePanel");
            SceneMovePanel.SetActive(false);
        }
        if (UIhelpers == null)
        {
            UIhelpers = GameObject.Find("UIHelpers");
        }
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

    void ActivateMenu()
    {
        if (!SceneMovePanel.activeSelf)
        {
            SceneMovePanel.SetActive(true);
            lineRenderer.enabled = true;
            StopGame();
        }
        else
        {
            SceneMovePanel.SetActive(false);
            lineRenderer.enabled = false;
            RebootGame();
        }
    }

    void StopGame()
    {
        Time.timeScale = 0;
    }
    void RebootGame()
    {
        Time.timeScale = 1;
    }
}
