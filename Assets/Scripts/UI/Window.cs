using System.Collections.Specialized;
//using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Window : MonoBehaviour, IPointerClickHandler
{
    public bool closeOnClick = true;
    public bool closeOnButtonClick = false;
    public bool closeOnClickOutside = false;
    private Animator animator;

    void Awake ()
    {
        animator = GetComponent<Animator> ();
    }

    void Start ()
    {
          if (closeOnButtonClick)
          {
              Button[] buttons = GetComponentsInChildren<Button> ();
              for (int i = 0; i < buttons.Length; i++)
              {
                  if (buttons[i] is Button)
                  {
                      ((Button) buttons[i]).onClick.AddListener (HideWindow);
                  }
              }
          }
    }

    // Update is called once per frame
    void Update ()
    {
        if (closeOnClick && Input.GetMouseButtonUp (0) && gameObject.activeSelf)
            HideWindow ();
    }

    public void ShowWindow ()
    {

        Debug.Log("Showing Window");
        gameObject.SetActive (true);

        if (animator != null)
            animator.Play ("intro");
        else 
        {
            LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(
                delegate()
                {
                    gameObject.SetActive (true);
                }
                );
         }
    }

    public void HideWindow ()
    {
        Debug.Log("Hiding Window");
        LeanTween.scale(gameObject, Vector3.zero, 0.2f).setEase(LeanTweenType.easeOutCubic).setOnComplete(
            delegate () 
            {        
                if (animator != null)
                    animator.Play ("outro");
                else
                    gameObject.SetActive (false);
            });
       
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if (closeOnClickOutside && gameObject.activeSelf)
            HideWindow ();
    }
}