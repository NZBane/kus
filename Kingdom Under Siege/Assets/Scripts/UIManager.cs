using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //singleton is used for when theres only one type of obejct in the game and needed to ben access from other palces in the game
    private static UIManager instance;


    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
                {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3;

    //[SerializeField]
    private Stat healthStat; //health of unit

    [SerializeField]
    private GameObject targetFrame; //unit frame to shwo target

    [SerializeField]
    private Image portraitFrame;
    
	// Use this for initialization
	void Start ()
    {

        healthStat = targetFrame.GetComponentInChildren<Stat>();//reference child component Stat from the unity heirarchy (an alternate from making it public
        //keybinds
        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
    }
    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }

    //make target frame appear of targeted enemy
    public void ShowTargetFrame(NPC target)
    {
        targetFrame.SetActive(true);
        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);
       portraitFrame.sprite = target.MyPortrait;
        target.healthChanged += new HealthChanged(UpdateTargetFrame); //frame gets updated per frame to match enemy health bar
        target.characterRemoved += new CharacterRemoved(HideTargetFrame); //bug fix hide frame when character is removed
    }


    //hide target frame
    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }
}
