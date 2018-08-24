using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        base.Update();  //executes Character.cs update|| base = access inheritance
        
        

        
	}
   

    //player direction/movement control
    private void GetInput()
    {
        //reset direction after every loop
        direction = Vector2.zero; //Prevents incrementing speed every loop causing player to run faster and faster.
        //GetKey = Hold key down
        //GetKeyDown = Toggle mode
        if(Input.GetKey(KeyCode.W))
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
