using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sector : MonoBehaviour
{
    private int baseIndex;
    private int scoreValue;
    private TextMeshPro valueLabel;

    public int BaseIndex
    {
        get => baseIndex;
        set => baseIndex = value;
    }

    public int ScoreValue
    {
        get => scoreValue;
        set
        {
            scoreValue = value; 
            SetValueLabel();
        }
    }


    public void Init(int i)
    {
        baseIndex = i;
        scoreValue = i;
        SetValueLabel();
    }

    private void SetValueLabel()
    {
        valueLabel.text = scoreValue.ToString();
    }


    private void Awake()
    {
        valueLabel = GetComponentInChildren<TextMeshPro>();
    }


}
