using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    //Icon used when moving and placing item
    [SerializeField]
    private Sprite icon;
    //Size of the stack
    [SerializeField]
    private int stackSize;
    //The item's title
    [SerializeField]
    private string title;
    //The item's quality
    [SerializeField]
    private Quality quality;
    //Reference to the slot the item is on
    private SlotScript slot;
    //Property for accessing the icon
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    //Property for accessing the stack size
    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }
    //Property for accessing the slotScript
    public SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public Quality MyQuality
    {
        get
        {
            return quality;
        }

    
    }

    public string MyTitle
    {
        get
        {
            return title;
        }

      
    }

    //Returns the description of the item
    public virtual string GetDescription()
    {
        
        
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality], MyTitle);
    }
    //Removes item from the inventory
    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
