using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenState : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = Settings.isFullscreen;
    }

    public void ChangeVal()
    {
        if (toggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Settings.isFullscreen = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Settings.isFullscreen = false;
        }
    }
}
