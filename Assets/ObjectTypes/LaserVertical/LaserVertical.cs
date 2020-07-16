using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVertical : MonoBehaviour, ISpawnedObject
{
    float dieTime = 0;
    float position = 0;
    float scale = 0;

    float spawnTime;

    public void OnSpawned(string[] args, object sender)
    {
        dieTime = float.Parse(args[0]);
        position = float.Parse(args[1]);
        scale = float.Parse(args[2]);

        spawnTime = Time2.elapsed;

        transform.localPosition = new Vector3(position, transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(scale, transform.localScale.y,  transform.localScale.z);

        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        float t = 0;
        while (t < 1)
        {
            t = (Time2.elapsed - spawnTime) * 4;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, 0, t), transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => Time2.elapsed >= spawnTime + dieTime);
        Destroy(gameObject);
    }
}
