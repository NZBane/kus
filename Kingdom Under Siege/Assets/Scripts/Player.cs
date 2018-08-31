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

  
    private float initMana = 50; //for adjusting mana of player

    [SerializeField]
    private GameObject[] spellPrefab; //for different spells

    [SerializeField]
    private Transform[] exitPoints; //for adjusting spell exit point from the character prefab staff

    //assigned initially to index 2 as player respawns looking down initially
    private int exitIndex = 2; //use to assign righ tdirection (will be used for the inputs below)

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
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //if not attacking and not moving, then the player can attack again
            if (!isAttacking && !IsMoving)
            {
                //coroutine = an action that can be done simoultaenously with other functions
                attackRoutine = StartCoroutine(Attack());
            }
            
        }
    }
   
    //good for puttign delays
    private IEnumerator Attack()
    {
     
        isAttacking = true;
        myAnimator.SetBool("attack", true);
        CastSpell();
        yield return new WaitForSeconds(2); //cast time hard code
       
        StopAttack();
        
    }

    public void CastSpell()
    {
        //quaternion = rotation of the prefab
        //instantiate the spell prefab on the position of the exit points which are determined by the inputs
        Instantiate(spellPrefab[0], exitPoints[exitIndex].position, Quaternion.identity);
       
    }

}
