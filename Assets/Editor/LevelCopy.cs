using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSystem))]
public class LevelCopy : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelSystem system = (LevelSystem)target;

        if (GUILayout.Button("Copy"))
        {
            Level.copiedLevel = system.level;
            Debug.Log("Level copied!");
        }
    }
}
