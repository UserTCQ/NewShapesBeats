using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelList : MonoBehaviour
{
    public List<Level> levels;

    List<int> levelsToDisplay = new List<int>();
    List<LevelBtn> buttons = new List<LevelBtn>();
    List<RectTransform> rectTransformsBtn = new List<RectTransform>();

    public Level selectedLevel;

    public float buttonOffsetY;
    public float buttonOffsetX;

    public GameObject buttonPrefab;
    public RectTransform container;

    public TMP_InputField searchBar;

    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levelsToDisplay.Add(i);
        }
        InitializeList();
    }

    void InitializeList()
    {
        container.sizeDelta = new Vector2(container.sizeDelta.x, 50f * levels.Count);
        for (int i = 0; i < levels.Count; i++)
        {
            var go = Instantiate(buttonPrefab, container);
            var rt = go.GetComponent<RectTransform>();
            go.SetActive(true);
            rectTransformsBtn.Add(rt);
            rt.anchoredPosition = new Vector2(buttonOffsetX, i * -50 + buttonOffsetY);
            var btn = go.GetComponent<LevelBtn>();
            btn.songName.text = levels[i].songName;
            btn.artist.text = levels[i].author;
            btn.value = i;
            buttons.Add(btn);
        }
    }

    public void Search()
    {
        levelsToDisplay.Clear();
        for (int i = 0; i < levels.Count; i++)
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
        int t = -1;
        for (int i = 0; i < buttons.Count; i++)
        {
            var btn = buttons[i];
            btn.Deselect();
            if (levelsToDisplay.Contains(btn.value))
            {
                t++;
                btn.gameObject.SetActive(true);
                rectTransformsBtn[i].anchoredPosition = new Vector2(buttonOffsetX, t * -50 + buttonOffsetY);
                if (levels[btn.value] == selectedLevel)
                {
                    btn.Select();
                }
            }
            else
                btn.gameObject.SetActive(false);
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
