﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommands : MonoBehaviour
{
    [Header("spawn command")]
    [SerializeField]
    private StringGameObjectDictionary objects;
    [SerializeField]
    private Transform objContainer;

    public void spawn(string[] args)
    {
        List<string> argsObj = new List<string>();

        for (int i = 1; i < args.Length; i++)
        {
            argsObj.Add(args[i]);
        }

        var obj = Instantiate(objects[args[0]], objContainer);
        obj.GetComponent<ISpawnedObject>().OnSpawned(argsObj.ToArray());
    }

    public void printargs(string[] args)
    {
        foreach (string arg in args)
        {
            Debug.Log(arg);
        }
    }
}