using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    //prefab for creating slots
    [SerializeField]
    private GameObject slotPrefab;
    //Canvas group for hiding or showing the bag
    private CanvasGroup canvasGroup;
    //List of all the slots that belong to the bag
    private List<SlotScript> slots = new List<SlotScript>(); 
    //If this bag is open of closed
    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    public List<SlotScript> MySlots
    {
        get
        {
            return slots;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in MySlots)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    private void Awake()
    {
        //Creates a referance to the canvas group
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();

        foreach (SlotScript slot in slots)
        {
            if (!slot.IsEmpty)
            {
                foreach (Item  item in slot.MyItems)
                {
                    items.Add(item);
                }
            }
        }
        return items;
    }

    //The amount of slots to create in each bag
    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slot.MyBag = this;
            MySlots.Add(slot);
        }
    }
    //Adds an item to the bag
    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in MySlots)//Checks all the slots
        {
            if (slot.IsEmpty)//If the slots is empty then we add the item
            {
                slot.AddItem(item);//Adds Item

                return true; //Successfully added item
            }
        }
        return false;//Failed to add item
    }

    //Opens or closes the bag
    public void OpenClose()
    {
        //Changes the alpha to open or closed
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        //Blocks or removes raycast blocking
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

}
