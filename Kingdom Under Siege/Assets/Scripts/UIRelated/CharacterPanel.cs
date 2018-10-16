using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvasGroup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
}
