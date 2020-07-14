using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleScript : MonoBehaviour, ISpawnedObject
{
    public void OnSpawned(string[] args, object sender)
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 32, 113, byte.Parse(args[0]));
        GetComponent<BoxCollider2D>().enabled = bool.Parse(args[1]);
    }
}
