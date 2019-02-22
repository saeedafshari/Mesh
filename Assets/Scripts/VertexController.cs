using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{
    TextMesh text;
    public int InitialValue { get; private set; }
    public int Value { get; set; }
    
    public void Program(int initialValue)
    {
        InitialValue = initialValue;
        Value = initialValue;

        text = transform.Find("Label").GetComponent<TextMesh>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        text.text = Value.ToString();
    }
}
