using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScreenResolutions : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown dropdownMenu;

    void Start()
    {
        resolutions = Screen.resolutions;
        dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, Settings.isFullscreen); });
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdownMenu.options.Add(new TMP_Dropdown.OptionData(ResToString(resolutions[i])));
            if (resolutions[i].Equals(Screen.currentResolution))
            {
                dropdownMenu.value = i;
            }
        }
        dropdownMenu.RefreshShownValue();
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}