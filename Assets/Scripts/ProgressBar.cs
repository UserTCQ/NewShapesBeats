using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public RectTransform runner;
    public RectTransform line;

    public Canvas canvas;

    public Vector2 offset;

    public float value;
    
    void Update()
    {
        runner.anchoredPosition = new Vector2(offset.x + line.rect.width * value, offset.y);
    }
}
