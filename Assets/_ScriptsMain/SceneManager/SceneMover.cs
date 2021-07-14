using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneMover : MonoBehaviour
{
    //[Header("一緒にシーン遷移するオブジェクト")]
    //[SerializeField] GameObject[] objects;
    [SerializeField] GameObject centerEyeAnchor;
    [SerializeField] float fadeTime = 2.0f;
    OVRScreenFade screenFade;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //foreach (var item in objects)
        //{
        //    DontDestroyOnLoad(item);
        //}

        screenFade = centerEyeAnchor.GetComponent<OVRScreenFade>();
    }

    public void ToTutorial()
    {
        screenFade.SceneFadeOut("GaussShooter_Tutorial");
    }

    public void ToGame()
    {
        screenFade.SceneFadeOut("GaussShooter_Game");
    }

    public void ToTitle()
    {
        screenFade.SceneFadeOut("GaussShooter_TitleMenu");
    }
}
