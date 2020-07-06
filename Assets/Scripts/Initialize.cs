using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Initialize : MonoBehaviour
{
    public TextMeshProUGUI song;
    public TextMeshProUGUI author;

    private void Start()
    {
        song.text = Level.usingLevel.songName;
        author.text = Level.usingLevel.author;
    }
}
