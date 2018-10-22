using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    //singleton is used for when theres only one type of obejct in the game and needed to ben access from other palces in the game
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }
    }
    //The current moveable
    public IMoveable MyMoveable { get; set;}
    //the icon of the item
    private Image icon;
    //offset to move the icon away from the mouse
    [SerializeField]
    private Vector3 offset;
	// Use this for initialization
	void Start ()
    {
        //creates a referance 
        icon = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        icon.transform.position = Input.mousePosition+offset; //move icon to mouse when clicked
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
        
	}
    //take a moveable so that it can be  moved around
    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        InventoryScript.MyInstance.FromSlot = null;
      
    }
    //deletes an item from the inventory
    public void DeleteItem()
    {
      
        if (MyMoveable is Item && InventoryScript.MyInstance.FromSlot != null)
        {
            (MyMoveable as Item).MySlot.Clear();
        }
        Drop();

        InventoryScript.MyInstance.FromSlot = null;
    }
}
