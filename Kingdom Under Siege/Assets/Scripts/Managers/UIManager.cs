using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    private ActionButton[] actionButtons;

    

    //[SerializeField]
    private Stat healthStat; //health of unit

    [SerializeField]
    private GameObject targetFrame; //unit frame to shwo target

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private CanvasGroup keybindMenu;

    private GameObject[] keybindButtons;
    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("keybind");
    }
    
	// Use this for initialization
	void Start ()
    {
        
        healthStat = targetFrame.GetComponentInChildren<Stat>();//reference child component Stat from the unity heirarchy (an alternate from making it public
        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("fireBall"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("frostOrb"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("lightningBolt"));
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCloseMenu();
        }
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

    public void OpenCloseMenu()
    {
        keybindMenu.alpha = keybindMenu.alpha > 0 ? 0 : 1; //if alpha is more than 0 = then alpha is set to 0 else go back to 1
        keybindMenu.blocksRaycasts = keybindMenu.blocksRaycasts == true ? false : true;
        Time.timeScale = Time.timeScale > 0 ? 0 : 1; //pause the game

    }
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyButton.image.sprite = useable.MyIcon;
        btn.MyButton.image.color = Color.white;
        btn.MyUseable = useable;
    }
}
