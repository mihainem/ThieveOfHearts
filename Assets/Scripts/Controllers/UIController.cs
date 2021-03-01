using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Space(10f)]
    public TextMeshProUGUI timerText;
    public Image timerFillImage;
    public UIElement collection;
    

    [Space(10f),Header("Popups")]
    [SerializeField] private Window tapToPlayPanel;
    [SerializeField] private MessagePanel winPanel;
    [SerializeField] private Window losePanel;


    private void Start()
    {
        winPanel.HideWindow();
        losePanel.HideWindow();
    }

    internal void TryShowFailedPanel()
    {
        if (!winPanel.isActiveAndEnabled) {
            losePanel.ShowWindow();
        }
    }

    internal void ShowWinPanel(int totalCollected)
    {
        winPanel.SetText($"Good job robin' {Environment.NewLine} you now have {totalCollected} more hearts under your hood ;)");
         winPanel.ShowWindow();
    }

    internal void ShowTapToPlayPanel()
    {
        tapToPlayPanel.ShowWindow();
    }
}
