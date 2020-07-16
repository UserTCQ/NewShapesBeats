using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleScript : MonoBehaviour, ISpawnedObject
{
    public void OnSpawned(string[] args, object sender)
    {
        transform.localPosition = new Vector2(float.Parse(args[0]), float.Parse(args[1]));
        transform.localScale = new Vector2(float.Parse(args[2]), float.Parse(args[3]));
        transform.localRotation = Quaternion.Euler(0, 0, float.Parse(args[4]));
        GetComponent<SpriteRenderer>().color = new Color32(byte.Parse(args[5]), byte.Parse(args[6]), byte.Parse(args[7]), byte.Parse(args[8]));
        GetComponent<BoxCollider2D>().enabled = bool.Parse(args[9]);
    }
}
