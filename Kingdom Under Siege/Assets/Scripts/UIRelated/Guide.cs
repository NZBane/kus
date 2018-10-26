using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {

    private static Guide instance;
    [SerializeField]
    private CanvasGroup canvasGroup;

    public static Guide MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Guide>();
            }
            return instance;
        }



        
    }



    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            Time.timeScale = Time.timeScale > 0 ? 0 : 1; //pause the game 


        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            Time.timeScale = Time.timeScale > 0 ? 0 : 1; //pause the game 

        }
    }
}
