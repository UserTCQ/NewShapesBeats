using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PreParseLevel
{
    public string dir;
    public string lvlInfoJson;
    public string lvlCommands;
}

public class InitializeGame : MonoBehaviour
{
    List<PreParseLevel> preParseLevels = new List<PreParseLevel>();

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
                PreParseLevel ppl = new PreParseLevel()
                {
                    dir = $"{_d}/",
                    lvlInfoJson = File.ReadAllText(pathInfo),
                    lvlCommands = File.ReadAllText(pathLayout)
                };
                preParseLevels.Add(ppl);
            }
        }

        LoadLevels();
    }

    private void LoadLevels()
    {
        List<Level> levels = new List<Level>();

        for (int i = 0; i < preParseLevels.Count; i++)
        {
            Level lvl = new Level();
            ParseLevel(preParseLevels[i], ref lvl);
            levels.Add(lvl);
        }

        listScript.levels = levels.ToArray();
    }

    private void ParseLevel(PreParseLevel ppl, ref Level level)
    {
        StorageLevel sl = JsonUtility.FromJson<StorageLevel>(ppl.lvlInfoJson);
        level.songName = sl.songName;
        level.author = sl.author;
        WWW w = new WWW(ppl.dir + sl.song);
        level.song = w.GetAudioClip(false, true, AudioType.OGGVORBIS);
        level.levelLength = sl.levelLength;
        level.commands = ParseLevelCommands(ppl.lvlCommands);
    }

    private Command[] ParseLevelCommands(string levelCmds)
    {
        string[] cmdStrs = levelCmds.Split('\n');
        Command[] cmds = new Command[cmdStrs.Length];
        for (int i = 0; i < cmdStrs.Length; i++)
        {
            string[] cmdSplitted = cmdStrs[i].Split(' ');
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
