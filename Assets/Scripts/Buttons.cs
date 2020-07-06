using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Retry()
    {
        PauseScript.psystem.UnPause();
        StartCoroutine(PlayerControl.player.die());
    }

    public void Reset()
    {
        Settings.hits = 0;
        Settings.deaths = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
