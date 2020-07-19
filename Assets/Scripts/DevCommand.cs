using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;
using System;

public class DevCommand : MonoBehaviour
{
    public TMP_InputField inputField;

    bool focus;

    void Update()
    {
        if (focus && inputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            var cmdSplitted = inputField.text.Split('"')
                     .Select((element, index) => index % 2 == 0
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                           : new string[] { element })
                     .SelectMany(element => element).ToArray();

            Command cmd = new Command();
            List<string> args = new List<string>();

            cmd.command = cmdSplitted[0];
            for (int j = 1; j < cmdSplitted.Length; j++)
            {
                args.Add(cmdSplitted[j]);
            }
            cmd.args = args.ToArray();
            inputField.text = "";
            LevelSystem.system.InterpretCommand(cmd);
        }
        focus = inputField.isFocused;
    }
}
