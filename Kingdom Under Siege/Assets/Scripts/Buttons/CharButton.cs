using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private ArmorType armorType;
    private Armor equippedArmor;
    [SerializeField]
    private Image icon;

    [SerializeField]
    private GearSocket gearSocket;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;
                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);

                }
                UIManager.MyInstance.RefreshTooltip(tmp);
            }
            else if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedArmor);
                CharacterPanel.MyInstance.MySelectedButton = this;
                icon.color = Color.red;
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();
        if (equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                armor.MySlot.AddItem(equippedArmor);
            }
         
            UIManager.MyInstance.RefreshTooltip(equippedArmor);
        }
        else
        {
            UIManager.MyInstance.HideToolTip();
        }
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        this.equippedArmor = armor;
        if(HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }
        HandScript.MyInstance.Drop();
        if(gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Equip(equippedArmor.MyAnimationClips);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)  
        {
            UIManager.MyInstance.ShowToolTip(new Vector2 (0,0), transform.position, equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        equippedArmor = null;
        if (gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Dequip();
        }
        equippedArmor = null;
    }
}
