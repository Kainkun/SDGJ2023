using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int count = 0;

    private void Awake()
    {
        text.text = count.ToString().PadLeft(2, '0');
    }

    public void Increment()
    {
        count++;
        count = Mathf.Min(99, count);
        text.text = count.ToString().PadLeft(2, '0');
    }

    public void Decrement()
    {
        count--;
        count = Mathf.Max(0, count);
        text.text = count.ToString().PadLeft(2, '0');
    }
}