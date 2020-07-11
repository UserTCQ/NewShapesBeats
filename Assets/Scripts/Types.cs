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
    public Command[] commands;
}

[Serializable]
public class StorageLevel
{
    public string songName = "null";
    public string author = "null";
    public string song;
    public float levelLength;
}

[Serializable]
public class Command
{
    public float time;
    public string command;
    public string[] args;
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
