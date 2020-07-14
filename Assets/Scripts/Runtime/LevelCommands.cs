using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommands : MonoBehaviour
{
    public void spawn(string[] args)
    {
        List<string> argsObj = new List<string>();

        int argSize = args.Length;

        GameObject obj;
        if (args.Length >= 2)
        {
            if (args[args.Length - 2] == "->")
            {
                obj = ObjectPool.currentPool.Spawn(args[0], args[args.Length - 1]);
                argSize -= 2;
            }
            else
                obj = ObjectPool.currentPool.Spawn(args[0]);
        }
        else
            obj = ObjectPool.currentPool.Spawn(args[0]);

        for (int i = 1; i < argSize; i++)
        {
            argsObj.Add(args[i]);
        }
        obj.GetComponent<ISpawnedObject>().OnSpawned(argsObj.ToArray(), this);
    }

    public void move(string[] args)
    {
        var newPos = new Vector2(float.Parse(args[2]), float.Parse(args[3]));
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];

        if (length > 0)
        {
            StartCoroutine(move());
        }
        else
        {
            group.transform.localPosition = newPos;
        }
        
        IEnumerator move()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localPosition = Vector2.Lerp(group.transform.localPosition, newPos, t);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void scale(string[] args)
    {
        var newScale = new Vector2(float.Parse(args[2]), float.Parse(args[3]));
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];

        if (length > 0)
        {
            StartCoroutine(scale());
        }
        else
        {
            group.transform.localScale = newScale;
        }

        IEnumerator scale()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localScale = Vector2.Lerp(group.transform.localScale, newScale, t);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void rotate(string[] args)
    {
        var newRotation = float.Parse(args[2]);
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];

        if (length > 0) 
        {
            StartCoroutine(rotate());
        }
        else
        {
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }

        IEnumerator rotate()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(group.transform.localRotation.z, newRotation, t));
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void destroy(string[] args)
    {
        var group = ObjectPool.currentPool.groups[args[0]];
        Destroy(group);
        group = null;
    }
}