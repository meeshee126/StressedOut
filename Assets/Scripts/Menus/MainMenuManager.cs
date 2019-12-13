using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Michael Schmidt
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiMainMenu, uiOptions;

    void Update()
    {
        //Return to Main Menu if in Options by pressing ESC button
        if(Input.GetKeyDown(KeyCode.Escape) && uiOptions.activeInHierarchy == true)
        {
            Back();
        }
    }

    /// <summary>
    /// Sarting Game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

    /// <summary>
    /// Toggle option menu  
    /// </summary>
    public void ToggleOption()
    {
        //Check if Menu or Option is active

        //if option is not active
        if(uiOptions.activeInHierarchy == false)
        {
            //disable menu
            uiMainMenu.gameObject.SetActive(false);

            //enable option
            uiOptions.gameObject.SetActive(true);
        }

        //if option is enable
        else
        {
            //enable menu
            uiMainMenu.gameObject.SetActive(true);

            //disable option
            uiOptions.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// return to Menu button
    /// </summary>
    public void Back()
    {
        uiMainMenu.gameObject.SetActive(true);
        uiOptions.gameObject.SetActive(false);
    }

    /// <summary>
    /// Quitting the Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
