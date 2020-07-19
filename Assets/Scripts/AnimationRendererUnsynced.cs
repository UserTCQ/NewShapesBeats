using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationRendererUnsynced : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int frame = 0;
    float frameFloat;

    public bool looping;
    public float framerate;
    public Sprite[] frames;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        while ((int)frameFloat < frames.Length || looping)
        {
            spriteRenderer.sprite = frames[frame];
            frameFloat += Time.deltaTime * framerate;
            frame = ((int)frameFloat) % frames.Length;
            yield return new WaitForEndOfFrame();
        }
    }
}
