using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour
{
    public RectTransform rt;
    public Vector3 offset;

    RectTransform t;

    private void Start()
    {
        t = GetComponent<RectTransform>();
    }

    void Update()
    {
        t.localPosition = rt.localPosition + offset;
    }
}
