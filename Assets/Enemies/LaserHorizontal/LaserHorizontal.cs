using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHorizontal : MonoBehaviour, ISpawnedObject
{
    float dieTime = 0;
    float position = 0;
    float scale = 0;

    float spawnTime;

    public void OnSpawned(string[] args)
    {
        dieTime = float.Parse(args[0]);
        position = float.Parse(args[1]);
        scale = float.Parse(args[2]);

        spawnTime = Time2.elapsed;

        transform.localPosition = new Vector3(transform.localPosition.x, position, transform.localPosition.z);
        transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);

        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        float t = 0;
        while (t < 1)
        {
            t = (Time2.elapsed - spawnTime) * 10;
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, 0, t), transform.localPosition.y, transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => Time2.elapsed >= spawnTime + dieTime);
        Destroy(gameObject);
    }
}
