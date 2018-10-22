﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    private LootWindow lootWindow;

    public Image MyIcon
    {
        get
        {
            return icon;
        }

       
    }

    public Text MyTitle
    {
        get
        {
            return title;
        }

       
    }

    public Item MyLoot { get; set; }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //for looting
        if (InventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideToolTip();
        }
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowToolTip(new Vector2 (1,0), transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}
