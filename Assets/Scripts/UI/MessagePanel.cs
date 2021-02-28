using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePanel : Window
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string message) 
    {
        text.text = message;
    }
}
