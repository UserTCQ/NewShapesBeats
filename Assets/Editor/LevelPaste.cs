using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelList))]
public class LevelPaste : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelList list = (LevelList)target;

        if (GUILayout.Button("Paste"))
        {
            list.levels.Add(Level.copiedLevel);
            Debug.Log("Level pasted!");
        }
    }
}
