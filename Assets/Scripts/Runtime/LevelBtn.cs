using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LevelBtn : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI songName;
    public TextMeshProUGUI artist;

    public LevelList mainList;

    public Color selectedColor;
    public Color idleColor;

    public int value;

    public void OnPointerClick(PointerEventData eventData)
    {
        mainList.OnButtonClick(this);
    }

    public void Select()
    {
        GetComponent<Image>().color = selectedColor;
    }

    public void Deselect()
    {
        GetComponent<Image>().color = idleColor;
    }
}
