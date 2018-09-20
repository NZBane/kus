using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public IMoveable MyMoveable { get; set;}

    private Image icon;

    [SerializeField]
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        icon = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        icon.transform.position = Input.mousePosition+offset; //move icon to mouse when clicked
	}

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
}
