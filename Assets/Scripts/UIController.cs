using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas worldCanvas;
    public Canvas cameraCanvas;
    public Camera mainCamera;
    public TextMeshProUGUI timeElapsing;
    public UIElement collection;
    [SerializeField] private GameObject tapToPlayPanel;


   // [SerializeField] private UIElement starsUI;
   //[SerializeField] private UIElement coinsUI;
   // [SerializeField] private UIElement timerUI;


    public void ShowMainMenu()
    {
     //   mainMenu.ShowWindow();
    }

    public void SetCoins(int value)
    {
      //  coinsUI.elementValue.text = value.ToString();
    }

    internal void SetStars(int value)
    {
      //  starsUI.elementValue.text = value.ToString();
    }
}
