﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StringFormatExample : MonoBehaviour
{
    private enum FormatOptions {Time };
    [SerializeField] private FormatOptions _FormatOption;
    [SerializeField] private TextMeshProUGUI _ExampleText;

    private float _Timer;

    void Update()
    {
        _Timer += 1 * Time.deltaTime;

        switch (_FormatOption)
        {
            case FormatOptions.Time:
                _ExampleText.text = string.Format("{0:00}:{1:00}:{2:00}", Mathf.Floor(_Timer / 3600), Mathf.Floor((_Timer / 60) % 60), _Timer % 60);
                break;
        }
    }
}
