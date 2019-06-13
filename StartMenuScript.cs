using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartMenuScript : MonoBehaviour {

    public string scene_to_load;
    public string level_Select;
    public Button NewGameButton, LevelSelectButton, OptionsButton, QuitAppbutton, BackButton;

    public GameObject TitleScreen, TitleOptionsMenu;

    // Use this for initialization
    void Start()
    {

        TitleOptionsMenu = GameObject.Find("OptionsPanel");
        TitleScreen = GameObject.Find("MainMenuPanel");

        scene_to_load = "Game Map - Prototype";
        level_Select = "levelSelectMenu";
        NewGameButton.onClick.AddListener(delegate { NewGameOnclick(scene_to_load); });
        LevelSelectButton.onClick.AddListener(delegate {LevelSelectOnClick(level_Select); });
        OptionsButton.onClick.AddListener(OptionsMenuOnClick);
        QuitAppbutton.onClick.AddListener(QuitApplicaitonOnClick);

        BackButton.onClick.AddListener(BackToMenuOnClick);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonUp("NewGameButton"))
        {
            NewGameOnclick("Game Map - Prototype");
        }
        if (Input.GetButtonUp("LevelSelectButton"))
        {
            LevelSelectOnClick("levelSelectMenu");
        }
        if (Input.GetButtonUp("OptionsButton"))
        {
            OptionsMenuOnClick();
        }
        if (Input.GetButtonUp("QuitAppButton"))
        {
            QuitApplicaitonOnClick();
        }
        if (Input.GetButtonUp("BackButton"))
        {
            BackToMenuOnClick();
        }
    }

    /// <summary>
    /// function to load up a new game from the start menu via path
    /// </summary>
    /// <parameters> none </parameters>
    /// <return> void </return>
    public void NewGameOnclick(string scene_to_load)
    {
        SceneManager.LoadScene(scene_to_load, LoadSceneMode.Single);
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <parameters> none </parameters>
    /// <return> void </return>
    public void QuitApplicaitonOnClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();

        #endif   
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <parameters> none </parameters>
    /// <return> void </return>
    public void LevelSelectOnClick(string level_Select)
    {
        SceneManager.LoadScene(level_Select, LoadSceneMode.Single);
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <parameters> none </parameters>
    /// <return> void </return>
    public void OptionsMenuOnClick()
    {
        if(TitleOptionsMenu == null)
        {
            TitleOptionsMenu.SetActive(true);
        }

    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <parameters> none </parameters>
    /// <return> void </return>
    public void BackToMenuOnClick()
    {
        if (TitleOptionsMenu.activeInHierarchy == false)
        {
            TitleOptionsMenu.SetActive(false);
            TitleScreen.SetActive(true);
        }
    }

    ///// <summary>
    ///// 
    ///// 
    ///// </summary>
    ///// <parameters> Game object to be disabled in hierarchy </parameters>
    ///// <return> void </return>
    //public void DisableGameObject(GameObject gameObject)
    //{
    //    gameObject.SetActive(false);
    //}

    ///// <summary>
    ///// 
    ///// 
    ///// </summary>
    ///// <parameters> Game object to be enabled in hierarch </parameters>
    ///// <return> void </return>
    //public void EnableGameObject(GameObject gameObject)
    //{
    //    gameObject.SetActive(true);
    //}
}
