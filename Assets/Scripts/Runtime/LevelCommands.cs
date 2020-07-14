using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommands : MonoBehaviour
{

    public void spawn(string[] args)
    {
        List<string> argsObj = new List<string>();

        for (int i = 1; i < args.Length; i++)
        {
            argsObj.Add(args[i]);
        }

        var obj = SpawnPool.currentPool.Spawn(args[0]);
        obj.GetComponent<ISpawnedObject>().OnSpawned(argsObj.ToArray(), this);
    }

    public void printargs(string[] args)
    {
        foreach (string arg in args)
        {
            Debug.Log(arg);
        }
    }
}