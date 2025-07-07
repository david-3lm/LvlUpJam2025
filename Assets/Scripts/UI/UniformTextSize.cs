using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UniformTextSize : MonoBehaviour
{
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        var tmps = GetComponentsInChildren<TextMeshProUGUI>();
        float minSize = float.MaxValue;
        foreach (var t in tmps)
            minSize = Mathf.Min(minSize, t.fontSize);

        foreach (var t in tmps)
        {
            t.enableAutoSizing = false;
            t.fontSize = minSize;
        }
    }
}

