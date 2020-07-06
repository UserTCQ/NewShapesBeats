using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelList : MonoBehaviour
{
    public Level[] levels;

    List<int> levelsToDisplay = new List<int>();
    List<LevelBtn> buttons = new List<LevelBtn>();

    public Level selectedLevel;

    public float buttonOffsetY;
    public float buttonOffsetX;

    public GameObject buttonPrefab;
    public RectTransform container;

    public TMP_InputField searchBar;

    void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levelsToDisplay.Add(i);
        }
        UpdateList();
    }

    public void Search()
    {
        levelsToDisplay.Clear();
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].songName.ToLower().Contains(searchBar.text.ToLower()) || levels[i].author.ToLower().Contains(searchBar.text.ToLower()))
            {
                levelsToDisplay.Add(i);
            }
        }
        UpdateList();
    }

    void UpdateList()
    {
        container.sizeDelta = new Vector2(container.sizeDelta.x, 50f * levelsToDisplay.Count);
        foreach (var go in buttons)
        {
            Destroy(go.gameObject);
        }
        buttons.Clear();
        for (int i = 0; i < levelsToDisplay.Count; i++)
        {
            var go = Instantiate(buttonPrefab, container);
            go.SetActive(true);
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonOffsetX, i * -50 + buttonOffsetY);
            var btn = go.GetComponent<LevelBtn>();
            btn.songName.text = levels[levelsToDisplay[i]].songName;
            btn.artist.text = levels[levelsToDisplay[i]].author;
            btn.value = levelsToDisplay[i];
            if (selectedLevel == levels[btn.value])
                btn.Select();
            buttons.Add(btn);
        }
    }

    public void OnButtonClick(LevelBtn btn)
    {
        if (selectedLevel != levels[btn.value])
        {
            ResetState();
            btn.Select();
            selectedLevel = levels[btn.value];
        }
        else
        {
            Level.usingLevel = levels[btn.value];
            SceneManager.LoadScene(1);
        }
    }

    public void ResetState()
    {
        foreach (var btn in buttons)
        {
            if (selectedLevel == levels[btn.value])
            {
                btn.Deselect();
                break;
            }
        }
    }
}
