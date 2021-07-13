using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneMover : MonoBehaviour
{
    [Header("一緒にシーン遷移するオブジェクト")]
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject centerEyeAnchor;
    [SerializeField] float fadeTime = 2.0f;
    OVRScreenFade screenFade;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach (var item in objects)
        {
            DontDestroyOnLoad(item);
        }

        screenFade = centerEyeAnchor.GetComponent<OVRScreenFade>();
    }

    public void ToTutorial()
    {
        screenFade.FadeOut();
        SceneManager.LoadScene("GaussShooter_Tutorial");
    }

    public void ToGame()
    {
        screenFade.FadeOut();
        SceneManager.LoadScene("GaussShooter_Game");
    }

    public void ToTitle()
    {
        screenFade.FadeOut();
        SceneManager.LoadScene("GaussShooter_TitleMenu");
    }
}
