using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void OpenGameScene() 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartPlay() 
    {
        GameManager.Instance.StartPlay();
    }
}
