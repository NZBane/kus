using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    //Stack for all items on this slot
    private ObservableStack<Item> items = new ObservableStack<Item>();
    //Reference to the slots icon
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;
    //Reference to the bag that this slot belongs to
    public BagScript MyBag { get; set; }
    //Checks if the item is empty
    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }
    //Indicates if the slot is full
    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }
    //Cureent item on the slot
    public Item MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }
    }
    //The icon on the slot
    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }
    //The item count on the slot
    public int MyCount
    {
        get
        {
            return items.Count;
        }
    }
    //Text of the stack
    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    //Adds item to the slot
    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }
                AddItem(newItems.Pop());            
            }
            return true;
        }
        return false;
    }

    //Removes item from the slot
    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            InventoryScript.MyInstance.OnItemCountChanged(items.Pop());
        }
    }

    public void Clear()
    {
        if (items.Count > 0)
        {
            InventoryScript.MyInstance.OnItemCountChanged(items.Pop());
            items.Clear();
        }
    }

    //When the slot is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)//if we dont have something to move
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    if (HandScript.MyInstance.MyMoveable is Bag)
                    {
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                           // UIManager.MyInstance.RefreshTooltip();
                            HandScript.MyInstance.Drop();
                        }
                    }
                    
                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    //Dequips a bag from the inventory
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                    if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.Slots > 0)
                    {
                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                       
                    }
                }
               else if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    AddItem(armor);
                    CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                    HandScript.MyInstance.Drop();
                }
            }
            
            else if (InventoryScript.MyInstance.FromSlot!= null)//if we have something to move
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)//when we right click on the slot
        {
            UseItem();//uses the item we clicked
        }
    }
    //Uses the item if it can be used
    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if(MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }
    //Stacks two items
    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && items.Count <MyItem.MyStackSize)
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }
    //Puts items back in the inventory
    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }
    //Swaps two items in the inventory
    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
            //copy all the items we need to swap from A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);
            //clears slot A
            from.items.Clear();
            //All items from slot B, copy into A
            from.AddItems(items);
            //clear B
            items.Clear();
            //Move the item from A, copy to B
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }
    //Merges two stacks of the same items
    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            //How many free slots are available in the stack
            int free = MyItem.MyStackSize - MyCount;
            for (int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }
            return true;
        }
        return false;
    }
    //Updates the slot
    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Show tooltip
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowToolTip(new Vector2(1,0), transform.position, MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hides tooltip
        UIManager.MyInstance.HideToolTip();
    }
}
