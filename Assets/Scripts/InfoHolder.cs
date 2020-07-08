using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public static Level usingLevel;

    public string songName = "null";
    public string author = "null";
    public AudioClip song;
    public float levelLength;
    public GObject[] objs;
}

[Serializable]
public class StorageLevel
{
    public string songName = "null";
    public string author = "null";
    public string song;
    public float levelLength;
    public StorageGObject[] objs;
}

[Serializable]
public class GObject
{
    public float time;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public string[] args;
    public GameObject spawnObj;
}

[Serializable]
public class StorageGObject
{
    public float time;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public string[] args;
    public string objType;
}

public static class Settings
{
    public static float volume = 1;
    public static float sfxVolume = 1;

    public static bool isFullscreen = true;
    public static bool isVsync = true;

    public static int hits;
    public static int deaths;
}
