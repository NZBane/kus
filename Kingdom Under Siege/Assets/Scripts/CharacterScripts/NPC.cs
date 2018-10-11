using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//delegates allows the creation of events 
//use for when enemy's health changes
public delegate void HealthChanged(float health);

public delegate void CharacterRemoved();

public class NPC : Character
{
    public event HealthChanged healthChanged;

    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private Sprite portrait;

    public Sprite MyPortrait
    {
        get
        {
            return portrait;
        }

   
    }

    public virtual void DeSelect()
    {
        healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTargetFrame);
        characterRemoved -= new CharacterRemoved(UIManager.MyInstance.HideTargetFrame);
    }
    public virtual Transform Select() //transform is teh hit box
    {
        return hitBox;
    }

    public void OnHealthChanged(float health) //delegate method event
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    //remove character
    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }
        Destroy(gameObject);
    }

    public virtual void Interact()
    {
        Debug.Log("this will open a dialogue with the NPC");
    }
}
