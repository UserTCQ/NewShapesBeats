using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public GameObject bounaryObj;

    private Vector2 bounds;
    private float objectWidth;
    private float objectHeight;

    public GameObject graphics;
    
    void Start()
    {
        bounds = new Vector2(bounaryObj.GetComponent<SpriteRenderer>().bounds.extents.x, bounaryObj.GetComponent<SpriteRenderer>().bounds.extents.y);
        objectWidth = graphics.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = graphics.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, bounds.x * -1 + objectWidth, bounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, bounds.y * -1 + objectHeight, bounds.y - objectHeight);
        transform.position = viewPos;
    }
}