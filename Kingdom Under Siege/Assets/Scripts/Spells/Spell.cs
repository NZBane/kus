using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;


[System.Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
    //Spells name
    [SerializeField]
    private string name;
    //Spells damage
    [SerializeField]
    private int damage;
    //Spells Icon
    [SerializeField]
    private Sprite icon;
    //Speed of the spell
    [SerializeField]
    private float speed;
    //Castime of the spell
    [SerializeField]
    private float castTime;
    //Spells Prefab
    [SerializeField]
    private GameObject spellPrefab;
    //Reference to the spells description
    [SerializeField]
    private string description;
    //Spells color
    [SerializeField]
    private Color barColor;
    //Property for accessing spells name
    public string MyName
    {
        get
        {
            return name;
        }

     
    }
    //Proerty for reading the damage
    public int MyDamage
    {
        get
        {
            return damage;
        }

     
    }
    //Property for reading the icon
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }

      
    }
    //Property for reading the speed
    public float MySpeed
    {
        get
        {
            return speed;
        }

    
    }
    //Property for reading the cast time
    public float MyCastTime
    {
        get
        {
            return castTime;
        }

     
    }
    //Property for reading the spell's prefab
    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }

      
    }
    //Property for reading the color
    public Color MyBarColor
    {
        get
        {
            return barColor;
        }

     
    }

    public string GetDescription()
    {
        return string.Format("<color=#bc0000>{0}</color>\nCast time: {1} second(s)\n<color=#323232>{2}\nthat causes {3} damage</color>", name, castTime, description, damage);
    }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}