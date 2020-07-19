using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static PauseScript psystem;

    public static bool pausable = true;

    public AudioSource audioSource;
    public GameObject pauseMenu;

    bool pause;

    private void Start()
    {
        psystem = this;
        pausable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausable)
        {
            pause = !pause;
            if (pause) UnPause(); 
            else Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        audioSource.Pause();
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        audioSource.UnPause();
        pauseMenu.SetActive(false);
    }
}
