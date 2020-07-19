using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSync : MonoBehaviour
{
    public TextMeshProUGUI tmpuMusic;
    public TextMeshProUGUI tmpuSfx;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSource.volume = Settings.volume;
        sfxSource.volume = Settings.sfxVolume;

        musicSlider.value = Settings.volume * 100;
        sfxSlider.value = Settings.sfxVolume * 100;
    }

    public void ChangeNum(int type)
    {
        if (type == 1)
        {
            musicSource.volume = musicSlider.value / 100f;
            tmpuMusic.text = musicSlider.value + "%";
            Settings.volume = musicSlider.value / 100f;
        }
        else
        {
            sfxSource.volume = sfxSlider.value / 100f;
            tmpuSfx.text = sfxSlider.value + "%";
            Settings.sfxVolume = musicSlider.value / 100f;
        }
    }
}
