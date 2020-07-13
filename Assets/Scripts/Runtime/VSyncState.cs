using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSyncState : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = Settings.isVsync;
    }

    public void ChangeVal()
    {
        if (toggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            Settings.isVsync = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Settings.isVsync = false;
        }
    }
}
