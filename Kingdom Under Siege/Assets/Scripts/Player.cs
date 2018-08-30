using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;



    
    private float initHealth = 100; //for adjusting health of player

  
    private float initMana = 100; //for adjusting mana of player


    // Use this for initialization
    protected override void Start () {
        health.Initialize(initHealth,initHealth);
        health.Initialize(initMana, initMana);


        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        //health.MyCurrentValue = 100;
        
        base.Update();  //executes Character.cs update|| base = access inheritance
       
        

        
	}
   

    //player direction/movement control
    private void GetInput()
    {
       




        //reset direction after every loop
        direction = Vector2.zero; //Prevents incrementing speed every loop causing player to run faster and faster.
                                 
        
        ///DEBUGGING ONLY!!!
                                  ///
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }




        //GetKey = Hold key down
        //GetKeyDown = Toggle mode
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
