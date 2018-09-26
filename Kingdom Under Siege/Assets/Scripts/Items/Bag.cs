using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bag",menuName ="Items/Bag",order =1)]
public class Bag : Item, IUseable
{ 
    //The amount of slots this bag has
    private int slots;
    //Refernences to the bag prefab
    [SerializeField]
    private GameObject bagPrefab;
    //Reference to the bagScript
    public BagScript MyBagScript { get; set; }
    //Reference to the bag button this bag is attached to
    public BagButton MyBagButton { get; set; }
    //Property for getting slots
    public int Slots
    {
        get
        {
            return slots;
        }
    }
    //Initializes the bad with the amount of slots
    public void Initialize(int slots)
    {
        this.slots = slots;
    }
    //Equips the bag
    public void Use()
    {
        if(InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            if (MyBagButton == null)
            {
                InventoryScript.MyInstance.AddBag(this);
            }
            else
            {
                InventoryScript.MyInstance.AddBag(this, MyBagButton);
            }
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} Slot Bag", slots);
    }
}
