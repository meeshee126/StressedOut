using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Michael Schmidt
public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiPause, uiOptions;
    
    [SerializeField]
    Image background;

    Timer timer;
    Sun sunScript;

	public GameObject ButtonSave;
	public GameObject MainCamera;

	public Vector3 StartCamera;

	private void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        sunScript = GameObject.Find("Sunset").GetComponent<Sun>();

		StartCamera = MainCamera.transform.position;
    }

    void Update()
    {
        SetBackground();
		SaveButton();

		//get back to pause menu if in option menu
		if (Input.GetKeyDown(KeyCode.Escape) && uiOptions.gameObject.activeInHierarchy)
        {
            Back();
            return;
        }
        
        //toggle pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

    }

    /// <summary>
    /// Set Background Panel when toggle pause menu
    /// </summary>
    void SetBackground()
    {
        //get Colors
        Color red = new Color(1, 0, 0, 0.2f);
        Color black = new Color(0, 0, 0, 0.2f);

        //set Colors
        if (timer.currentDayTime == Timer.DayTime.Panic)
        {
            background.color = red;
        }
        else
        {
            background.color = black;
        }
    }

    /// <summary>
    /// Toggleling Pause menu
    /// </summary>
    public void PauseMenu()
    {
        // checks after pressing "escape" button if pause Menu is active or not

        // if pause Menu is not active
        if (uiPause.activeInHierarchy == false)
        {
            // pause Menu is setting active
            uiPause.gameObject.SetActive(true);
            background.gameObject.SetActive(true);

            // disable enviroment physics
            Time.timeScale = 0;

            // disable Player controls 
            GameObject.Find("Player").GetComponent<Player>().enabled = false;     
        }

        // if pause menu is active after pressing "escape" button
        else
        {
            // pause menu is setting inactive
            uiPause.gameObject.SetActive(false);
            background.gameObject.SetActive(false);

            // enable enviroment physics
            Time.timeScale = 1;

            // enable Player and controls
            GameObject.Find("Player").GetComponent<Player>().enabled = true;
        }
    }

    /// <summary>
    /// Toggle option menu  
    /// </summary>
    public void ToggleOption()
    {
        //Check if Pausemenu or Option is active

        //if option is not active
        if (uiOptions.activeInHierarchy == false)
        {
            //disable pausemenu 
            uiPause.gameObject.SetActive(false);

            //enable option
            uiOptions.gameObject.SetActive(true);
        }

        //if option is enable
        else
        {
            //enable pausemenu
            uiPause.gameObject.SetActive(true);

            //disable option
            uiOptions.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// return to Pausemenu button
    /// </summary>
    public void Back()
    {
        uiPause.gameObject.SetActive(true);
        uiOptions.gameObject.SetActive(false);
    }

    /// <summary>
    /// Return to Main menu
    /// </summary>
    public void BackToMenu()
    {
        // reset Pause Menu to OFF
        uiPause.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        // before getting back to Mainmenu scene, activate all scripts and physic
        GameObject.Find("Player").GetComponent<Player>().enabled = true;
        Time.timeScale = 1;

        // loading Mainmenu scene
        SceneManager.LoadScene("MainMenu");
    }

	/// <summary>
	/// Henrik Hafner
	/// prevents you from saving at night and when you are not in the base
	/// </summary>
	public void SaveButton()
	{
		if (timer.currentDayTime == Timer.DayTime.Day && MainCamera.transform.position == StartCamera)
		{
			ButtonSave.gameObject.SetActive(true);
		}
		else
		{
			ButtonSave.gameObject.SetActive(false);
		}
	}
}
