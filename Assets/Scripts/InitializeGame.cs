using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    List<string> levelsToLoad = new List<string>();

    private void Awake()
    {
        string root = Application.streamingAssetsPath;
        string[] subDirs = Directory.GetDirectories(root);

        foreach (var d in subDirs)
        {
            string path = root + $"/{subDirs}/level.json";
            if (File.Exists(path))
            {
                levelsToLoad.Add(File.ReadAllText(path));
            }
        }

        LoadLevels();
    }

    private void LoadLevels()
    {
        //TODO: load levels
    }

    private Level ParseLevel(string levelJson)
    {
        //TODO: convert
        return null;
    }
}
