using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommands : MonoBehaviour
{
    [Header("shake command")]
    public Transform camera;

    public void spawn(string[] args)
    {
        List<string> argsObj = new List<string>();

        int argSize = args.Length;

        GameObject obj;
        if (args.Length >= 2)
        {
            if (args[args.Length - 2] == "->")
            {
                obj = ObjectPool.currentPool.Spawn(args[1], args[args.Length - 1], int.Parse(args[0]));
                argSize -= 2;
            }
            else
                obj = ObjectPool.currentPool.Spawn(args[1], int.Parse(args[0]));
        }
        else
            obj = ObjectPool.currentPool.Spawn(args[1], int.Parse(args[0]));

        for (int i = 2; i < argSize; i++)
        {
            argsObj.Add(args[i]);
        }
        obj.GetComponent<ISpawnedObject>().OnSpawned(argsObj.ToArray(), this);
    }

    public void move(string[] args)
    {
        var newPos = new Vector3(float.Parse(args[2]), float.Parse(args[3]));
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];
        var oldPos = group.transform.localPosition;

        if (length > 0)
        {
            switch (args[4])
            {
                case "easein":
                    StartCoroutine(moveIn());
                    break;
                case "easeout":
                    StartCoroutine(moveOut());
                    break;
                case "easeinout":
                    StartCoroutine(moveInOut());
                    break;
                case "linear":
                    StartCoroutine(moveLinear());
                    break;
            }
        }
        else
        {
            group.transform.localPosition = newPos;
        }

        IEnumerator moveIn()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localPosition = Vector2.Lerp(oldPos, newPos, EaseIn(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localPosition = newPos;
        }

        IEnumerator moveOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localPosition = Vector2.Lerp(oldPos, newPos, EaseOut(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localPosition = newPos;
        }

        IEnumerator moveInOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localPosition = Vector2.Lerp(oldPos, newPos, EaseInOut(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localPosition = newPos;
        }

        IEnumerator moveLinear()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localPosition = Vector2.Lerp(oldPos, newPos, t);
                yield return new WaitForEndOfFrame();
            }
            group.transform.localPosition = newPos;
        }
    }

    public void scale(string[] args)
    {
        var newScale = new Vector2(float.Parse(args[2]), float.Parse(args[3]));
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];
        var oldScale = group.transform.localScale;

        if (length > 0)
        {
            switch (args[4])
            {
                case "easein":
                    StartCoroutine(scaleIn());
                    break;
                case "easeout":
                    StartCoroutine(scaleOut());
                    break;
                case "easeinout":
                    StartCoroutine(scaleInOut());
                    break;
                case "linear":
                    StartCoroutine(scaleLinear());
                    break;
            }
        }
        else
        {
            group.transform.localScale = newScale;
        }

        IEnumerator scaleIn()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localScale = Vector2.Lerp(oldScale, newScale, EaseIn(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localScale = newScale;
        }

        IEnumerator scaleOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localScale = Vector2.Lerp(oldScale, newScale, EaseOut(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localScale = newScale;
        }

        IEnumerator scaleInOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localScale = Vector2.Lerp(oldScale, newScale, EaseInOut(t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localScale = newScale;
        }

        IEnumerator scaleLinear()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localScale = Vector2.Lerp(oldScale, newScale, t);
                yield return new WaitForEndOfFrame();
            }
            group.transform.localScale = newScale;
        }
    }

    public void rotate(string[] args)
    {
        var newRotation = float.Parse(args[2]);
        float length = float.Parse(args[1]);

        var group = ObjectPool.currentPool.groups[args[0]];
        var oldRotation = group.transform.localRotation.z;

        if (length > 0) 
        {
            switch (args[3])
            {
                case "easein":
                    StartCoroutine(rotateIn());
                    break;
                case "easeout":
                    StartCoroutine(rotateOut());
                    break;
                case "easeinout":
                    StartCoroutine(rotateInOut());
                    break;
                case "linear":
                    StartCoroutine(rotateLinear());
                    break;
            }
        }
        else
        {
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }

        IEnumerator rotateIn()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldRotation, newRotation, EaseIn(t)));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }

        IEnumerator rotateOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldRotation, newRotation, EaseOut(t)));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }

        IEnumerator rotateInOut()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldRotation, newRotation, EaseInOut(t)));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }

        IEnumerator rotateLinear()
        {
            float startTime = Time2.elapsed;
            float t = 0;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                group.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldRotation, newRotation, t));
                yield return new WaitForEndOfFrame();
            }
            group.transform.localRotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    public void destroy(string[] args)
    {
        var group = ObjectPool.currentPool.groups[args[0]];
        Destroy(group);
        ObjectPool.currentPool.groups.Remove(args[0]);
    }

    public void shake(string[] args)
    {
        var length = float.Parse(args[0]);

        if (length == 0)
            return;

        var npos = new Vector2(float.Parse(args[1]), float.Parse(args[2]));

        StartCoroutine(shakeCam());

        IEnumerator shakeCam()
        {
            float t = 0;
            float startTime = Time2.elapsed;
            while (t < 180)
            {
                t = (Time2.elapsed - startTime) * 180 / length;
                camera.position = Vector3.Lerp(new Vector3(0, 0, -100), new Vector3(npos.x, npos.y, -100), Mathf.Sin(t * 0.0174532925f));
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void rotatecam(string[] args)
    {
        var length = float.Parse(args[0]);

        if (length == 0)
            return;

        var nrot = float.Parse(args[1]);
        var orot = camera.rotation.z;

        if (length > 0)
            StartCoroutine(camRotate());
        else
            camera.rotation = Quaternion.Euler(0, 0, nrot);

        IEnumerator camRotate()
        {
            float t = 0;
            float startTime = Time2.elapsed;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                camera.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(orot, nrot, EaseOut(t)));
                yield return new WaitForEndOfFrame();
            }
            camera.rotation = Quaternion.Euler(0, 0, nrot);
        }
    }

    public void moveCam(string[] args)
    {
        var length = float.Parse(args[0]);

        var npos = new Vector2(float.Parse(args[1]), float.Parse(args[2]));

        if (length > 0)
            StartCoroutine(moveCam());
        else
            camera.position = new Vector3(npos.x, npos.y, -100);

        IEnumerator moveCam()
        {
            float t = 0;
            float startTime = Time2.elapsed;
            while (t < 1)
            {
                t = (Time2.elapsed - startTime) / length;
                camera.position = Vector3.Lerp(camera.position, new Vector3(npos.x, npos.y, -100), t);
                yield return new WaitForEndOfFrame();
            }
            camera.position = new Vector3(npos.x, npos.y, -100);
        }
    }

    float EaseInOut(float t)
    {
        float sqt = t * t;
        return sqt / (2.0f * (sqt - t) + 1.0f);
    }

    float EaseIn(float t)
    {
        return t * t;
    }

    float EaseOut(float t)
    {
        return 1 - (1 - t) * (1 - t);
    }
}