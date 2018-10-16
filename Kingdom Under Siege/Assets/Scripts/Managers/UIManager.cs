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
    //Reference to all the action buttons
    [SerializeField]
    private ActionButton[] actionButtons;  

    //[SerializeField]
    private Stat healthStat; //health of unit

    [SerializeField]
    private GameObject targetFrame; //unit frame to shwo target

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private CharacterPanel charPanel;

    private Text toolTipText;
    //Reference to the keybind menu
    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;
    //Reference to all the keybind buttons on the menu
    private GameObject[] keybindButtons;
    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("keybind");
        toolTipText = tooltip.GetComponentInChildren<Text>();
    }
    
	// Use this for initialization
	void Start ()
    {        
        healthStat = targetFrame.GetComponentInChildren<Stat>();//reference child component Stat from the unity heirarchy (an alternate from making it public        
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keybindMenu);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(spellBook);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            charPanel.OpenClose();
        }
    }

    //Make target frame appear of targeted enemy
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

    // Time.timeScale = Time.timeScale > 0 ? 0 : 1; //pause the game 

    //Updates the text on a keybindbutton after the key has been changed
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

 
    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1; //if alpha is more than 0 = then alpha is set to 0 else go back to 1
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }
    //Updates the stacksize on a clickable slot
    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)//if the slot has more then 1 item on it
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else //if it only has 1 item
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);//if item is 1 remove text
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0)//if the slot is empty, then hide icon
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
    //Shows the tooltip
    public void ShowToolTip(Vector3 position, IDescribable desription)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        toolTipText.text = desription.GetDescription();
    }
    //Hides the tooltip
    public void HideToolTip()
    {
        tooltip.SetActive(false);
    }
}
