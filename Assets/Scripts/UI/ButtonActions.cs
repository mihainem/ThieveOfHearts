using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// ButtonActions is added on buttons to make encapsulate method calls to their own object.
/// </summary>
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

    public void RetryLevel() 
    {
        GameManager.Instance.RetryLevel();
    }
    public void PlayNextLevel() 
    {
        GameManager.Instance.PlayNextLevel();
    }
}
