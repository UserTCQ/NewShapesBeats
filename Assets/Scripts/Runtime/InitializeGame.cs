using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class RawLevel
{
    public string dir;
    public string lvlInfoJson;
    public string lvlCommands;
}

public class InitializeGame : MonoBehaviour
{
    List<RawLevel> rawLevels = new List<RawLevel>();

    [SerializeField]
    private LevelList listScript;

    private void Awake()
    {
        string root = Application.streamingAssetsPath;
        string[] subDirs = Directory.GetDirectories(root);

        foreach (var d in subDirs)
        {
            string _d = d.Replace("\\", "/");
            string pathInfo = $"{_d}/level.json";
            string pathLayout = $"{_d}/layout.lvl";

            if (File.Exists(pathInfo) && File.Exists(pathLayout))
            {
                RawLevel ppl = new RawLevel()
                {
                    dir = $"{_d}/",
                    lvlInfoJson = File.ReadAllText(pathInfo),
                    lvlCommands = File.ReadAllText(pathLayout)
                };
                rawLevels.Add(ppl);
            }
        }

        LoadLevels();
    }

    private void LoadLevels()
    {
        List<Level> levels = new List<Level>();

        for (int i = 0; i < rawLevels.Count; i++)
        {
            Level lvl = new Level();
            ParseLevel(rawLevels[i], ref lvl);
            levels.Add(lvl);
        }

        listScript.levels = levels;
    }

    private void ParseLevel(RawLevel ppl, ref Level level)
    {
        StorageLevel sl = JsonUtility.FromJson<StorageLevel>(ppl.lvlInfoJson);
        level.songName = sl.songName;
        level.author = sl.author;
        level.song = ppl.dir + sl.song;
        level.levelLength = sl.levelLength;
        level.commands = ParseLevelCommands(ppl.lvlCommands);
    }

    private Command[] ParseLevelCommands(string levelCmds)
    {
        string[] cmdStrs = levelCmds.Split('\n');
        Command[] cmds = new Command[cmdStrs.Length];
        for (int i = 0; i < cmdStrs.Length; i++)
        {
            var cmdSplitted = cmdStrs[i].Split('"')
                     .Select((element, index) => index % 2 == 0 
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                           : new string[] { element })
                     .SelectMany(element => element).ToArray();

            List<string> args = new List<string>();
            cmds[i] = new Command();
            cmds[i].time = float.Parse(cmdSplitted[0]);
            cmds[i].command = cmdSplitted[1];
            for (int j = 2; j < cmdSplitted.Length; j++)
            {
                args.Add(cmdSplitted[j]);
            }
            cmds[i].args = args.ToArray();
        }
        return cmds;
    }
}
