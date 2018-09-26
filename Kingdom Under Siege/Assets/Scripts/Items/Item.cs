using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality {Common, Uncommon, Rare, Epic}

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
    //Returns the description of the item
    public virtual string GetDescription()
    {
        string color = string.Empty;

        switch (quality)
        {
            case Quality.Common:
                color = "#515551";
                break;
            case Quality.Uncommon:
                color = "#0ac404";
                break;
            case Quality.Rare:
                color = "#0588c5";
                break;
            case Quality.Epic:
                color = "#1f04ba";
                break;
        }
        return string.Format("<color={0}>{1}</color>", color, title);
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
