using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public static class Time2
{
    public static float elapsed;
}

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem system;

    public AudioSource source;

    public ProgressBar progessBar;

    public Transform endLine;

    public SpawnPool pool;

    public float minPos;
    public float maxPos;
    public float scrollSpeed;

    public GameObject endScreen;
    public RectTransform endCard;

    public LevelCommands cmdClass;

    public delegate void CommandHandler(string[] args);
    private Dictionary<string, CommandHandler> commands;

    [SerializeField]
    private List<GameObject> objectsToDisable;

    public bool stop = false;

#if UNITY_EDITOR
    [SerializeField]
    private bool bypass = false;
    public Level level;
#endif

    public Coroutine processor;

    void Start()
    {
        system = this;

#if UNITY_EDITOR
        if (bypass)
        {
            Level.usingLevel = level;
        }
        WWW w = new WWW(Level.usingLevel.song);
        source.clip = w.GetAudioClip(true, true);
        source.Play();
#else
        WWW w = new WWW(Level.usingLevel.song);
        source.clip = w.GetAudioClip();
        source.Play();
#endif

        commands = new Dictionary<string, CommandHandler>()
        {
            { "spawn",  cmdClass.spawn },
            { "printargs", cmdClass.printargs }
        };

        #region InitializePool
        foreach (var cmd in Level.usingLevel.commands)
        {
            if (cmd.command == "spawn")
            {
                pool.Add(cmd.args[0]);
            }
        }
        #endregion

        processor = StartCoroutine(LevelProcessor());
    }

    private void Update()
    {
        if (!stop)
        {
            Time2.elapsed = (float)source.timeSamples / source.clip.frequency;

            progessBar.value = Time2.elapsed / Level.usingLevel.levelLength;
        }

        if (!stop && Time2.elapsed >= Level.usingLevel.levelLength)
        {
            stop = true;
            StopAllCoroutines();
            StartCoroutine(endAnim());
        }
    }

    IEnumerator endAnim()
    {
        float t = 0;
        Vector2 pos;
        while (t < 1)
        {
            t += Time.deltaTime * scrollSpeed;
            pos = Vector2.Lerp(new Vector2(maxPos, 0), new Vector2(minPos, 0), t);
            endLine.position = pos;
            if (PlayerControl.player.transform.position.x >= pos.x)
            {
                PauseScript.pausable = false;
                Destroy(endLine.gameObject);
                foreach (var obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(1, 0, t) * Settings.volume;
            yield return new WaitForEndOfFrame();
        }
        source.Stop();
        endScreen.SetActive(true);
        endScreen.GetComponent<EndCardScript>().EndInit();
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.1f;
            endCard.anchoredPosition = Vector2.Lerp(endCard.anchoredPosition, Vector2.zero, t);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LevelProcessor()
    {
        for (int i = 0; i < Level.usingLevel.commands.Length; i++)
        {
            yield return new WaitUntil(() => Time2.elapsed >= Level.usingLevel.commands[i].time);
            InterpretCommand(Level.usingLevel.commands[i]);
        }
    }

    public void InterpretCommand(Command command)
    {
        for (int i = 0; i < command.args.Length; i++)
        {
            switch (command.args[i]) 
            {
                case "rndx":
                    command.args[i] = Random.Range(-9.6f, 9.6f).ToString();
                    break;
                case "rndy":
                    command.args[i] = Random.Range(-5.4f, 5.4f).ToString();
                    break;
            }
        }
        commands[command.command].Invoke(command.args);
    }
}
