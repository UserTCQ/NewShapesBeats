using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public Transform container;
    
    void Update()
    {
        transform.localPosition = new Vector3(container.localPosition.x, container.localPosition.y - 0.1f, 0.001f);
        transform.localScale = container.localScale;
        transform.rotation = container.rotation;
    }
}
